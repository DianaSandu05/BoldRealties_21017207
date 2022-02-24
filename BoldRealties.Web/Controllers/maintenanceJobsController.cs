using BoldRealties.DAL;
using BoldRealties.DAL.Repository.IRepository;
using BoldRealties.Models;
using Microsoft.AspNetCore.Mvc;

namespace BoldRealties.Web.Controllers
{
    public class maintenanceJobsController : Controller
    {
        
        private readonly IUnitOfWork _unit;
        public maintenanceJobsController(IUnitOfWork unit) //implementation of connection string and table to retrieve data
        {
            _unit = unit;
        }
      
        public IActionResult Index()
        {
            IEnumerable<jobs> objMJList = _unit.maintenanceJobs.GetAll();
            return View(objMJList);
        }
        public IActionResult AddMJ()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken] //to avoid the cross site request forgery
        public IActionResult AddMJ(jobs maintenanceJobs)
        {
           if(ModelState.IsValid)
            {
                _unit.maintenanceJobs.Add(maintenanceJobs);
                _unit.Save();
                TempData["success"] = "The record was added successfully!";
                return RedirectToAction("Index");
            }
            return View(maintenanceJobs);
        }
        public IActionResult EditMJ(int? ID)
        {
            if(ID==null || ID==0)
            {
                return NotFound();
            }
            var objFromDb = _unit.maintenanceJobs.GetFirstOrDefault(x=>x.ID==ID);
            if(objFromDb == null)
            {
                return NotFound();
            }
            return View(objFromDb);
        }
        [HttpPost]
        [ValidateAntiForgeryToken] //to avoid the cross site request forgery
        public IActionResult EditMJ(jobs maintenanceJobs)
        {
            if (ModelState.IsValid)
            {
                _unit.maintenanceJobs.Update(maintenanceJobs);
                _unit.Save();
                TempData["success"] = "The record was updated successfully!";
                return RedirectToAction("Index");
            }
            return View(maintenanceJobs);
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
