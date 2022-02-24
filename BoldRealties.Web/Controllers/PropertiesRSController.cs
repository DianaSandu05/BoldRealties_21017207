using BoldRealties.DAL;
using BoldRealties.DAL.Repository.IRepository;
using BoldRealties.Models;
using Microsoft.AspNetCore.Mvc;

namespace BoldRealties.Web.Controllers
{
    public class PropertiesRSController : Controller
    {
        private readonly IUnitOfWork _unit;
        public PropertiesRSController(IUnitOfWork unit) //implementation of connection string and table to retrieve data
        {
            _unit = unit;
        }
        public IActionResult Index()
        {
            IEnumerable<PropertiesRS> objPropertiesList = _unit.Properties.GetAll();
            return View(objPropertiesList);
        }
        public IActionResult AddProperties()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken] //to avoid the cross site request forgery
        public IActionResult AddProperties(PropertiesRS property)
        {
           if(ModelState.IsValid)
            {
                _unit.Properties.Add(property);
                _unit.Save();
                TempData["success"] = "The record was added successfully!";
                return RedirectToAction("Index");
            }
            return View(property);
        }
        public IActionResult EditProperties(int? ID)
        {
            if(ID==null || ID==0)
            {
                return NotFound();
            }
            var PropertiesFromDb = _unit.Properties.GetFirstOrDefault(x=>x.ID==ID);
            if(PropertiesFromDb == null)
            {
                return NotFound();
            }
            return View(PropertiesFromDb);
        }
        [HttpPost]
        [ValidateAntiForgeryToken] //to avoid the cross site request forgery
        public IActionResult EditProperties(PropertiesRS property)
        {
            if (ModelState.IsValid)
            {
                _unit.Properties.Update(property);
                _unit.Save();
                TempData["success"] = "The record was updated successfully!";
                return RedirectToAction("Index");
            }
            return View(property);
        }
        public IActionResult DeleteProperties(int? ID)
        {
            if (ID == null || ID == 0)
            {
                return NotFound();
            }
            var PropertiesFromDb = _unit.Properties.GetFirstOrDefault(x => x.ID == ID);
            if (PropertiesFromDb == null)
            {
                return NotFound();
            }
            return View(PropertiesFromDb);
        }
        [HttpPost]
        [ValidateAntiForgeryToken] //to avoid the cross site request forgery
        public IActionResult DeleteProperty(int? ID)
        {
            var Property = _unit.Properties.GetFirstOrDefault(x=>x.ID== ID);
            if (Property == null)
            {
                return NotFound();
            }

            _unit.Properties.Remove(Property);
                _unit.Save();
            TempData["success"] = "The record was deleted successfully!";
                return RedirectToAction("Index");
           
            
        }
    }
}
