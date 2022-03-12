using BoldRealties.DAL.Repository.IRepository;
using BoldRealties.Models;
using BoldRealties.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BoldRealties.Web.Controllers
{
    public class TenancyController : Controller
    {
        private readonly IUnitOfWork _unit;
        private readonly IWebHostEnvironment _webHost;
     
        public TenancyController(IUnitOfWork unit, IWebHostEnvironment webHost) //implementation of connection string and table to retrieve data
        {
            _unit = unit;
            _webHost = webHost;
        }
        public IActionResult Index()
        {
            IEnumerable<tenancies> objTenanciesList = _unit.Tenancies.GetAll();
            return View(objTenanciesList);
        }
        public IActionResult Upsert(int? ID)
        {

            TenancyViewModel tvm = new()
            {
                tenancy = new(),
                 PropertiesList = _unit.Properties.GetAll().Select(x => new SelectListItem
                {
                    Text = x.propertyAddress,
                    Value = x.ID.ToString()
                }),
                UserList = _unit.Users.GetAll().Select(x => new SelectListItem
                {
                    Text = x.firstName + x.lastName,
                 /*   Value = x.ID.ToString()*/
                }),
            };
          if(ID==null || ID ==0)
            {
                return View(tvm);
            }
            else
            {
                if (ID == null || ID == 0)
                {
                    return NotFound();
                }
                var TenancyFromDb = _unit.Tenancies.GetFirstOrDefault(x => x.Id == ID);
                if (TenancyFromDb == null)
                {
                    return NotFound();
                }
                return View(TenancyFromDb);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken] //to avoid the cross site request forgery
        public IActionResult Upsert(TenancyViewModel tenancies, IFormFile? file)
        {
            //add tenancy it's not working at the moment because of the code below. Comment it out and replace the input from Upsert/Tenancy
            //with the following code  <div class="mb-3  col-6">
           //< label asp -for= "tenancy.imagePath" > Upload Images </ label >
            //  < input type = "file"  id = "uploadBox" name = "img" asp -for= "tenancy.imagePath" class="form-control" />    
         // </div>
      /*     if (ModelState.IsValid)
            {
                string wwwrootPath = _webHost.WebRootPath;
                if(file!=null)
                {
                   
                    string fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(wwwrootPath, @"files\tenancy");
                    var extension = Path.GetExtension(file.FileName);
 
                    using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                    {
                        file.CopyTo(fileStreams);
                    }    
                    tenancies.tenancy.filePath = @"\files\tenancy" + fileName + extension;
                }
              
                 _unit.Tenancies.Add(tenancies.tenancy);
                _unit.Save();
                TempData["success"] = "The record was added successfully!";
                return RedirectToAction("Index");
            }
      */
            return View(tenancies);
        }
       
        public IActionResult DeleteTenancies(int? ID)
        {
            if (ID == null || ID == 0)
            {
                return NotFound();
            }
            var TenancyFromDb = _unit.Tenancies.GetFirstOrDefault(x => x.Id == ID);
            if (TenancyFromDb == null)
            {
                return NotFound();
            }
            return View(TenancyFromDb);
        }
        [HttpPost]
        [ValidateAntiForgeryToken] //to avoid the cross site request forgery
        public IActionResult DeleteTenancy(int? ID)
        {
            var tenancies = _unit.Tenancies.GetFirstOrDefault(x => x.Id == ID);
            if (tenancies == null)
            {
                return NotFound();
            }

            _unit.Tenancies.Remove(tenancies);
            _unit.Save();
            TempData["success"] = "The record was deleted successfully!";
            return RedirectToAction("Index");


        }
       public IActionResult ViewMyTenancy()
        {
            return View();
        }
    }
}
