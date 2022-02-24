using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoldRealties.Models;
using BoldRealties.DAL.Repository.IRepository;

namespace BoldRealties.DAL.Repository.IRepository
{
    public interface IEnquiriesRepository : IRepository<Enquiries>
    {
        void Update(Enquiries enquiries);
    }
}
