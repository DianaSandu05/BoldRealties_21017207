using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using BoldRealties.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authentication;
using BoldRealties.DAL.Repository.IRepository;
using BoldRealties.BLL;

namespace BoldRealties.Web.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class ExternalLoginModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IEmailSender _emailSender;
        private readonly ILogger<ExternalLoginModel> _logger;
        private readonly IUserStore<IdentityUser> _userStore;
        private readonly IUserEmailStore<IdentityUser> _emailStore;
        private readonly IUnitOfWork _unitOfWork;

        public ExternalLoginModel(
            SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager,
             IUserStore<IdentityUser> userStore,
            ILogger<ExternalLoginModel> logger,
            IEmailSender emailSender,
            IUnitOfWork unitOfWork)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _logger = logger;
            _userStore = userStore;
            _emailStore = GetEmailStore();
            _emailSender = emailSender;
            _unitOfWork = unitOfWork;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ProviderDisplayName { get; set; }

        public string ReturnUrl { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }
             public string firstName { get; set; }
            [Required]
            public string lastName { get; set; }
            public string? PhoneNumber { get; set; }
        }

        public IActionResult OnGetAsync()
        {
            return RedirectToPage("./Login");
        }

        public IActionResult OnPost(string provider, string returnUrl = null)
        {
            // Request a redirect to the external login provider.
            var redirectUrl = Url.Page("./ExternalLogin", pageHandler: "Callback", values: new { returnUrl });
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
            return new ChallengeResult(provider, properties);
        }
        private readonly IReadOnlyDictionary<string, string> _claimsToSync =
     new Dictionary<string, string>()
     {
             { "account_id", "account_id" },
             {"account_name", "account_name" },
             {"accounts", "accounts" },
             { "access_token", "access_token" },
             {"base_uri", "base_uri" },
             {"refresh_token", "refresh_token" },
             { "expires_in", "expires_in"}
     };
        public async Task<IActionResult> OnGetCallbackAsync(string returnUrl = null, string remoteError = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            if (remoteError != null)
            {
                ErrorMessage = $"Error from external provider: {remoteError}";
                return RedirectToPage("./Login", new {ReturnUrl = returnUrl });
            }
            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                ErrorMessage = "Error loading external login information.";
                return RedirectToPage("./Login", new { ReturnUrl = returnUrl });
            }

            // Sign in the user with this external login provider if the user already has a login.
            var result = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false, bypassTwoFactor : true);
            if (result.Succeeded)
            {
                _logger.LogInformation("{Name} logged in with {LoginProvider} provider.", info.Principal.Identity.Name, info.LoginProvider);
                if (_claimsToSync.Count > 0)
                {
                    var user = await _userManager.FindByLoginAsync(info.LoginProvider,
                        info.ProviderKey);
                    var userClaims = await _userManager.GetClaimsAsync(user);
                    bool refreshSignIn = false;

                    foreach (var addedClaim in _claimsToSync)
                    {
                        var userClaim = userClaims
                            .FirstOrDefault(c => c.Type == addedClaim.Key);

                        if (info.Principal.HasClaim(c => c.Type == addedClaim.Key))
                        {
                            var externalClaim = info.Principal.FindFirst(addedClaim.Key);

                            if (userClaim == null)
                            {
                                await _userManager.AddClaimAsync(user,
                                    new Claim(addedClaim.Key, externalClaim.Value));
                                refreshSignIn = true;
                            }
                            else if (userClaim.Value != externalClaim.Value)
                            {
                                await _userManager
                                    .ReplaceClaimAsync(user, userClaim, externalClaim);
                                refreshSignIn = true;
                            }
                        }
                        else if (userClaim == null)
                        {
                            // Fill with a default value
                            await _userManager.AddClaimAsync(user, new Claim(addedClaim.Key,
                                addedClaim.Value));
                            refreshSignIn = true;
                        }
                    }

                    if (refreshSignIn)
                    {
                        await _signInManager.RefreshSignInAsync(user);
                    }
                }
                return LocalRedirect(returnUrl);
            }
            if (result.IsLockedOut)
            {
                return RedirectToPage("./Lockout");
            }
            else
            {
                // If the user does not have an account, then ask the user to create an account.
                ReturnUrl = returnUrl;
                ProviderDisplayName = info.ProviderDisplayName;
                if (info.Principal.HasClaim(c => c.Type == ClaimTypes.Email))
                {
                    Input = new InputModel
                    {
                        Email = info.Principal.FindFirstValue(ClaimTypes.Email),
                        firstName = info.Principal.FindFirstValue(ClaimTypes.Name),
                        lastName = info.Principal.FindFirstValue(ClaimTypes.GivenName)
                    };
                }
                return Page();
            }
        }

        public async Task<IActionResult> OnPostConfirmationAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("https://boldrealties.azurewebsites.net/");
            // Get the information about the user from the external login provider
            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                ErrorMessage = "Error loading external login information during confirmation.";
                return RedirectToPage("./Login", new { ReturnUrl = returnUrl });
            }

            if (ModelState.IsValid)
            {
                /*var user = new IdentityUser { UserName = Input.Email, Email = Input.Email };
*/
                var user = CreateUser();
                await _userStore.SetUserNameAsync(user, Input.Email, CancellationToken.None);
                await _emailStore.SetEmailAsync(user, Input.Email, CancellationToken.None);
                user.firstName = Input.firstName;
                user.lastName = Input.lastName;
                user.PhoneNumber = Input.PhoneNumber;
                var result = await _userManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, StaticDetails.Role_Tenant);
                    result = await _userManager.AddLoginAsync(user, info);
                    if (result.Succeeded)
                    {
                        _logger.LogInformation("User created an account using {Name} provider.", info.LoginProvider);
                        // If they exist, add claims to the user for:
                        //    Given (first) name
                        //    Locale
                        //    Picture
                   
                     
                        // Include the access token in the properties
                        // using Microsoft.AspNetCore.Authentication;
                        var props = new AuthenticationProperties();
                        props.StoreTokens(info.AuthenticationTokens);
                        props.IsPersistent = true;

                        var userId = await _userManager.GetUserIdAsync(user);
                        var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                        code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                        var callbackUrl = Url.Page(
                            "/Account/ConfirmEmail",
                            pageHandler: null,
                            values: new { area = "Identity", userId = userId, code = code },
                            protocol: Request.Scheme);

                        await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                            $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                        // If account confirmation is required, we need to show the link if we don't have a real email sender
                        if (_userManager.Options.SignIn.RequireConfirmedAccount)
                        {
                            return RedirectToPage("./RegisterConfirmation", new { Email = Input.Email });
                        }

                        await _signInManager.SignInAsync(user, isPersistent: false, info.LoginProvider);

                        return LocalRedirect(returnUrl);
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            ProviderDisplayName = info.ProviderDisplayName;
            ReturnUrl = returnUrl;
            return Page();
        }

        private Users CreateUser()
        {
            try
            {
                return Activator.CreateInstance<Users>();
            }
            catch
            {

                throw new InvalidOperationException($"Can't create an instance of '{nameof(IdentityUser)}" +
                    $"Ensure that '{nameof(IdentityUser)}' is not an abstract class and had a parameter" +
                    $"override the register page in Areas/Identity/Pages/Account/Register.cshtml");
            }
        }
        private IUserEmailStore<IdentityUser> GetEmailStore()
        {
            if (!_userManager.SupportsUserEmail)
            {
                throw new NotSupportedException("The default UI requires a user store with email support.");
            }
            return (IUserEmailStore<IdentityUser>)_userStore;
        }
    }
}
