using BoldRealties.DAL;
using BoldRealties.DAL.Repository.IRepository;
using BoldRealties.Models;
using BoldRealties.Models.ViewModels;
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

        public IActionResult Index()
        {
            IEnumerable<jobs> objMJList = _unit.maintenanceJobs.GetAll();
            return View(objMJList);
        }
        public IActionResult Upsert(int? ID)
        {
            // Keeping this for later as I want to get the identity of the user later on.

            /*
               var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            jobs objFromDb = _unit.maintenanceJobs.GetFirstOrDefault(
                u => u.UserID == claim.Value && u.tenanciesID == mJobVM.mJobVM.tenanciesID);
          
            */

            //the code below is to display users, properties and tenancies in the dropdown
            //this will be changed at a later stage as I would like to have search functionality instead of dropdown but still didn't decided
            mJobsVM mJobVM = new()
            {
                mJobVM = new(),
                TenancyList = _unit.Tenancies.GetAll().Select(x => new SelectListItem
                {
                    Text = x.managementType,
                    Value = x.Id.ToString()
                }),
              
                UserList = _unit.Users.GetAll().Select(x => new SelectListItem
                {
                    Text = x.firstName + x.lastName,
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
                    var fileUploads = Path.Combine(wwwRootPath, @"files\jobs");
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
