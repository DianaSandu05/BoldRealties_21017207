using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BoldRealties.Models
{
    public class tenancies
    {
        [Key]
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        [Required]
        public float rentPrice { get; set; }
        [Required]
        public float comission { get; set; }
     
        public int? PropertyID { get; set; }
        [ForeignKey("PropertyID")]
        [ValidateNever]
        public PropertiesRS PropertiesRS { get; set; }

        public string? UserID { get; set; }
        [ForeignKey("UserID")]
        [ValidateNever]
        public Users Users { get; set; }


        public string managementType { get; set; }

        public string? filePath { get; set; }
        [ValidateNever]
        public bool billsIncluded { get; set; }
    }
}
