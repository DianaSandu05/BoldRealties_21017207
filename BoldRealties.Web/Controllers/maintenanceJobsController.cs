using BoldRealties.BLL;
using BoldRealties.DAL;
using BoldRealties.DAL.Repository.IRepository;
using BoldRealties.Models;
using BoldRealties.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

namespace BoldRealties.Web.Controllers
{

    public class maintenanceJobsController : Controller
    {

        private readonly IUnitOfWork _unit;
        private readonly IWebHostEnvironment _webHost;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public maintenanceJobsController(IUnitOfWork unit, IWebHostEnvironment webHost, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager) //implementation of connection string and table to retrieve data
        {
            _unit = unit;
            _webHost = webHost;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        [Authorize(Roles = StaticDetails.Role_Admin)]
        public IActionResult Index()
        {
            IEnumerable<jobs> objMJList = _unit.maintenanceJobs.GetAll();
            return View(objMJList);
        }
        //function to list all the invoices for a property for a specific tenancy for logged in landlord
        public IActionResult maintenanceJobsLandlord(int? Id)
        {
            IEnumerable<jobs> obj = _unit.maintenanceJobs.GetAll().Where(u => u.tenanciesID == Id);
            return View(obj);
        }
        [Authorize(Roles = StaticDetails.Role_Tenant)]
        //this method gets the list with maintenance work for the tenant
        public IActionResult maintenanceTenant(int? id)
        {
            IEnumerable<jobs> objMJList = _unit.maintenanceJobs.GetAll(u => u.tenanciesID == id);

            return View(objMJList);
        }
        //this functions retrieves the list with quoted maintenance jobs by a subcontractor
        // the user ID will be equal to subcontractor Id which is compared to the claim value to retrieve the list
        public IActionResult QuotedMaintenanceJobs()
        {
            //DS: claim identity of the logged in user
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            
            IEnumerable<jobs> objMJList = _unit.maintenanceJobs.GetAll(u => u.UserID == claim.Value);

            return View(objMJList);
        }
        public IActionResult Upsert(int? ID)
        {
            //the code below is to display users, properties and tenancies in the dropdown
            //this will be changed at a later stage as I would like to have search functionality instead of dropdown but still didn't decided
            mJobsVM mJobVM = new()
            {
                mJobVM = new(),
                TenancyList = _unit.Tenancies.GetAll().Select(x => new SelectListItem
                {
                    Text = x.Id.ToString(),
                    Value = x.Id.ToString()
                }),
              
                UserList = _unit.Users.GetAll().Select(x => new SelectListItem
                {
                    Text = x.firstName + x.lastName,
                    Value = x.Id.ToString()
                }),
                InvoiceList = _unit.Invoices.GetAll().Select(x => new SelectListItem
                {
                    Text = x.invoice_No.ToString(),
                    Value = x.Id.ToString()
                }),


            };
            
            if (ID == null || ID == 0)
            {
                return View(mJobVM);
            }
            else
            {
               mJobVM.mJobVM = _unit.maintenanceJobs.GetFirstOrDefault(u => u.ID == ID);
                return View(mJobVM);


            }

        }
        [HttpPost]
        [ValidateAntiForgeryToken] //to avoid the cross site request forgery
        public IActionResult Upsert(mJobsVM jobsVM, IFormFile? file)
        { //this takes the identity to the user who is logged in!!! maybe use it for payment for tenant
            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHost.WebRootPath;
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString();
                    var fileUploads = Path.Combine(wwwRootPath, @"files\tenancy");
                    var extension = Path.GetExtension(file.FileName);

                    if (jobsVM.mJobVM.filePath != null)
                    {
                        var oldImagePath = Path.Combine(wwwRootPath, jobsVM.mJobVM.filePath.TrimStart('\\'));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }
                    using (var fileStreams = new FileStream(Path.Combine(fileUploads, fileName + extension), FileMode.Create))
                    {
                        file.CopyTo(fileStreams);
                    }
                    jobsVM.mJobVM.filePath = @"\files\jobs\" + fileName + extension;

                }
                if (jobsVM.mJobVM.ID == 0)
                {
                    _unit.maintenanceJobs.Add(jobsVM.mJobVM);
                }
                else
                {
                    _unit.maintenanceJobs.Update(jobsVM.mJobVM);
                }
                _unit.Save();
                TempData["success"] = "Record added successfully";
                return RedirectToAction("Index");
            }
            return View(jobsVM);
      
        }
            public IActionResult DeleteMJ(int? ID)
        {
            if (ID == null || ID == 0)
            {
                return NotFound();
            }
            var objFromDb = _unit.maintenanceJobs.GetFirstOrDefault(x => x.ID == ID);
            if (objFromDb == null)
            {
                return NotFound();
            }
            return View(objFromDb);
        }
        [HttpPost]
        [ValidateAntiForgeryToken] //to avoid the cross site request forgery
        public IActionResult DeleteMJs(int? ID)
        {
            var maintenanceJobs = _unit.maintenanceJobs.GetFirstOrDefault(x=>x.ID== ID);
            if (maintenanceJobs == null)
            {
                return NotFound();
            }

            _unit.maintenanceJobs.Remove(maintenanceJobs);
                _unit.Save();
            TempData["success"] = "The record was deleted successfully!";
                return RedirectToAction("Index");
        }
        
        
    }
}
