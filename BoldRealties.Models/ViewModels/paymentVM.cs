using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoldRealties.Models.ViewModels
{
    public class paymentVM
    {
        public IEnumerable<payment> PaymentList { get; set; }
     
        public RentPaymentHeader RentPaymentHeader { get; set; }
       
    }
}
