using Microsoft.EntityFrameworkCore.Storage;
using POS.Repos.Attendance;

namespace POS.Repos
{
    public interface IUnitOfWork
    {
        IProductRepo Products {get;}
        IBatchRepo Batches {get;}
        IInvoiceItemRepo SaleItems {get;}
        ISaleRepo Sales {get;}
        IBuyerRepo Buyers {get;}
        ISupplierRepo Suppliers {get;}
        IPurchaseRepo Purchases {get;}
        IRefreshTokenRepo RefreshTokens { get; }
        IPurchaseItemRepo PurchaseItems { get; }
        IImportInfoRepo ImportInfos { get; }
        IAttendanceDayRepo AttendanceDays { get; }
        IAttendancePunchRepo AttendancePunches { get; }

        Task<int> CommitAsync(CancellationToken cancellationToke = default);
        Task<IDbContextTransaction> BeginTransactionAsync();
    }
}