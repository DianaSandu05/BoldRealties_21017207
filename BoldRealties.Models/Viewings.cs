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
    public class Viewings
    {
        [Key]
        public int Id { get; set; }
        public DateTime viewing_Date { get; set; }
        public int UserID { get; set; }
        [ForeignKey("UserID")]
        [ValidateNever]
        public Users Users { get; set; }

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
