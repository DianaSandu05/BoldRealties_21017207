using BoldRealties.DAL.Repository.IRepository;
using BoldRealties.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoldRealties.DAL.Repository
{
    public class RentPaymentDetailsRepository : Repository<RentPaymentDetails>, IRentPaymentDetailsRepository
    {
        private BoldRealties_dbContext _db;

        public RentPaymentDetailsRepository(BoldRealties_dbContext db) : base(db)
        {
            _db = db;
        }


        public void Update(RentPaymentDetails obj)
        {
            _db.RentPaymentDetails.Update(obj);
        }

   
    }
}

