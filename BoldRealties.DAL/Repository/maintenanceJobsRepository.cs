using BoldRealties.Models;

namespace BoldRealties.DAL.Repository.IRepository
{
    public class maintenanceJobsRepository : Repository<jobs>, ImaintenanceJobsRepository
    {
        private BoldRealties_dbContext _db;
        public maintenanceJobsRepository(BoldRealties_dbContext db) : base(db)
        {
            _db = db;
        }


        public void Update(jobs mJ)
        {
            _db.jobs.Update(mJ);
            var objFromDb = _db.jobs.FirstOrDefault(x => x.ID == mJ.ID);
            if (objFromDb != null)
            {
                objFromDb.StartDate = mJ.StartDate;
                objFromDb.EndDate = mJ.EndDate;
                objFromDb.isCompleted = mJ.isCompleted;

            }
        }
    }
}
