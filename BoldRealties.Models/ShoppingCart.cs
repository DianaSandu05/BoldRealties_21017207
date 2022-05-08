using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BoldRealties.Models
{
    public class ShoppingCart
    {
        public int Id { get; set; }

        public int TenancyId { get; set; }
        [ForeignKey("TenancyId")]
        [ValidateNever]
        public tenancies tenancies { get; set; }
        public int Count { get; set; }

        public string UserId { get; set; }
        [ForeignKey("UserId")]
        [ValidateNever]
        public Users Users { get; set; }

        [NotMapped]
        public double Price { get; set; }
    }
}
