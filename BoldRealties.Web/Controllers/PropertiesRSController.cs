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
        [Authorize(Roles = StaticDetails.Role_Admin)]
        //function to list all properties in admin portal
        public IActionResult Index()
        {
            IEnumerable<PropertiesRS> objPropertiesList = _unit.Properties.GetAll();
            return View(objPropertiesList);
        }
        [Authorize(Roles = StaticDetails.Role_Admin)]
        //function to update and/or insert properties 
        //GET
        public IActionResult Upsert(int? id)
        {
            PropertiesVM propertiesVM = new()
            {
                Properties = new(),
                TenancyList = _unit.Tenancies.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Id.ToString(),
                    Value = i.Id.ToString()
                }),
                UserList = _unit.Users.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Id.ToString(),
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
        [Authorize(Roles = StaticDetails.Role_Admin)]
        //function to update and/or delete
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
           

        //function to display the properties on the Properties for unauthentificated users
        public IActionResult PropertiesList()
        {
            IEnumerable<PropertiesRS> objPropertiesList = _unit.Properties.GetAll();
            return View(objPropertiesList);
        }

        //                                   Aside comments
        //details function in home controller  -- details of the property for the applicants in home controller
        //details function in properties controller -- details of the property for admin in this controller -- upsert

        [Authorize(Roles = StaticDetails.Role_Landlord)]

        //get the properties list for the logged in landlord
        public IActionResult LandlordProperty()
        {

            //DS: we create a variable 'claimsIdentity' and we initialize it with the name identifier
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            //DS: we then create another variable 'claim' to extract the claim from the ClaimsIdentity
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);


            IEnumerable<PropertiesRS> obj = _unit.Properties.GetAll().Where(u => u.UserId == claim.Value);
            return View(obj);
        }
        [Authorize(Roles = StaticDetails.Role_Landlord)]
        //get property details for each property the landlord has -- this function is called from LandlordProperty page
        //it passes the property and compares with the id from the db and displays the record for that property
        public IActionResult PropertyDetailsLandlord(int? ID)
        {
            PropertiesVM propertiesVM = new()
            {
                Properties = new(),
                TenancyList = _unit.Tenancies.GetAll().Select(x => new SelectListItem
                {
                    Text = x.Id.ToString(),
                    /*Value = x.ID.ToString()*/
                }),
                UserList = _unit.Users.GetAll().Select(x => new SelectListItem
                {
                    Text = x.firstName + x.lastName,
                    /*   Value = x.ID.ToString()*/
                }),
            };
            if (ID == null || ID == 0)
            {
                return View(propertiesVM);
            }
            else
            {
                propertiesVM.Properties = _unit.Properties.GetFirstOrDefault(u => u.ID == ID);
                return View(propertiesVM);


            }
        }
        /* The function TenancyDetails() gets the details of the property
          then the landlord can get info about the tenancy for that property.
          In the PropertyDetailsLandlord the tenancyId(FK) of the property is displayed
         when the button to redirect the landlord to TenancyDetails page is clicked, 
        the tenancyId(FK) is compared to the all any id from tenancies tables and if it maches, the record is displayed
        */
        [Authorize(Roles = StaticDetails.Role_Landlord)]
        public IActionResult TenancyDetails(int? ID)
        {

            //DS: we create a variable 'claimsIdentity' and we initialize it with the name identifier
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            //DS: we then create another variable 'claim' to extract the claim from the ClaimsIdentity
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);


            IEnumerable<tenancies> objTenanciesList = _unit.Tenancies.GetAll().Where(u => u.PropertyID == ID);
            return View(objTenanciesList);
        }

      
       
    }
}
