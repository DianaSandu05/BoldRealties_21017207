using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoldRealties.Models
{
    public class jobs
    {
        [Key]
        public int ID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool isCompleted { get; set; }
        public string diagnostics { get; set; }
    
        public int tenanciesID { get; set; }
        [ForeignKey("tenanciesID")]
        [ValidateNever]
        public tenancies Tenancies { get; set; }

        public string UserID { get; set; }
        [ForeignKey("UserID")]
        [ValidateNever]
        public IdentityUser Users { get; set; }

        public string filePath { get; set; }
        public int PropertyID { get; set; }
        [ForeignKey("PropertyID")]
        [ValidateNever]
        public PropertiesRS PropertiesRS { get; set; }

        public int invoiceID { get; set; }
        [ForeignKey("invoiceID")]
        [ValidateNever]
        public Invoices Invoices { get; set; }

        public string imagePath { get; set; }
    }
}
