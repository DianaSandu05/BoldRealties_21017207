using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoldRealties.DAL.Repository.IRepository;
using BoldRealties.Models;

namespace BoldRealties.DAL.Repository
{
    public class EnquiriesRepository : Repository<Enquiries>, IEnquiriesRepository
    {
        private BoldRealties_dbContext _db;
        public EnquiriesRepository(BoldRealties_dbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Enquiries enquiries)
        {
            _db.Enquiries.Update(enquiries);
        }
    }
}

