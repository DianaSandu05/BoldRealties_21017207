using BoldRealties.BLL;
using BoldRealties.DAL.Repository.IRepository;
using BoldRealties.Models;
using BoldRealties.Models.ViewModels;
using BoldSign.Api;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace BoldRealties.Web.Controllers
{
    /// <summary>
    /// Initializes a new instance of the <see cref="HomeController"/> class.
    /// </summary>
    /// <param name=" apiClient ">The api client.</param>
    public class TenancyController : Controller
    {
        private readonly ApiClient apiClient;
        private readonly IUnitOfWork _unit;
        private readonly IWebHostEnvironment _webHost;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger<TenancyController> _logger;
        public tenancyVM TenancyVM { get; set; }


        public TenancyController(IUnitOfWork unit, ILogger<TenancyController> logger, IWebHostEnvironment webHost,
            UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _unit = unit;
            _webHost = webHost;
            _userManager = userManager;
            _roleManager = roleManager;
            _logger = logger;
        
        }

        //function for details of the tenancy for payment
        [Authorize(Roles = StaticDetails.Role_Admin)]
        public IActionResult Index()
        {
            IEnumerable<tenancies> objTenanciesList = _unit.Tenancies.GetAll();
            return View(objTenanciesList);

        }

        //function for details of the tenancy for payment
        [Authorize(Roles = StaticDetails.Role_Admin)]
        public IActionResult Upsert(int? ID)
        {
            tenancyVM tenancyVM = new()
            {
                tenancy = new(),
                PropertiesList = _unit.Properties.GetAll().Select(x => new SelectListItem
                {
                    Text = x.ID.ToString(),
                    Value = x.ID.ToString()
                }),
                UserList = _unit.Users.GetAll().Select(x => new SelectListItem
                {
                    /*Text = x.Id.ToString(),
                    Value = x.ID.ToString()*/
                    Text = x.firstName + x.lastName,
                    Value = x.Id.ToString()
                }),
            };
            if (ID == null || ID == 0)
            {
                return View(tenancyVM);
            }
            else
            {
                tenancyVM.tenancy = _unit.Tenancies.GetFirstOrDefault(u => u.Id == ID);
                return View(tenancyVM);


            }

        }
        [HttpPost]

        //function for admin to update and insert tenancy
        [Authorize(Roles = StaticDetails.Role_Admin)]
        [ValidateAntiForgeryToken] //to avoid the cross site request forgery
        public IActionResult Upsert(tenancyVM tenancyVM, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHost.WebRootPath;
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString();
                    var fileUploads = Path.Combine(wwwRootPath, @"files\tenancy");
                    var extension = Path.GetExtension(file.FileName);

                    if (tenancyVM.tenancy.filePath != null)
                    {
                        var oldImagePath = Path.Combine(wwwRootPath, tenancyVM.tenancy.filePath.TrimStart('\\'));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    using (var fileStreams = new FileStream(Path.Combine(fileUploads, fileName + extension), FileMode.Create))
                    {
                        file.CopyTo(fileStreams);
                    }
                    tenancyVM.tenancy.filePath = @"\files\tenancy\" + fileName + extension;

                }
                if (tenancyVM.tenancy.Id == 0)
                {
                    _unit.Tenancies.Add(tenancyVM.tenancy);
                }
                else
                {
                    _unit.Tenancies.Update(tenancyVM.tenancy);
                }
                _unit.Save();
                TempData["success"] = "Record added successfully";
                return RedirectToAction("Index");
            }
            return View(tenancyVM);

        }
        /*
              logical:
              Function Details()
              1. get tenancy id, create payment obj and pass the tenanancy details. return the obj

              Function DetailsPayment()
              1. claim the identity of the tenant
              2. pass the identity claim value to the payment obj.userID
              3. get shopping cart from database if the ApplicationUserId is equal to user id from the claim identity
              4. and product id is equal tot the product id (FK) from shopping cart
               */

        //function for details of the tenancy for payment
        [Authorize(Roles = StaticDetails.Role_Tenant)]
        public IActionResult Details(int TenancyId)
        {
            var tenancyFromDb = _unit.Tenancies.
                        GetFirstOrDefault(u => u.Id == TenancyId, includeProperties: "PropertiesRS");
            ShoppingCart payment = new ShoppingCart()
            {
                tenancies = tenancyFromDb,
                TenancyId = tenancyFromDb.Id
            };
            return View(payment);
        }

        //function for details of the tenancy for payment
        [Authorize(Roles = StaticDetails.Role_Tenant)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult Details(ShoppingCart shoppingCart)
        {
           
                //DS: we create a variable 'claimsIdentity' and we initialize it with the name identifier
                var claimsIdentity = (ClaimsIdentity)User.Identity;
                //DS: we then create another variable 'claim' to extract the claim from the ClaimsIdentity
                var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
          
            shoppingCart.UserId = claim.Value;
            
            ShoppingCart paymentFromDb = _unit.ShoppingCart.GetFirstOrDefault(
                    u => u.UserId == shoppingCart.UserId && u.TenancyId == shoppingCart.TenancyId
                    , includeProperties: "tenancies"
                    );
                //DS: the if statement conditions checks if there is no record. If true, a new record will be added to DB
                if (paymentFromDb == null)
                {
                    _unit.ShoppingCart.Add(shoppingCart);
                    _unit.Save();
                    HttpContext.Session.SetInt32(StaticDetails.SessionPayment,
                        _unit.ShoppingCart.GetAll(u => u.UserId == claim.Value).ToList().Count);
                }
                else
                {
                    _unit.ShoppingCart.IncrementCount(paymentFromDb, shoppingCart.Count);
                    _unit.Save();
                }
                _unit.Save();

          
                return RedirectToAction(nameof(Index));
       
        }
        [Authorize(Roles = StaticDetails.Role_Admin)]
        //view my tenancy function used to display the tenancy details for admin
        public IActionResult TenancyDetails(int? ID)
        {
            tenancyVM tenancyVM = new()
            {
                tenancy = new(),
                PropertiesList = _unit.Properties.GetAll().Select(x => new SelectListItem
                {
                    Text = x.propertyAddress,
                    Value = x.ID.ToString()
                }),
                UserList = _unit.Users.GetAll().Select(x => new SelectListItem
                {
                    Text = x.firstName + x.lastName,
                    /*   Value = x.ID.ToString()*/
                }),
            };
            if (ID == null || ID == 0)
            {
                return View(tenancyVM);
            }
            else
            {
                tenancyVM.tenancy = _unit.Tenancies.GetFirstOrDefault(u => u.Id == ID);
                return View(tenancyVM);


            }
        }
        [Authorize(Roles = StaticDetails.Role_Tenant)]
        public IActionResult Find()
        {

            //DS: we create a variable 'claimsIdentity' and we initialize it with the name identifier
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            //DS: we then create another variable 'claim' to extract the claim from the ClaimsIdentity
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);


            IEnumerable<tenancies> objTenanciesList = _unit.Tenancies.GetAll().Where(u=> u.UserID == claim.Value);
            return View(objTenanciesList);


        }

        //function to get a list with all payments made from all users for all tenancies
        [Authorize(Roles = StaticDetails.Role_Admin)]
        public IActionResult TenancyPayments()
        {
            IEnumerable<OrderHeader> obj = _unit.OrderHeader.GetAll();
            return View(obj);
        }
        }

      
    }
