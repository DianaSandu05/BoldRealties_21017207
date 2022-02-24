using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoldRealties.Models;
using BoldRealties.DAL.Repository.IRepository;

namespace BoldRealties.DAL.Repository
{
    public class DepositsRepository :Repository<Deposits>, IDepositsRepository
    {
        private BoldRealties_dbContext _db;

        public DepositsRepository(BoldRealties_dbContext db) :base(db)
        {
            _db = db;
        }
        public void Update(Deposits deposits)
        {
            _db.Deposits.Update(deposits);
        }
    }
}
