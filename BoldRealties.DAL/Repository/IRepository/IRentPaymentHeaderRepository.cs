using BoldRealties.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoldRealties.DAL.Repository.IRepository
{
    public interface IRentPaymentHeaderRepository : IRepository<RentPaymentHeader>
    {
        void Update(RentPaymentHeader obj);
        void UpdateStatus(int id, string orderStatus, string? paymentStatus = null);
        void UpdateStripePaymentID(int id, string sessionId, string paymentItentId);
    }
}
