using BoldRealties.Models;

namespace BoldRealties.DAL.Repository.IRepository
{
    public interface IUserRepository : IRepository<Users>
    {
        void Update(Users user);

    }
}
