using BoldRealties.DAL;
using BoldRealties.DAL.Repository.IRepository;
using BoldRealties.Models;
using BoldRealties.BLL;
using BoldRealties.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace BoldRealties.Web.Controllers
{

    public class PropertiesRSController : Controller
    {
        private readonly IUnitOfWork _unit;
        private readonly IWebHostEnvironment _host;
        public PropertiesRSController(IUnitOfWork unit,  IWebHostEnvironment host) //implementation of connection string and table to retrieve data
        {
            _unit = unit;
            _host = host;
        }
        public IActionResult Index()
        {
            IEnumerable<PropertiesRS> objPropertiesList = _unit.Properties.GetAll();
            return View(objPropertiesList);
        }
        //GET
        public IActionResult Upsert(int? id)
        {
            PropertiesVM propertiesVM = new()
            {
                Properties = new(),
                TenancyList = _unit.Tenancies.GetAll().Select(i => new SelectListItem
                {
                    Text = i.managementType,
                    Value = i.Id.ToString()
                }),
                UserList = _unit.Users.GetAll().Select(i => new SelectListItem
                {
                    Text = i.firstName + i.lastName,
                    Value = i.Id.ToString()
                }),
            };

            if (id == null || id == 0)
            {
              
                return View(propertiesVM);
                    }
            else
            {
                propertiesVM.Properties = _unit.Properties.GetFirstOrDefault(u => u.ID == id);
                return View(propertiesVM);

              
            }


        }
        [HttpPost]
        [ValidateAntiForgeryToken] //to avoid the cross site request forgery
        public IActionResult Upsert(PropertiesVM propertiesVM, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _host.WebRootPath;
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString();
                    var ImgUploads = Path.Combine(wwwRootPath, @"images\Properties");
                    var extension = Path.GetExtension(file.FileName);

                    if (propertiesVM.Properties.imagePath != null)
                    {
                        var oldImagePath = Path.Combine(wwwRootPath, propertiesVM.Properties.imagePath.TrimStart('\\'));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    using (var fileStreams = new FileStream(Path.Combine(ImgUploads, fileName + extension), FileMode.Create))
                    {
                        file.CopyTo(fileStreams);
                    }
                    propertiesVM.Properties.imagePath = @"\images\Properties\" + fileName + extension;

                }
                if (propertiesVM.Properties.ID == 0)
                {
                    _unit.Properties.Add(propertiesVM.Properties);
                }
                else
                {
                    _unit.Properties.Update(propertiesVM.Properties);
                }
                _unit.Save();
                TempData["success"] = "Record added successfully";
                return RedirectToAction("Index");
            }
            return View(propertiesVM);
        }
        //POST
        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var obj = _unit.Properties.GetFirstOrDefault(u => u.ID == id);
            if (obj == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }

            var oldImagePath = Path.Combine(_host.WebRootPath, obj.imagePath.TrimStart('\\'));
            if (System.IO.File.Exists(oldImagePath))
            {
                System.IO.File.Delete(oldImagePath);
            }

            _unit.Properties.Remove(obj);
            _unit.Save();
            return Json(new { success = true, message = "Delete Successful" });

        }
        public IActionResult Details(int tenancyId)
        {
            payment Obj = new()
            {
                
                TenancyID = tenancyId,
                tenancies = _unit.Tenancies.GetFirstOrDefault(u => u.Id == tenancyId),
            };

            return View(Obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult Details(payment payment)
        {
            //this takes the identity to the user who is logged in!!! maybe use it for payment for tenant
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            payment.UserID = claim.Value;

            payment objFromDb = _unit.payment.GetFirstOrDefault(
                u => u.UserID == claim.Value && u.TenancyID == payment.TenancyID);


            if (objFromDb == null)
            {

                _unit.payment.Add(payment);
                _unit.Save();
                
                
            }
         

            return RedirectToAction(nameof(Index));
        }
    }
}
