using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoldRealties.DAL.Repository.IRepository;
using BoldRealties.Models;

namespace BoldRealties.DAL.Repository
{
    public class ViewingsRepository : Repository<Viewings>, IViewingsRepository
    {
        private BoldRealties_dbContext _db;
    public ViewingsRepository(BoldRealties_dbContext db) : base(db)
    {
        _db = db;
    }


    public void Update(Viewings viewings)
    {
        _db.Viewings.Update(viewings);
    }
}
}
