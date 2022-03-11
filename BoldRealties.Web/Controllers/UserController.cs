using BoldRealties.DAL.Repository.IRepository;
using BoldRealties.Models;
using Microsoft.AspNetCore.Mvc;

namespace BoldRealties.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly IUnitOfWork _unit;
       
        public UserController(IUnitOfWork unit) //implementation of connection string and table to retrieve data
        {
            _unit = unit;
        }

        public IActionResult Index()
       {
           IEnumerable<Users> objUserList = _unit.Users.GetAll();
           return View(objUserList);
       }
       public IActionResult AddUser()
       {
           return View();
       }
       [HttpPost]
       [ValidateAntiForgeryToken] //to avoid the cross site request forgery
       public IActionResult AddUser(Users user)
       {
           if (ModelState.IsValid)
           {
               _unit.Users.Add(user);
               _unit.Save();
               TempData["success"] = "The record was added successfully!";
               return RedirectToAction("Index");
           }
           return View(user);
       }
       public IActionResult EditUser(int? ID)
       {
           if (ID == null || ID == 0)
           {
               return NotFound();
           }
           var UserFromDb = _unit.Properties.GetFirstOrDefault(x => x.ID == ID);
           if (UserFromDb == null)
           {
               return NotFound();
           }
           return View(UserFromDb);
       }
       [HttpPost]
       [ValidateAntiForgeryToken] //to avoid the cross site request forgery
       public IActionResult EditUser(Users user)
       {
           if (ModelState.IsValid)
           {
               _unit.Users.Update(user);
               _unit.Save();
               TempData["success"] = "The record was updated successfully!";
               return RedirectToAction("Index");
           }
           return View(user);
       }
       public IActionResult DeleteUser(int? ID)
       {
           if (ID == null || ID == 0)
           {
               return NotFound();
           }
      /*     var UserFromDb = _unit.Users.GetFirstOrDefault(x => x.ID == ID);
           if (UserFromDb == null)
           {
               return NotFound();
           }*/
           return View();
       }
       [HttpPost]
       [ValidateAntiForgeryToken] //to avoid the cross site request forgery
       public IActionResult DeleteUsers(int? ID)
       {
          /* var user = _unit.Users.GetFirstOrDefault(x => x.ID == ID);
           if (user == null)
           {
               return NotFound();
           }*/
/*
           _unit.Users.Remove(user);*/
           _unit.Save();
           TempData["success"] = "The record was deleted successfully!";
           return RedirectToAction("Index");
}
    }
}

