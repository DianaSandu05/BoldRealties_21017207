using BoldRealties.BLL;
using BoldRealties.DAL.Repository.IRepository;
using BoldRealties.Models;
using BoldRealties.Models.ViewModels;
using DocuSign.eSign.Api;
using DocuSign.eSign.Client;
using DocuSign.eSign.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace BoldRealties.Web.Controllers
{

    public class TenancyController : Controller
    {
        private readonly IUnitOfWork _unit;
        private readonly IWebHostEnvironment _webHost;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger<TenancyController> _logger;
        /// <summary>
        /// Creates an envelope that would include two documents and add a signer and cc recipients to be notified via email
        /// </summary>
        /// <param name="signerEmail">Email address for the signer</param>
        /// <param name="signerName">Full name of the signer</param>
        /// <param name="ccEmail">Email address for the cc recipient</param>
        /// <param name="ccName">Name of the cc recipient</param>
        /// <param name="accessToken">Access Token for API call (OAuth)</param>
        /// <param name="basePath">BasePath for API calls (URI)</param>
        /// <param name="accountId">The DocuSign Account ID (GUID or short version) for which the APIs call would be made</param>
        /// <param name="docPdf">String of bytes representing the document (pdf)</param>
        /// <param name="docDocx">String of bytes representing the Word document (docx)</param>
        /// <param name="envStatus">Status to set the envelope to</param>
        /// <returns>EnvelopeId for the new envelope</returns>

        public TenancyController(IUnitOfWork unit, ILogger<TenancyController> logger, IWebHostEnvironment webHost, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager
       )
        {
            _unit = unit;
            _webHost = webHost;
            _userManager = userManager;
            _roleManager = roleManager;
            _logger = logger;
            ViewBag.title = "Signing request by email";
        }
        public IActionResult Index()
        {
            IEnumerable<tenancies> objTenanciesList = _unit.Tenancies.GetAll();
            return View(objTenanciesList);
           
        }
        public IActionResult Upsert(int? ID)
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
        [HttpPost]
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

        [HttpDelete]
        public IActionResult DeleteTenancies(int? ID)
        {
            if (ID == null || ID == 0)
            {
                return NotFound();
            }
            var TenancyFromDb = _unit.Tenancies.GetFirstOrDefault(x => x.Id == ID);
            if (TenancyFromDb == null)
            {
                return NotFound();
            }
            return View(TenancyFromDb);
        }
        [HttpPost]
        [ValidateAntiForgeryToken] //to avoid the cross site request forgery
        public IActionResult DeleteTenancy(int? ID)
        {
            var tenancies = _unit.Tenancies.GetFirstOrDefault(x => x.Id == ID);
            if (tenancies == null)
            {
                return NotFound();
            }

            _unit.Tenancies.Remove(tenancies);
            _unit.Save();
            TempData["success"] = "The record was deleted successfully!";
            return RedirectToAction("Index");


        }
        //most probably this should be moved in the user portal??

        //To do for Details:
        /*
         1. The details should be taken from user page when the tenant view his tenancy
        2. Show payment due
        3. Click details
        4. View details about payments and have a button for 'shopping cart' aka 'pay now'
        5. Redirect to payment summary. 

        logical:
        Function Details()
        1. get tenancy id, create payment obj and pass the tenanancy details. return the obj

        Function DetailsPayment()
        1. claim the identity of the tenant
        2. pass the identity claim value to the payment obj.userID
        3. get shopping cart from database if the ApplicationUserId is equal to user id from the claim identity
        4. and product id is equal tot the product id (FK) from shopping cart
         */
        public IActionResult Details(int id)
        {
            var tenancyFromDb = _unit.Tenancies.
                        GetFirstOrDefault(u => u.Id == id, includeProperties: "PropertiesRS");
            //shopping cart = payment
            //product = tenancy

            payment paymentObj = new payment()
            {
                tenancies = tenancyFromDb,
                TenancyID = tenancyFromDb.Id
            };
            return View(paymentObj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult Details(payment paymentObj)
        {

            paymentObj.ID = 0;
            if (ModelState.IsValid)
            {
                //DS: we create a variable 'claimsIdentity' and we initialize it with the name identifier
                var claimsIdentity = (ClaimsIdentity)User.Identity;
                //DS: we then create another variable 'claim' to extract the claim from the ClaimsIdentity
                var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
                //DS: we assign the value of user id extracted from the ClaimsIdentity to 'ApplicationUserId
                paymentObj.UserID = claim.Value;

                //DS:get shopping cart from database if the ApplicationUserId is equal to user id from the claim identity
                //DS: and product id is equal tot the product id (FK) from shopping cart
                payment paymentFromDb = _unit.payment.GetFirstOrDefault(
                    u => u.UserID== paymentObj.UserID && u.TenancyID == paymentObj.TenancyID
                    , includeProperties: "tenancies" 
                    );
                //DS: the if statement conditions checks if there is no record. If true, a new record will be added to DB
                if (paymentFromDb == null)
                {
                    //LC: no records exists in database for that product for that user
                    _unit.payment.Add(paymentObj);
                }
                else
                {

                    paymentFromDb.Count += paymentObj.Count;
                    //LC: _unitOfWork.ShoppingCart.Update(cartFromDb);
                }
                _unit.Save();

                var count = _unit.payment
                    .GetAll(c => c.UserID == paymentObj.UserID)
                    .ToList().Count();

              /*  HttpContext.Session.SetObject(StaticDetails.SessionPayment, paymentObj);*/

                //DS: these lines were causing the errors

           /*     HttpContext.Session.SetInt32(StaticDetails.SessionPayment, count);
*/
                return RedirectToAction(nameof(Index));
            }
            else
            {
                var tenancyFromDb = _unit.Tenancies.
                        GetFirstOrDefault(u => u.Id == paymentObj.TenancyID, includeProperties: "PropertiesRS");
                payment paymentObject = new payment()
                {
                    tenancies = tenancyFromDb,
                    TenancyID = tenancyFromDb.Id
                };
                return View(paymentObject);
            }


        }
        public IActionResult ViewMyTenancy()
        {
            return View();
        }


    }
}