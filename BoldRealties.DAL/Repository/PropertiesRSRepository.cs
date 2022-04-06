using BoldRealties.Models;

namespace BoldRealties.DAL.Repository.IRepository
{
    public class PropertiesRSRepository : Repository<PropertiesRS>, IPropertiesRSRepository
    {
        private BoldRealties_dbContext _db;
        public PropertiesRSRepository(BoldRealties_dbContext db) : base(db)
        {
            _db = db;
        }


        public void Update(PropertiesRS properties)
        {
            _db.PropertiesRS.Update(properties);
        }
        public PropertiesRS GetPropertyByID(int propertyId)
        {
            return _db.PropertiesRS.Find(propertyId);
        }
    }
}
