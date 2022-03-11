using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Identity;

namespace BoldRealties.Models
{
    public class Deposits
    {
        [Key]
        public int Id { get; set; }
        public int amount { get; set; }
        [Required]
        public bool isReceived { get; set; }
        [Required]
        public DateTime receivedDate { get; set; }
        [Required]
        public DateTime due_date { get; set; }
        public string FilePath { get; set; }
        public bool isProtected { get; set; }
        [Required]
        public DateTime protected_Date { get; set; }
        [Required]
        public bool isReturned { get; set; }
        [Required]
        public DateTime returned_Date { get; set; }
        public bool isTransfered { get; set; }
        [Required]

        public string UserID { get; set; }
        [ForeignKey("UserID")]
        [ValidateNever]
        public IdentityUser Users { get; set; }
        public int TenancyID { get; set; }
        [ForeignKey("TenancyID")]
        [ValidateNever]
        public tenancies tenancies { get; set; }

        public DateTime transfered_Date { get; set; }
       
    }
}
