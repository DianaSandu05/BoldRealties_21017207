using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoldRealties.Models.ViewModels
{
    public class tenancyVM
    {
        public tenancies tenancy { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> PropertiesList { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> UserList { get; set; }
        [ValidateNever]

         public IEnumerable<SelectListItem> Invoices { get; set; }
    
    }
}
