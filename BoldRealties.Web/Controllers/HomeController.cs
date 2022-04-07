using BoldRealties.DAL.Repository.IRepository;
using BoldRealties.Models;
using BoldRealties.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PagedList;
using System.Diagnostics;
using System.Security.Claims;

namespace BoldRealties.Web.Controllers
{
   
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unit;

        public HomeController(ILogger<HomeController> logger, IUnitOfWork unit)
        {
            _logger = logger;
            _unit = unit;
        }

        public IActionResult Index(int? id, string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.CurrentFilter = searchString;

            var propertiesRs = from p in _unit.Properties.GetAll()
                               select p;
            if (!String.IsNullOrEmpty(searchString))
            {
                propertiesRs = propertiesRs.Where(p => p.propertyAddress.ToUpper().Contains(searchString.ToUpper())
                                       || p.marketPrice.ToString().Contains(searchString.ToUpper()));
            }
    
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(propertiesRs.ToPagedList(pageNumber, pageSize));
            var propertiesList = _unit.Properties.GetFirstOrDefault(i => i.ID == id);
            return View(propertiesList);
        

   
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]

     
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult ContactUs()
        {   
            return View();
        }
    

    }
}