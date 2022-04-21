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
    public class RentPaymentHeader
    {
        public int Id { get; set; }
        public string? UserId { get; set; }
        [ForeignKey("UserId")]
        [ValidateNever]
        public Users Users { get; set; }
        public DateTime PaymentDate { get; set; }
        public double PaymentAmount { get; set; }
        public string PaymentStatus { get; set; }
        public string OrderStatus { get; set; }
        public int RentPaymentId { get; set; }
        [ForeignKey("RentPaymentId")]
        [ValidateNever]
        public RentPaymentHeader RentPaymentHeaders { get; set; }
        public string? SessionId { get; set; }
        public int TenancyId { get; set; }
        [ForeignKey("TenancyId")]
        [ValidateNever]
        public tenancies tenancies { get; set; }
        
        public int Count { get; set; }
        public string? PaymentIntentId { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Address { get; set; }
        public string? Carrier { get; set; }
        public string? TrackingNumber { get; set; }
        public DateTime ShippingDate { get; set; }
        public DateTime PaymentDueDate { get; set; }

    }
}
