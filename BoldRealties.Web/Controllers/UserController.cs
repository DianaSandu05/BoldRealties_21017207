using BoldRealties.BLL;
using BoldRealties.DAL.Repository.IRepository;
using BoldRealties.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BoldRealties.Web.Controllers
{

    public class UserController : Controller
    {
        private readonly IUnitOfWork _unit;
       
        public UserController(IUnitOfWork unit) //implementation of connection string and table to retrieve data
        {
            _unit = unit;
        }
        [Authorize(Roles = StaticDetails.Role_Admin)]
        //function to display the list with all users
        public IActionResult Index()
       {
           IEnumerable<Users> objUserList = _unit.Users.GetAll();
           return View(objUserList);
       }
    }
}

