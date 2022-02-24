using Microsoft.AspNetCore.Mvc;
using BoldRealties.Models;
using BoldRealties.DAL.Repository.IRepository;

namespace BoldRealties.Web.Controllers
{
    public class paymentController : Controller
    {
        private readonly IUnitOfWork _unit;
        public IActionResult Index()
        {
            return View();
        }
    }
}
