using Microsoft.AspNetCore.Mvc;
using BoldRealties.DAL.Repository.IRepository;
using BoldRealties.Models;

namespace BoldRealties.Web.Controllers
{
    public class baReportsController : Controller
    {
        private readonly IUnitOfWork _unit;

        public baReportsController(IUnitOfWork unit) //implementation of connection string and table to retrieve data
        {
            _unit = unit;
        }
        public IActionResult Index()
        {
            IEnumerable<BA_Reports> reportsList = _unit.BA_Reports.GetAll();
            return View(reportsList);
        }
        public IActionResult AddReports()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken] //to avoid the cross site request forgery

        public IActionResult AddReports(BA_Reports ba_Reports)
        {
            if (ModelState.IsValid)
            {
                _unit.BA_Reports.Add(ba_Reports);
                _unit.Save();
                TempData["success"] = "The record was added successfully!";
                return RedirectToAction("Index");
            }
            return View(ba_Reports);
        }
      
    }
}
