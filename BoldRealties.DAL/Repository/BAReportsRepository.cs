using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoldRealties.Models;
using BoldRealties.DAL.Repository.IRepository;

namespace BoldRealties.DAL.Repository
{
    public class BAReportsRepository : Repository<BA_Reports>, IBAReportsRepository
    {
        private BoldRealties_dbContext _db;
        public BAReportsRepository(BoldRealties_dbContext db) :base(db)
        {
            _db = db;
        }
        public void Update(BA_Reports reports)
        {
            _db.BBAReports.Update(reports);
        }
    }
}
