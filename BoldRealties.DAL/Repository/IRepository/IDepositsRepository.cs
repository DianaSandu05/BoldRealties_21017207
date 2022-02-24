using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoldRealties.Models;

namespace BoldRealties.DAL.Repository.IRepository
{
    public interface IDepositsRepository : IRepository<Deposits>
    {
        void Update(Deposits deposits);
    }
}
