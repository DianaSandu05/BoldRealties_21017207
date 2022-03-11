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
        public bool billsIncluded { get; set; }
        public float comission { get; set; }
        [Required]
        public string imagePath { get; set; }
        public int PropertyID { get; set; }
        [ForeignKey("PropertyID")]
        [ValidateNever]
        public PropertiesRS PropertiesRS { get; set; }

        public string UserID { get; set; }
        [ForeignKey("UserID")]
        [ValidateNever]
        public IdentityUser Users { get; set; }

        public string managementType { get; set; }

        public int accountsID { get; set; }
        [ForeignKey("accountsID")]
        [ValidateNever]
        public Accounts Accounts { get; set; }

        public int DepositsID { get; set; }
        [ForeignKey("DepositsID")]
        [ValidateNever]
        public Deposits Deposits { get; set; }


        public string filePath { get; set; }

    }
}
