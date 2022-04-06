using BoldRealties.Models;

namespace BoldRealties.DAL.Repository.IRepository
{
    public interface IPropertiesRSRepository : IRepository<PropertiesRS>
    {
        void Update(PropertiesRS properties);
        PropertiesRS GetPropertyByID(int propertyId);
    }
}
