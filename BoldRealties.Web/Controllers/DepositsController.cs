using Microsoft.AspNetCore.Mvc;
using BoldRealties.Models;
using BoldRealties.DAL.Repository.IRepository;

namespace BoldRealties.Web.Controllers
{
    public class DepositsController : Controller
    {
        public readonly IUnitOfWork _unit;
        public DepositsController(IUnitOfWork unit)
        {
            _unit = unit;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult AddDeposits()
        {
            return View();
        }
    }
}
