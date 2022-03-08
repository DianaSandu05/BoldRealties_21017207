using Microsoft.AspNetCore.Mvc;
using BoldRealties.Models;
using BoldRealties.DAL.Repository.IRepository;

namespace BoldRealties.Web.Controllers
{
    public class AccountsController : Controller
    {
        public readonly IUnitOfWork _unit;
        public AccountsController(IUnitOfWork unit)
        {

            _unit = unit;
        }
        public IActionResult Index()
        {
            IEnumerable<Accounts> objAccountsList = _unit.Accounts.GetAll();
            return View(objAccountsList);
        }
        public IActionResult AddAccountsRecord()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddAccountsRecord(Accounts accounts)
        {
            if(ModelState.IsValid)
            {
                _unit.Accounts.Add(accounts);
                _unit.Save();
                TempData["success"] = "The record was added successfully!";
                return RedirectToAction("Index");
            }
            return View(accounts);
        }
        public IActionResult UpdateAccountsRecord(int? ID)
        {
            if(ID == null || ID == 0)
            {
                return NotFound();
            }
            var objFromDb = _unit.Accounts.GetFirstOrDefault(x =>x.ID == ID);
            if(objFromDb == null)
            {
                return NotFound();
            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateAccountsRecord(Accounts accounts)
        {
            if(ModelState.IsValid)
            {
                _unit.Accounts.Update(accounts);
                _unit.Save();
                TempData["success"] = "The record was updated successfully!";
                return RedirectToAction("Index");
            }
            return View(accounts);
        }
        public IActionResult DeleteAccountsRecords(int? ID)
        {
            if(ID == null || ID == 0)
            {
                return NotFound();
            }
            var objFromDb = _unit.Accounts.GetFirstOrDefault(x=> x.ID == ID);
            if(objFromDb == null)
            {
                return NotFound();
            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteAccountsRecord(Accounts accounts)
        {
            if(ModelState.IsValid)
            {
                _unit.Accounts.Remove(accounts);
                _unit.Save();
                TempData["success"] = "The record was deleted successfully!";
                return RedirectToAction("Index");
            }
            return View(accounts);
        }

    }
}
