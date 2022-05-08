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
    public class Users : IdentityUser
    {
     
        public string firstName { get; set; }
        [Required]
        public string lastName { get; set; }

        public string filePath { get; set; }
   
        public string? Address { get; set; }

    }
}
