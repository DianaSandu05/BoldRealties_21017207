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
    public class Viewings
    {
        [Key]
        public int Id { get; set; }
        public DateTime viewing_Date { get; set; }
        public string UserID { get; set; }
        [ForeignKey("UserID")]
        [ValidateNever]
        public IdentityUser Users { get; set; }

        public int PropertyID { get; set; }
        [ForeignKey("PropertyID")]
        [ValidateNever]
        public PropertiesRS PropertiesRS { get; set; }

        public int ApplicantID { get; set; }
        [ForeignKey("ApplicantID")]
        [ValidateNever]
        public Enquiries Enquiries { get; set; }

        public bool isConfirmed { get; set; }

    }
}
