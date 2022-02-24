using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoldRealties.Models;

namespace BoldRealties.DAL.Repository.IRepository
{
    public interface IBAReportsRepository : IRepository<BA_Reports>
    {
        void Update(BA_Reports reports);
    }
}
