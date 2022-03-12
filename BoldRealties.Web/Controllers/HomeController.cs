using BoldRealties.DAL.Repository.IRepository;
using BoldRealties.Models;
using BoldRealties.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]

        //most probably this should be moved in the user portal??
        public IActionResult Details (int tenancyId)
        {
            tenancies tenancyObj = _unit.Tenancies.GetFirstOrDefault(x => x.Id == tenancyId /* includeProperties: "tenancies, rentPrice"*/);
            return View(tenancyObj);
        }
        public IActionResult Details(tenancies tenancy)
        {
            //extract user ID from claimsIdentity
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            tenancy.UserID = claim.Value;

            _unit.Tenancies.Add(tenancy);
            _unit.Save();
            return RedirectToAction(nameof(Index));
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}