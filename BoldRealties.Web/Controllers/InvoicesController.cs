using BoldRealties.BLL;
using BoldRealties.DAL.Repository.IRepository;
using BoldRealties.Models;
using BoldRealties.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BoldRealties.Web.Controllers
{

    public class InvoicesController : Controller
    {
        private readonly IUnitOfWork _unit;
        private readonly IWebHostEnvironment _webHost;


        public InvoicesController(IUnitOfWork unit, IWebHostEnvironment webHost) //implementation of connection string and table to retrieve data
        {
            _unit = unit;
            _webHost = webHost;

        }
        //function to show a list with invoices
        [Authorize(Roles = StaticDetails.Role_Admin)]

        public IActionResult Index()
        {
            IEnumerable<Invoices> invoicesList = _unit.Invoices.GetAll();
            return View(invoicesList);
        }
        [Authorize(Roles = StaticDetails.Role_Landlord)]
        //function to list all the invoices for a property for a specific tenancy for logged in landlord
        public IActionResult InvoicesListLandlord(int? Id)
        {
            IEnumerable<Invoices> obj = _unit.Invoices.GetAll().Where(u => u.TenancyID == Id);
            return View(obj);

        }
        //function to update and/or create invoices 
        [Authorize(Roles = StaticDetails.Role_Admin)]
        public IActionResult Upsert(int? ID)
        {
            //the code below is to display users, properties and tenancies in the dropdown
            //this will be changed at a later stage as I would like to have search functionality instead of dropdown but still didn't decided
            InvoicesVM invoicesVM = new()
            {
                invoices = new(),
                TenancyList = _unit.Tenancies.GetAll().Select(x => new SelectListItem
                {
                    Text = x.managementType,
                    Value = x.Id.ToString()
                }),
                PropertyList = _unit.Properties.GetAll().Select(x => new SelectListItem
                {
                    Text = x.propertyAddress,
                    Value = x.ID.ToString()
                }),
            };

            if (ID == null || ID == 0)
            {
                return View(invoicesVM);
            }
            else
            {
                invoicesVM.invoices = _unit.Invoices.GetFirstOrDefault(u => u.Id == ID);
                return View(invoicesVM);
            }
        }
        [Authorize(Roles = StaticDetails.Role_Admin)]
        [HttpPost]
        [ValidateAntiForgeryToken] //to avoid the cross site request forgery
        public IActionResult Upsert(InvoicesVM invoicesVM, IFormFile? file)
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

                    if (invoicesVM.invoices.filePath != null)
                    {
                        var oldImagePath = Path.Combine(wwwRootPath, invoicesVM.invoices.filePath.TrimStart('\\'));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    using (var fileStreams = new FileStream(Path.Combine(fileUploads, fileName + extension), FileMode.Create))
                    {
                        file.CopyTo(fileStreams);
                    }
                    invoicesVM.invoices.filePath = @"\files\tenancy\" + fileName + extension;

                }
                if (invoicesVM.invoices.Id == 0)
                {
                    //add invoice
                    _unit.Invoices.Add(invoicesVM.invoices);
                }
                else
                {
                    //update invoice
                    _unit.Invoices.Update(invoicesVM.invoices);
                }
                _unit.Save();
                TempData["success"] = "Record added successfully";
                return RedirectToAction("Index");
            }
            return View(invoicesVM);

        }
        public IActionResult Delete(int? ID)
        {
            if (ID == null || ID == 0)
            {
                return NotFound();
            }
            var objFromDb = _unit.Invoices.GetFirstOrDefault(x => x.Id == ID);
            if (objFromDb == null)
            {
                return NotFound();
            }
            return View(objFromDb);
        }
        [HttpPost]
        [ValidateAntiForgeryToken] //to avoid the cross site request forgery
        public IActionResult DeleteInvoice(int? ID)
        {
            var invoice = _unit.Invoices.GetFirstOrDefault(x => x.Id == ID);
            if (invoice == null)
            {
                return NotFound();
            }

            _unit.Invoices.Remove(invoice);
            _unit.Save();
            TempData["success"] = "The record was deleted successfully!";
            return RedirectToAction("Index");
        }
        //displays details about an invoice
        [Authorize(Roles = StaticDetails.Role_Admin)]
        public IActionResult Details()
        {
            InvoicesVM invoices = new()
            {
                invoices = new(),
                TenancyList = _unit.Tenancies.GetAll().Select(x => new SelectListItem
                {
                    Text = x.managementType + x.PropertiesRS.propertyAddress,
                    Value = x.Id.ToString()
                }),
                PropertyList = _unit.Properties.GetAll().Select(x => new SelectListItem
                {
                    Text = x.propertyAddress,
                    Value = x.ID.ToString()
                })


            };
            return View(invoices);
        }

    }
}
