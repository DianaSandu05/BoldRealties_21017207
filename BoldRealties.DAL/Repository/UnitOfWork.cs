using BoldRealties.DAL.Repository.IRepository;

namespace BoldRealties.DAL.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BoldRealties_dbContext _db;
        public UnitOfWork(BoldRealties_dbContext db)
        {
            _db = db;
            Accounts = new AccountsRepository(_db);
            BA_Reports = new BAReportsRepository(_db);
            Deposits = new DepositsRepository(_db);
            Enquiries = new EnquiriesRepository(_db);
            Invoices = new InvoicesRepository(_db);
            payment = new paymentRepository(_db);
            maintenanceJobs = new maintenanceJobsRepository(_db);
            OfficeAddress = new officeAddressRepository(_db);
            Properties = new PropertiesRSRepository(_db);
            Tenancies = new TenanciesRepository(_db);
            Users = new UserRepository(_db);
            viewings = new ViewingsRepository(_db);
        }
        public IAccountsRepository Accounts { get; private set; }
        public IBAReportsRepository BA_Reports { get; private set; }
        public IDepositsRepository Deposits { get; private set; }
        public IEnquiriesRepository Enquiries { get; private set; }
        public IInvoicesRepository Invoices { get; private set; }
        public IpaymentRepository payment { get; private set; }
        public ImaintenanceJobsRepository maintenanceJobs { get; private set; }
        public IofficeAddressRepository OfficeAddress { get; private set; }
        public IPropertiesRSRepository Properties { get; private set; }
        public ITenancyRepository Tenancies { get; private set; }
        public IUserRepository Users { get; private set; }
      
        public IViewingsRepository viewings { get; private set; }
        
        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
