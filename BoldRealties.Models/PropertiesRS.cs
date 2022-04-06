using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BoldRealties.Models
{
    public class PropertiesRS
    {
        [Key]
        public int ID { get; set; }
        public float floorSpace { get; set; }
        public int balconiesNo { get; set; }
        [Required]
        public float balconySpace { get; set; }
        public int bathsNo { get; set; }
        [Required]
        public int bedsNo { get; set; }
        [Required]
        public int garagesNo { get; set; }
        public int parkingNo { get; set; }
        public string estateDescription { get; set; }
        [Required]
        public string propertyType { get; set; }
        [Required]
        public bool billsIncluded { get; set; }
        [Required]
        public string propertyAddress { get; set; }
        [Required]
        public float marketPrice { get; set; }
        [Required]
        public bool petsAllowed { get; set; }
        public float minPrice { get; set; }
        public int? TenancyID { get; set; }
        [ForeignKey("TenancyID")]
        [ValidateNever]
        public tenancies? tenancies { get; set; }
        public string? UserID { get; set; }
        [ForeignKey("UserID")]
        [ValidateNever]
        public IdentityUser? Users { get; set; }
        public int? EnquiriesID { get; set; }
        [ForeignKey("EnquiriesID")]
        [ValidateNever]
        public Enquiries? Enquiries { get; set; }
        [ValidateNever]
      
        public string imagePath { get; set; }
        [ValidateNever]
        public float maxPrice { get; set; }
    

    }
}
