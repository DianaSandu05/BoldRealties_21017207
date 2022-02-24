using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoldRealties.Models;
using BoldRealties.DAL.Repository.IRepository;

namespace BoldRealties.DAL.Repository
{
    public class AccountsRepository : Repository<Accounts>, IAccountsRepository
    {
        private BoldRealties_dbContext _db;
        public AccountsRepository(BoldRealties_dbContext db) : base(db)
        {
            _db = db;
        }
        public void Update(Accounts accounts)
        {
            _db.Accounts.Update(accounts);
        }
    }
}
