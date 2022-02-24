using Microsoft.AspNetCore.Mvc;
using BoldRealties.Models;
using BoldRealties.DAL.Repository.IRepository;

namespace BoldRealties.Web.Controllers
{
    public class ViewingsController : Controller
    {
        public readonly IUnitOfWork _unit;
        public ViewingsController(IUnitOfWork unit)
        {
            _unit = unit;
        }
        public IActionResult Index()
        {
            IEnumerable<Viewings> viewingList = _unit.viewings.GetAll();
            return View();
        }
        public IActionResult AddViewings()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddViewings(Viewings viewing)
        {
            if(ModelState.IsValid)
            {
                _unit.viewings.Add(viewing);
                _unit.Save(); 
                TempData["success"] = "The record was added successfully!";
                return RedirectToAction("Index");
            }
            return View(viewing);
        }
        public IActionResult EditViewing(int? ID)
        {
            if(ID== null || ID == 0)
            {
                return NotFound();
            }
            var objFromDb = _unit.viewings.GetFirstOrDefault(x => x.Id == ID);
            if(objFromDb == null)
            {
                return NotFound();
            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditViewing(Viewings viewing)
        {
            if(ModelState.IsValid)
            {
                _unit.viewings.Update(viewing);
                _unit.Save();
                TempData["success"] = "The record was updated successfully!";
                return RedirectToAction("Index");
            }
            return View(viewing);
        }
        public IActionResult DeleteViewing(int? ID)
        {
            if(ID == null || ID == 0)
            {
                return NotFound();
            }
            var objFromDb = _unit.viewings.GetFirstOrDefault(x => x.Id==ID);
            if(objFromDb == null)
            {
                return NotFound();
            }
            return View();
        }
        public IActionResult DeleteViewing(Viewings viewing)
        {
            if(ModelState.IsValid)
            {
                _unit.viewings.Remove(viewing);
                _unit.Save();
                TempData["success"] = "The record was deleted successfully";
                return RedirectToAction("Index");
            }
            return View(viewing);
        }
    }
}
