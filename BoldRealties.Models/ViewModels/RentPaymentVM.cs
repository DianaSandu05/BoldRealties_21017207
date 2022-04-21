using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoldRealties.Models.ViewModels
{
    public class RentPaymentVM
    {
        public RentPaymentHeader RentPaymentHeader { get; set; }
        public IEnumerable<RentPaymentDetails> RentPaymentDetails { get; set; }
    }
}
