using Microsoft.AspNetCore.Mvc;
using BoldRealties.Models;
using BoldRealties.DAL.Repository.IRepository;
using BoldRealties.Models.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using BoldRealties.BLL;

namespace BoldRealties.Web.Controllers
{
    [Authorize(Roles = StaticDetails.Role_Admin)]
    public class ViewingsController : Controller
    {
        public readonly IUnitOfWork _unit;
        private readonly IWebHostEnvironment _host;
        public ViewingsController(IUnitOfWork unit, IWebHostEnvironment host)
        {
            _unit = unit;
            _host = host;
        }
        //function to list all records in Admin portal
       
        public IActionResult Index()
        {
            IEnumerable<Viewings> viewingList = _unit.viewings.GetAll();
            return View(viewingList);
        }
       
        public IActionResult Upsert(int? id)
        {
            ViewingsVM viewingsVM = new()
            {
                Viewings = new(),
                PropertiesList = _unit.Properties.GetAll().Select(i => new SelectListItem
                {
                    Text = i.propertyAddress,
                    Value = i.ID.ToString()
                }),
                UserList = _unit.Users.GetAll().Select(i => new SelectListItem
                {
                    Text = i.firstName + i.lastName,
                
                }),
                //get the aplicants full name for the viewing
                AplicantsList = _unit.Enquiries.GetAll().Select(i => new SelectListItem
                {
                    Text = i.NameSurname,
                    Value = i.ID.ToString()
                }),
            };

            if (id == null || id == 0)
            {

                return View(viewingsVM);
            }
            else
            {
                viewingsVM.Viewings= _unit.viewings.GetFirstOrDefault(u => u.Id == id);
                return View(viewingsVM);


            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken] //to avoid the cross site request forgery
        public IActionResult Upsert(ViewingsVM viewingsVM)
        {
            if (ModelState.IsValid)
            {
                if (viewingsVM.Viewings.Id == 0)
                {
                    _unit.viewings.Add(viewingsVM.Viewings);
                }
                else
                {
                    _unit.viewings.Update(viewingsVM.Viewings);
                }
                _unit.Save();
                TempData["success"] = "Record added successfully";
                return RedirectToAction("Index");
            }
            return View(viewingsVM);
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
