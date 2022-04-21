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
    public class payment
    {
        [Key]
        public int ID{get; set; }
        public double amount { get; set; }
        public DateTime received_Date { get; set; }
        public int TenancyID { get; set; }
        [ForeignKey("TenancyID")]
        [ValidateNever]
        public tenancies tenancies { get; set; }
   
        public string? UserID { get; set; }
        [ForeignKey("UserID")]
        [ValidateNever]
        public Users Users { get; set; }
       
        public int Count { get; set; }

       

    }
}
