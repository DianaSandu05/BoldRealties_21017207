using BoldRealties.Models;

namespace BoldRealties.DAL.Repository.IRepository
{
    public class TenanciesRepository : Repository<tenancies>, ITenancyRepository
    {
        private BoldRealties_dbContext _db;
        public TenanciesRepository(BoldRealties_dbContext db) : base(db)
        {
            _db = db;
        }
        public void Update(tenancies tenancy)
        {
            _db.tenancies.Update(tenancy);
        }
    }
}
