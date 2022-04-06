using Microsoft.AspNetCore.Mvc;
using BoldRealties.Models;
using BoldRealties.DAL.Repository.IRepository;

namespace BoldRealties.Web.Controllers
{

    public class officeAddressController : Controller
    {
        private readonly IUnitOfWork _unit;

        public officeAddressController(IUnitOfWork unit) //implementation of connection string and table to retrieve data
        {
            _unit = unit;
        }
        public IActionResult Index()
        {
            IEnumerable<officeAddress> addressesList = _unit.OfficeAddress.GetAll();
            return View(addressesList);
        }
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken] //to avoid the cross site request forgery

        public IActionResult Add(officeAddress office)
        {
            if (ModelState.IsValid)
            {
                _unit.OfficeAddress.Add(office);
                _unit.Save();
                TempData["success"] = "The record was added successfully!";
                return RedirectToAction("Index");
            }
            return View(office);
        }
        public IActionResult EditofficeAddress(int? ID)
        {
            if (ID == null || ID == 0)
            {
                return NotFound();
            }
            var officeFromDb = _unit.OfficeAddress.GetFirstOrDefault(x => x.ID == ID);
            if (officeFromDb == null)
            {
                return NotFound();
            }
            return View(officeFromDb);
        }
        [HttpPost]
        [ValidateAntiForgeryToken] //to avoid the cross site request forgery
        public IActionResult EditofficeAddress(officeAddress office)
        {
            if (ModelState.IsValid)
            {
                _unit.OfficeAddress.Add(office);
                _unit.Save();
                TempData["success"] = "The record was updated successfully!";
                return RedirectToAction("Index");
            }
            return View(office);
        }
      
    }
}
