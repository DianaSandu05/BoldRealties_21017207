using Microsoft.AspNetCore.Mvc;
using BoldRealties.Models;
using BoldRealties.DAL.Repository.IRepository;

namespace BoldRealties.Web.Controllers
{
    public class paymentController : Controller
    {
        private readonly IUnitOfWork _unit;
        public paymentController(IUnitOfWork unit) //implementation of connection string and table to retrieve data
        {
            _unit = unit;
        }
        public IActionResult Index()
        {
            IEnumerable<payment> objpaymentList = _unit.payment.GetAll();
            return View(objpaymentList);
        }
    }
}
