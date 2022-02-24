﻿using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoldRealties.Models
{
    public class Users
    {
        [Key]
        public int ID { get; set; }
        public string firstName { get; set; }
        [Required]
        public string lastName { get; set; }
        [Required]
        public string userName { get; set; }
        public string password { get; set; }
        public string filePath { get; set; }
        public string email { get; set; }    
        [Required]
        public string Role { get; set; }
        [Required]
        public int PropertyID { get; set; }
        [ForeignKey("PropertyID")]
        [ValidateNever]
        public PropertiesRS PropertiesRS { get; set; }

        public int ApplicantID { get; set; }
        [ForeignKey("ApplicantID")]
        [ValidateNever]
        public Enquiries Enquiries { get; set; }

        public int InvoicesID { get; set; }
        [ForeignKey("InvoicesID")]
        [ValidateNever]
        public Invoices Invoices { get; set; }

        public int AccountsID { get; set; }
        [ForeignKey("AccountsID")]
        [ValidateNever]
        public Accounts Accounts { get; set; }


        public string imagePath { get; set; }
    }
}
