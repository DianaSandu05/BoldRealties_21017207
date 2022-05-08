using Microsoft.AspNetCore.Mvc;
using BoldRealties.Models;
using BoldRealties.DAL.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using BoldRealties.BLL;
using BoldRealties.Models.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BoldRealties.Web.Controllers
{
    public class DepositsController : Controller
    {
        public readonly IUnitOfWork _unit;
        private readonly IWebHostEnvironment _webHost;
        public DepositsController(IUnitOfWork unit, IWebHostEnvironment webHost)
        {
            _unit = unit;
            _webHost = webHost;
        }
        [Authorize(Roles = StaticDetails.Role_Admin)]
        //displays a list the deposits 
        public IActionResult Index()
        {
            IEnumerable<Deposits> objDepositsList = _unit.Deposits.GetAll();
            return View(objDepositsList);
        }
        [Authorize(Roles = StaticDetails.Role_Admin)]
        public IActionResult Upsert(int? ID)
        {
            //the code below is to display users, properties and tenancies in the dropdown
            //this will be changed at a later stage as I would like to have search functionality instead of dropdown but still didn't decided
            DepositsVM depositsVM = new()
            {
                Deposits = new(),
                TenancyList = _unit.Tenancies.GetAll().Select(x => new SelectListItem
                {
                    Text = x.managementType,
                    Value = x.Id.ToString()
                }),
                UserList = _unit.Users.GetAll().Select(x => new SelectListItem
                {
                    Text = x.Id,
                    Value = x.Id.ToString()
                }),
            };

            if (ID == null || ID == 0)
            {
                return View(depositsVM);
            }
            else
            {
                depositsVM.Deposits = _unit.Deposits.GetFirstOrDefault(u => u.Id == ID);
                return View(depositsVM);
            }
        }
        [Authorize(Roles = StaticDetails.Role_Admin)]
        [HttpPost]
        [ValidateAntiForgeryToken] //to avoid the cross site request forgery
        public IActionResult Upsert(DepositsVM depositsVM, IFormFile? file)
        {
            //validates the input
            if (ModelState.IsValid)
            {
                //create/update image
                string wwwRootPath = _webHost.WebRootPath;
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString();
                    var fileUploads = Path.Combine(wwwRootPath, @"files\tenancy");
                    var extension = Path.GetExtension(file.FileName);

                    if (depositsVM.Deposits.FilePath != null)
                    {
                        var oldImagePath = Path.Combine(wwwRootPath, depositsVM.Deposits.FilePath.TrimStart('\\'));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    using (var fileStreams = new FileStream(Path.Combine(fileUploads, fileName + extension), FileMode.Create))
                    {
                        file.CopyTo(fileStreams);
                    }
                    depositsVM.Deposits.FilePath = @"\files\tenancy\" + fileName + extension;

                }
                if (depositsVM.Deposits.Id == 0)
                {
                    //add invoice
                    _unit.Deposits.Add(depositsVM.Deposits);
                }
                else
                {
                    //update invoice
                    _unit.Deposits.Update(depositsVM.Deposits);
                }
                _unit.Save();
                TempData["success"] = "Record added successfully";
                return RedirectToAction("Index");
            }
            return View(depositsVM);

        }
    }
}
