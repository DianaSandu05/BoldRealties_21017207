using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoldRealties.Models
{
    public class RentPaymentDetails
    {
        public int Id { get; set; }
        public int RentPaymentId { get; set; }
        [ForeignKey("RentPaymentId")]
        [ValidateNever]
        public RentPaymentHeader RentPaymentHeader { get; set; }
        public int TenancyId { get; set; }
        public tenancies Tenancies { get; set; }
        public int Count { get; set; }
        public double Price { get; set; }

    }
}
