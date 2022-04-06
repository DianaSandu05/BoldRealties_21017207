using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoldRealties.Models.ViewModels
{
    public class InvoicesVM
    {
        public Invoices invoices { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> TenancyList { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> PropertyList { get; set; }

    }
}
