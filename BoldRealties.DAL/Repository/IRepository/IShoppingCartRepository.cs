using BoldRealties.DAL.Repository.IRepository;
using BoldRealties.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BoldRealties.DAL.Repository.IRepository
{
    public interface IShoppingCartRepository : IRepository<ShoppingCart>
    {
        int IncrementCount(ShoppingCart shoppingCart, int count);
        int DecrementCount(ShoppingCart shoppingCart, int count);
    }
}
