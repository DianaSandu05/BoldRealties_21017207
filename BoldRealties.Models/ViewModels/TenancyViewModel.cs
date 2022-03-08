using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoldRealties.Models.ViewModels
{
    public class TenancyViewModel
    {
        public tenancies tenancy { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> PropertiesList { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> UserList { get; set; }
        public IEnumerable<SelectListItem> AccountsList { get; set; }
       
        public IEnumerable<SelectListItem> DepositsList { get; set; }
       
        public IEnumerable<SelectListItem> ViewingsList { get; set; }
       
        public IEnumerable<SelectListItem> EnquiriesList { get; set; }
       
        public IEnumerable<SelectListItem> baReportsList { get; set; }
      
        public IEnumerable<SelectListItem> maintenanceJobsList { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> officeAddressList { get; set; }
        public IEnumerable<SelectListItem> InvoicesList { get; set; }
        public IEnumerable<SelectListItem> paymentList { get; set; }



    }
}
