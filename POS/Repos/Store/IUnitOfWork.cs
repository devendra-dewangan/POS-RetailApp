using Microsoft.EntityFrameworkCore.Storage;
using POS.Repos.Attendance;
using POS.Repos.Invoice;

namespace POS.Repos
{
    public interface IUnitOfWork
    {
        IProductRepo Products {get;}
        IProductBatchRepo ProductBatches {get;}
        IInvoiceRepo Invoices {get;}
        IInvoiceItemRepo InvoiceItems {get;}
        ISaleRepo Sales {get;}
        IBuyerRepo Buyers {get;}
        ISupplierRepo Suppliers {get;}
        IPurchaseRepo Purchases {get;}
        IRefreshTokenRepo RefreshTokens { get; }
        IImportInfoRepo ImportInfos { get; }
        IAttendanceDayRepo AttendanceDays { get; }
        IAttendancePunchRepo AttendancePunches { get; }

        Task<int> CommitAsync(CancellationToken cancellationToke = default);
        Task<IDbContextTransaction> BeginTransactionAsync();
    }
}