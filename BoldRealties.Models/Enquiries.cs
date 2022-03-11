using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Identity;

namespace BoldRealties.Models
{
    public class Enquiries
    {
        [Key]
        public int ID { get; set; }
        public string NameSurname { get; set; }
        [Required]
        public string email { get; set; }
        [Required]
        public string applicantType { get; set; }
        public int phone_No { get; set; }
        [Required]
        public bool pets { get; set; }
        [Required]
        public int noPets { get; set; }
        public int noBeds { get; set; }
        [Required]
        public int noBaths { get; set; }
        [Required]
        public string specification_Content { get; set; }
        public string location { get; set; }
        [Required]
        public string postcode { get; set; }
        [Required]
        public int minPrice { get; set; }
        [Required]
        public int maxPrice { get; set; }
        [Required]

        public string UserID { get; set; }
        [ForeignKey("UserID")]
        [ValidateNever]
        public IdentityUser Users { get; set; }
        public int PropertyID { get; set; }
        [ForeignKey("PropertyID")]
        [ValidateNever]

        public PropertiesRS PropertiesRS { get; set; }

     
        public string crime_Rate { get; set; }

    }
}
