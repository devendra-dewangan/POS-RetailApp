using Microsoft.EntityFrameworkCore.Storage;
using POS.Data;
using POS.Repos.Attendance;
using POS.Repos.Invoice;

namespace POS.Repos
{
    public class UnitOfWork(AppDbContext context) : IUnitOfWork
    {
        private IProductRepo? _products;
        public IProductRepo Products => _products ??= new ProductRepo(context);

        private IProductBatchRepo? _batches;
        public IProductBatchRepo ProductBatches => _batches ??= new ProductBatchRepo(context);

        private IInvoiceRepo? _invoice;
        public IInvoiceRepo Invoices => _invoice ??= new InvoiceRepo(context);

        private IInvoiceItemRepo? _saleItems;
        public IInvoiceItemRepo InvoiceItems => _saleItems ??= new InvoiceItemRepo(context);

        private ISaleRepo? _sales;
        public ISaleRepo Sales => _sales ??= new SaleRepo(context);

        private IBuyerRepo? _buyers;
        public IBuyerRepo Buyers => _buyers ??= new BuyerRepo(context);

        private ISupplierRepo? _suppliers;
        public ISupplierRepo Suppliers => _suppliers ??= new SupplierRepo(context);

        private IPurchaseRepo? _purchases;
        public IPurchaseRepo Purchases => _purchases ??= new PurchaseRepo(context);

        private IRefreshTokenRepo? _refreshTokens;
        public IRefreshTokenRepo RefreshTokens => _refreshTokens ??= new RefreshTokenRepo(context);

        private IImportInfoRepo? _importInfos;
        public IImportInfoRepo ImportInfos => _importInfos ??= new ImportInfoRepo(context);

        private IAttendanceDayRepo? _attendanceDays;
        public IAttendanceDayRepo AttendanceDays => _attendanceDays ??= new AttendanceDayRepo(context);

        private IAttendancePunchRepo? _attendancePunches;
        public IAttendancePunchRepo AttendancePunches => _attendancePunches ??= new AttendancePunchRepo(context);

        private IStockMovementRepo _stockMovements;
        public IStockMovementRepo StockMovements => _stockMovements ??= new StockMovementRepo(context);


        public async Task<int> CommitAsync(CancellationToken cancellationToken = default)
        {
            return await context.SaveChangesAsync(cancellationToken);
        }

        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            return await context.Database.BeginTransactionAsync();
        }

    }
}

      