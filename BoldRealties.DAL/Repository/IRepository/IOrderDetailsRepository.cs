using BoldRealties.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BoldRealties.DAL.Repository.IRepository
{
    public interface IOrderDetailsRepository : IRepository<OrderDetails>
    {
        void Update(OrderDetails obj);
    }
}
