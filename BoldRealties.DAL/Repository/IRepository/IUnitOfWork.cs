namespace BoldRealties.DAL.Repository.IRepository
{
    public interface IUnitOfWork
    {
     
        IDepositsRepository Deposits { get; }
        IEnquiriesRepository Enquiries { get; }
        IInvoicesRepository Invoices { get; }
        ImaintenanceJobsRepository maintenanceJobs { get; }
        IPropertiesRSRepository Properties { get; }
        ITenancyRepository Tenancies { get; }
        IUserRepository Users { get; }
        IViewingsRepository viewings { get; }
        IOrderHeaderRepository OrderHeader { get; }
        IOrderDetailsRepository OrderDetails { get; }
        IShoppingCartRepository ShoppingCart { get; }

        void Save();
    }
}
