using BoldRealties.DAL.Repository.IRepository;
using BoldRealties.Models;
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
        public IActionResult Index()
        {
            IEnumerable<Enquiries> enquiriesList = _unit.Enquiries.GetAll();
            return View(enquiriesList);
        }
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
        public IActionResult EditEnquiry(int? ID)
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
        public IActionResult EditEnquiry(Enquiries enquiries)
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
        public IActionResult DeleteEnquiries(int? ID)
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
        public IActionResult DeleteUsers(int? ID)
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

    }
}
