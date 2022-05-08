using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace BoldRealties.Models
{
    public class Invoices
    {
        [Key]
        public int Id { get; set; }
        public int invoice_No { get; set; }
        [ValidateNever]
        public int PropertyID { get; set; }
        [ForeignKey("PropertyID")]
        [ValidateNever]
        public PropertiesRS PropertiesRS { get; set; }
        [ValidateNever]
        public int TenancyID { get; set; }
        [ForeignKey("TenancyID")]
        [ValidateNever]
        public tenancies tenancies { get; set; }
        
        public string? filePath { get; set; }
        public DateTime Due_Date { get; set; }

    }
}
