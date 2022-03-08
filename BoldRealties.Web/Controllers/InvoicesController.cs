using BoldRealties.DAL.Repository.IRepository;
using BoldRealties.Models;
using Microsoft.AspNetCore.Mvc;

namespace BoldRealties.Web.Controllers
{
    public class InvoicesController : Controller
    {
        private readonly IUnitOfWork _unit;

        public InvoicesController(IUnitOfWork unit) //implementation of connection string and table to retrieve data
        {
            _unit = unit;
        }
        public IActionResult Index()
        {
            IEnumerable<Invoices> invoicesList = _unit.Invoices.GetAll();
            return View(invoicesList);
        }
        public IActionResult AddInvoice()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken] //to avoid the cross site request forgery

        public IActionResult AddInvoices(Invoices invoices)
        {
            if (ModelState.IsValid)
            {
                _unit.Invoices.Add(invoices);
                _unit.Save();
                TempData["success"] = "The record was added successfully!";
                return RedirectToAction("Index");
            }
            return View(invoices);
        }
      
    }
}
