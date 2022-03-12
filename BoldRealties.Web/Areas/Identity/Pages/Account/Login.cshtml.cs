using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using BoldRealties.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using BoldRealties.BLL;
using System.Security.Claims;
using BoldRealties.DAL.Repository.IRepository;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BoldRealties.Web.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class LoginModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ILogger<LoginModel> _logger;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IUnitOfWork _unitOfWork;

        public LoginModel(SignInManager<IdentityUser> signInManager, 
            ILogger<LoginModel> logger,
            UserManager<IdentityUser> userManager,
             RoleManager<IdentityRole> roleManager,
             IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _roleManager = roleManager;
            _unitOfWork = unitOfWork;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public string ReturnUrl { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [Display(Name = "Remember me?")]
            public bool RememberMe { get; set; }
        
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
           
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }

            returnUrl ??= Url.Content("~/");

            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string? returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");

            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        
            if (ModelState.IsValid)
            {
               /* RoleList = _roleManager.Roles.FindFirst(x => x.Name).Where(y => y.email == Input.Email)()*/
                
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var result = await _signInManager.PasswordSignInAsync(Input.Email, Input.Password, Input.RememberMe, lockoutOnFailure: false);
                var identityUser = _userManager.FindByIdAsync(Input.Email);
                // Resolve the user via their email
                var user = await _userManager.FindByEmailAsync(Input.Email);
                // Get the roles for the user
                var roles = await _userManager.GetRolesAsync(user);


                if (result.Succeeded)
                {
                    _logger.LogInformation("User logged in.");
                    if (roles.Contains("Landlord"))
                    {
                        return RedirectToPage("ViewMyTenancy", "tenancies", new { email = Input.Email, returnUrl = returnUrl });

                    }
                    else if (roles.Contains("Tenant"))
                    {
                        return RedirectToPage("Index", "tenancies", new { email = Input.Email, returnUrl = returnUrl });
                    }
                    else if (roles.Contains("Admin"))
                    {
                        return RedirectToPage("Index", "tenancies", new { email = Input.Email, returnUrl = returnUrl });
                    }
                    else if (roles.Contains("User"))
                    {
                        return RedirectToPage("Index", "PropertiesRS", new { email = Input.Email, returnUrl = returnUrl });

                    }
                    else if (roles.Contains("Subcontractor"))
                    {
                        return RedirectToPage("AddMJ", "maintenanceJobs", new { email = Input.Email, returnUrl = returnUrl });

                    }
                    else
                    {
                        return LocalRedirect(returnUrl);
                    }
                  
                }
                if (result.RequiresTwoFactor)
                {
                    return RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, RememberMe = Input.RememberMe });
                }
                if (result.IsLockedOut)
                {
                    _logger.LogWarning("User account locked out.");
                    return RedirectToPage("./Lockout");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return Page();
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
