using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoldRealties.DAL.Repository.IRepository;
using BoldRealties.Models;


namespace BoldRealties.DAL.Repository
{
    public class paymentRepository : Repository<payment>, IpaymentRepository
    {
        private BoldRealties_dbContext _db;

        public paymentRepository(BoldRealties_dbContext db) : base(db)
        {
            _db = db;
        }

        public int DecrementCount(payment payment, int count)
        {
            payment.Count -= count;
            return payment.Count;
        }

        public int IncrementCount(payment payment, int count)
        {
            payment.Count += count;
            return payment.Count;
        }
    }

}