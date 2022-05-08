using BoldRealties.DAL.Repository.IRepository;
using BoldRealties.Models;
using System;
using System.Collections.Generic;
using System.Text;
using BoldRealties.DAL;

namespace BoldRealties.DAL.Repository
{
    public class OrderDetailsRepository : Repository<OrderDetails>, IOrderDetailsRepository
    {
        private BoldRealties_dbContext _db;

        public OrderDetailsRepository(BoldRealties_dbContext db) : base(db)
        {
            _db = db;
        }


        public void Update(OrderDetails obj)
        {
            _db.OrderDetail.Update(obj);
        }
    }
}
