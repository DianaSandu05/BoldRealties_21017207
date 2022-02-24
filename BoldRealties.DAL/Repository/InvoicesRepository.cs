using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoldRealties.Models;
using BoldRealties.DAL.Repository.IRepository;

namespace BoldRealties.DAL.Repository
{
    public class InvoicesRepository : Repository<Invoices>, IInvoicesRepository
    {
        private BoldRealties_dbContext _db;
        
        public InvoicesRepository(BoldRealties_dbContext db) :base(db)
        {
            _db = db;
        }
        public void Update(Invoices invoices)
        {
            _db.Invoices.Update(invoices);
        }
    }
}
