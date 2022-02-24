using BoldRealties.Models;

namespace BoldRealties.DAL.Repository.IRepository
{
    public interface ITenancyRepository : IRepository<tenancies>
    {
        void Update(tenancies tenancy);

    }
}
