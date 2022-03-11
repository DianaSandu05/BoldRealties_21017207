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
    public class Accounts
    {
        //we first create the properties
        // then add reference of this class in the DbContext
        [Key]
        public int ID { get; set; }
        public float balance_due { get; set; }
        public float charged_date { get; set; }
        public bool isReceived { get; set; }
        public DateTime received_Date { get; set; }

        public string UserID { get; set; }
        [ForeignKey("UserID")]
        [ValidateNever]
        public IdentityUser Users { get; set; }

        public int TenancyID { get; set; }
        [ForeignKey("TenancyID")]
        [ValidateNever]
        public tenancies tenancies { get; set; }

        public int PaymentID { get; set; }
        [ForeignKey("PaymentID")]
        [ValidateNever]
        public payment payment { get; set; }
  
        public int InvoiceID { get; set; }
        [ForeignKey("InvoiceID")]
        [ValidateNever]
        public Invoices Invoices { get; set; }
    
        public string FilePath { get; set; }
    }
}
