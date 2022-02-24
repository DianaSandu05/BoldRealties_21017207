using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoldRealties.Models;

namespace BoldRealties.DAL.Repository.IRepository
{
    public interface IpaymentRepository : IRepository<payment>
    {
        void Update(payment payment);

    }
}
