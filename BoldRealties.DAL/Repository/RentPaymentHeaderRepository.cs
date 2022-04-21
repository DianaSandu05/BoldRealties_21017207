using BoldRealties.DAL.Repository.IRepository;
using BoldRealties.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoldRealties.DAL.Repository
{
    public class RentPaymentHeaderRepository : Repository<RentPaymentHeader>, IRentPaymentHeaderRepository
    {
        private BoldRealties_dbContext _db;

        public RentPaymentHeaderRepository(BoldRealties_dbContext db) : base(db)
        {
            _db = db;
        }


        public void Update(RentPaymentHeader obj)
        {
            _db.RentPaymentHeaders.Update(obj);
        }

        public void UpdateStatus(int id,string orderStatus, string? paymentStatus = null)
        {
            var orderFromDb = _db.RentPaymentHeaders.FirstOrDefault(u => u.Id == id);
            orderFromDb.OrderStatus = orderStatus;
            if (orderFromDb != null)
            {
                if (paymentStatus != null)
                {
                    orderFromDb.PaymentStatus = paymentStatus;
                }
            }
        }

        public void UpdateStripePaymentID(int id, string sessionId, string paymentItentId)
        {
            var orderFromDb = _db.RentPaymentHeaders.FirstOrDefault(u => u.Id == id);
            orderFromDb.PaymentDate = DateTime.Now;
            orderFromDb.SessionId = sessionId;
            orderFromDb.PaymentIntentId = paymentItentId;
        }
    }
}

