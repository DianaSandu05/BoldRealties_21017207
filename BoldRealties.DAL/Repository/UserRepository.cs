using BoldRealties.Models;

namespace BoldRealties.DAL.Repository.IRepository
{
    public class UserRepository : Repository<Users>, IUserRepository
    {
        private BoldRealties_dbContext _db;
        public UserRepository(BoldRealties_dbContext db) : base(db)
        {
            _db = db;
        }


        public void Update(Users user)
        {
            _db.Users.Update(user);
        }
     

        public Users GetUserByID(string userId)
        {
            return _db.user.Find(userId);
        }
    }
}
