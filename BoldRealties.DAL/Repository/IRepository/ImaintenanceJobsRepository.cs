using BoldRealties.Models;

namespace BoldRealties.DAL.Repository.IRepository
{
    public interface ImaintenanceJobsRepository : IRepository<jobs>
    {
        void Update(jobs maintenanceJobs);

    }
}
