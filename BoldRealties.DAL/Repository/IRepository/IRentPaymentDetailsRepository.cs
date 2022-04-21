using BoldRealties.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoldRealties.DAL.Repository.IRepository
{
    public interface IRentPaymentDetailsRepository : IRepository<RentPaymentDetails>
    {
        void Update(RentPaymentDetails obj);
      
    }
}
