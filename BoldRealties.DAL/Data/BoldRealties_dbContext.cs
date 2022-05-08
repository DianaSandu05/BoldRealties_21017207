using BoldRealties.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BoldRealties.DAL
{
    //it is like a data layer to connect the application to the database
    public class BoldRealties_dbContext : IdentityDbContext // DbContext is part of EntityFrameworkCore
                                                    // it is used to manage and access the database
    {
        public BoldRealties_dbContext(DbContextOptions<BoldRealties_dbContext> options) : base(options)
        { // retrieve the options
          //and pass that on to the base class of DbContext
          //this constructor is critical for the connection with the database


        }
        // we reference each class here and then we do the migration
        public DbSet<PropertiesRS> PropertiesRS { get; set; }
        public DbSet<Users> user { get; set; }
        public DbSet<tenancies> tenancies { get; set; }
        public DbSet<jobs> jobs { get; set; }
        public DbSet<Enquiries> Enquiries { get; set; }
        public DbSet<Deposits> Deposits { get; set; }
        public DbSet<Invoices> Invoices { get; set; }
        public DbSet<Viewings> Viewings { get; set; }
        public DbSet<OrderDetails> OrderDetail { get; set; }
        public DbSet<OrderHeader> OrderHeaders { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
    }
}
