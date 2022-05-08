using BoldRealties.BLL;
using BoldRealties.DAL.Repository.IRepository;
using BoldRealties.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BoldRealties.Web.Controllers
{

    public class EnquiriesController : Controller
    {
        private readonly IUnitOfWork _unit;

        public EnquiriesController(IUnitOfWork unit) //implementation of connection string and table to retrieve data
        {
            _unit = unit;
        }
        [Authorize(Roles = StaticDetails.Role_Admin)]
        //function to display the enquiries list in admin portal
        public IActionResult Index()
        {
            IEnumerable<Enquiries> enquiriesList = _unit.Enquiries.GetAll();
            return View(enquiriesList);
        }
        //function to send enquiries in the home page - for unauthorized users
        public IActionResult AddEnquiry()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken] //to avoid the cross site request forgery

        public IActionResult AddEnquiry(Enquiries enquiries)
        {
            if (ModelState.IsValid)
            {
                _unit.Enquiries.Add(enquiries);
                _unit.Save();
                TempData["success"] = "The record was added successfully!";
                return RedirectToAction("Index");
            }
            return View(enquiries);
        }
        [Authorize(Roles = StaticDetails.Role_Admin)]
        //function to edit enquiries in the admin portal
        public IActionResult Edit(int? ID) 
        {
            if (ID == null || ID == 0)
            {
                return NotFound();
            }
            var EnquiryFromDb = _unit.Enquiries.GetFirstOrDefault(x => x.ID == ID);
            if (EnquiryFromDb == null)
            {
                return NotFound();
            }
            return View(EnquiryFromDb);
        }
        [HttpPost]
        [ValidateAntiForgeryToken] //to avoid the cross site request forgery
        [Authorize(Roles = StaticDetails.Role_Admin)]
        public IActionResult Edit(Enquiries enquiries)
        {
            if (ModelState.IsValid)
            {
                _unit.Enquiries.Add(enquiries);
                _unit.Save();
                TempData["success"] = "The record was updated successfully!";
                return RedirectToAction("Index");
            }
            return View(enquiries);
        }
        [Authorize(Roles = StaticDetails.Role_Admin)]
        //delete function for enquiries in the admin portal
        public IActionResult Delete(int? ID)
        {
            if (ID == null || ID == 0)
            {
                return NotFound();
            }
            var EnquiryFromDb = _unit.Enquiries.GetFirstOrDefault(x => x.ID == ID);
            if (EnquiryFromDb == null)
            {
                return NotFound();
            }
            return View(EnquiryFromDb);
        }
        //post
        [HttpPost]
        [ValidateAntiForgeryToken] //to avoid the cross site request forgery
        public IActionResult DeleteEnquiry(int? ID)
        {
            var enquiry = _unit.Enquiries.GetFirstOrDefault(x => x.ID == ID);
            if (enquiry == null)
            {
                return NotFound();
            }

            _unit.Enquiries.Remove(enquiry);
            _unit.Save();
            TempData["success"] = "The record was deleted successfully!";
            return RedirectToAction("Index");
        }
        [Authorize(Roles = StaticDetails.Role_Admin)]
        //function to see the details of the enquiry/applicant
        public IActionResult Details(int? ID)
        {
            Enquiries enquiries = new();
            enquiries = _unit.Enquiries.GetFirstOrDefault(u => u.ID == ID);

            return View(enquiries);
        }
     
        
    }
}
