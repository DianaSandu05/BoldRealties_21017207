using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoldRealties.Models;

namespace BoldRealties.DAL.Repository.IRepository
{
    public interface IofficeAddressRepository : IRepository<officeAddress>
    {
        void Update(officeAddress officeAddress);
    }
}
