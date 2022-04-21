namespace BoldRealties.DAL.Repository.IRepository
{
    public interface IUnitOfWork
    {
        IAccountsRepository Accounts { get; }
        IBAReportsRepository BA_Reports { get; }
        IDepositsRepository Deposits { get; }
        IEnquiriesRepository Enquiries { get; }
        IInvoicesRepository Invoices { get; }
        ImaintenanceJobsRepository maintenanceJobs { get; }
        IofficeAddressRepository OfficeAddress { get; }
        IpaymentRepository payment { get; }

        IPropertiesRSRepository Properties { get; }
        ITenancyRepository Tenancies { get; }
        IUserRepository Users { get; }

        IViewingsRepository viewings { get; }
        IRentPaymentHeaderRepository RentPaymentHeader { get; }
        IRentPaymentDetailsRepository RentPaymentDetails { get; }
        
        void Save();
    }
}
