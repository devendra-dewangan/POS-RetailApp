using POS.Entity;
using POS.Entity.Inovice;
using POS.Model;
using POS.Repos;

namespace POS.Services
{
    public class PurchaseService : IPurchaseService
    {
        private IUnitOfWork _unitOfWork;
        private ILiteStore _liteStore;

        public PurchaseService(IUnitOfWork uow, ILiteStore liteStore)
        {
            _unitOfWork = uow;
            _liteStore = liteStore;
        }

        public async Task<int> AddPurchaseAsync(int supplierId)
        {
            var supplier = await _unitOfWork.Suppliers.GetByIDAsync(supplierId) 
                ?? throw new InvalidOperationException("Supplier not found.");

            var purchase = new PurchaseCart
            {
                Purchase = new PurchaseInvoice
                {
                    Supplier = supplier,
                    Invoice = new Invoice
                    {
                        InvoiceNumber = $"INV-{DateTime.UtcNow.Ticks}",
                        InvoiceDate = DateTime.UtcNow,
                        InvoiceItems = []
                    }

                },
                Status = CartStatus.Open,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,

            };
            _liteStore.PurchaseCarts.Upsert(purchase);
            return purchase.Id;
        }

        public async Task<PurchaseInvoice> CompletePurchaseAsync(int purchaseCartId)
        {
            var purchaseCart = _liteStore.PurchaseCarts.FindById(purchaseCartId);
            if (purchaseCart == null || purchaseCart.Status != CartStatus.Open)
                throw new InvalidOperationException("Invalid purchase cart.");

            purchaseCart.Status = CartStatus.Locked;
            _liteStore.PurchaseCarts.Update(purchaseCart);


            var purchase = purchaseCart.Purchase!;
            var purchaseItems = purchase.Invoice!.InvoiceItems;
            var productIds = purchaseItems.Select(i => i.ProductId).Distinct().ToList();
            var products = await _unitOfWork.Products.GetByIdsAsync(productIds);
            var productDict = products!.ToDictionary(p => p.Id);

            using var transaction = await _unitOfWork.BeginTransactionAsync();
            try
            {
                //todo
                foreach (var item in purchaseItems)
                { 
                //{
                //    // Ensure relationship
                //    item.Purchase = purchase;

                //    foreach (var batch in item.Batch)
                //    {
                //        batch.ProductId = item.ProductId;
                //        batch.PurchaseItem = item;
                //    }

                //    if (!productDict!.TryGetValue(item.ProductId, out var product))
                //        throw new Exception($"Product not found: {item.ProductId}");

                    //product.TotalStock += item.Quantity;
                }

                await _unitOfWork.Purchases.AddAsync(purchase);
                await _unitOfWork.CommitAsync();
                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }

            // Update the cart status
            purchaseCart.Status = CartStatus.Completed;
            _liteStore.PurchaseCarts.Update(purchaseCart);
            _liteStore.PurchaseCarts.Delete(purchaseCartId);
            return purchase;
        }

        public async Task<IEnumerable<PurchaseInvoice>> GetAllPurchasesAsync()
        {
            return await _unitOfWork.Purchases.GetAllAsync() ?? [];
        }



        public async Task<IEnumerable<PurchaseInvoice>> GetPurchaseByInvoiceAsync(string invoiceNumber)
        {
            var invoice = await _unitOfWork.Invoices.GetInvoiceByInvoiceNumber(invoiceNumber);
            var purchases = await _unitOfWork.Purchases.GetByInvoiceIdsAsync(invoice.Select(x => x.Id));
            return purchases ?? [];
        }

        public async Task<PurchaseInvoice?> AddPurchaseItemAsync(int purchaseDraftId, AddPurchaseItemRequestDto request)
        {

            var purchaseCart = _liteStore.PurchaseCarts.FindById(purchaseDraftId);

            if (purchaseCart == null || purchaseCart.Status != CartStatus.Open)
                throw new InvalidOperationException("Invalid purchase cart.");

            var purchase = purchaseCart.Purchase!;

            var product = await _unitOfWork.Products.GetByIDAsync(request.ProductId)
                ?? throw new InvalidOperationException("Product not found.");


            // ✅ Create new PurchaseItem
            var purchaseItem = new InvoiceItem
            {
                ProductId = request.ProductId,
                Batch = new ProductBatch
                {
                    BatchNumber = request.Batch.BatchNumber,
                    RemainingStock = request.Batch.Quantity,
                    OpeningStock = request.Batch.Quantity,
                    MRP = request.Batch.MRP,
                    SaleRate = request.Batch.SalePrice,
                    ProductId = request.ProductId,
                },
            };


            purchase.Invoice!.InvoiceItems.Add(purchaseItem);
            purchaseCart.UpdatedAt = DateTime.UtcNow;

            // Save in LiteDB (draft)
            _liteStore.PurchaseCarts.Update(purchaseCart);

            return purchase;
        }
    }
}