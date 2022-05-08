using BoldRealties.DAL.Repository.IRepository;

namespace BoldRealties.DAL.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BoldRealties_dbContext _db;
        public UnitOfWork(BoldRealties_dbContext db)
        {
            _db = db;
        
            Deposits = new DepositsRepository(_db);
            Enquiries = new EnquiriesRepository(_db);
            Invoices = new InvoicesRepository(_db);
            maintenanceJobs = new maintenanceJobsRepository(_db);
            Properties = new PropertiesRSRepository(_db);
            Tenancies = new TenanciesRepository(_db);
            Users = new UserRepository(_db);
            viewings = new ViewingsRepository(_db);
           
            OrderDetails = new OrderDetailsRepository(_db);
            OrderHeader = new OrderHeaderRepository(_db);
            ShoppingCart = new ShoppingCartRepository(_db);
        }
      
        public IDepositsRepository Deposits { get; private set; }
        public IEnquiriesRepository Enquiries { get; private set; }
        public IInvoicesRepository Invoices { get; private set; }
        public ImaintenanceJobsRepository maintenanceJobs { get; private set; }
        public IPropertiesRSRepository Properties { get; private set; }
        public ITenancyRepository Tenancies { get; private set; }
        public IUserRepository Users { get; private set; }
      
        public IViewingsRepository viewings { get; private set; }
     
        public IOrderHeaderRepository OrderHeader { get; private set; }
        public IOrderDetailsRepository OrderDetails { get; private set; }
        public IShoppingCartRepository ShoppingCart { get; private set; }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
