using POS.Constants;
using POS.Entity;
using POS.Entity.Inovice;
using POS.Entity.Product;
using POS.Repos;
namespace POS.Services
{
    public class SaleService : ISaleService
    {
        private IUnitOfWork _unitOfWork;
        private ILiteStore _liteStore;
        private const string SaleInvoice = "SaleInvoice";

        public SaleService(IUnitOfWork uow, ILiteStore liteStore)
        {
            _unitOfWork = uow;
            _liteStore = liteStore;
        }

        public async Task<int> AddSaleAsync(int buyerId)
        {

            var sale = new SaleCart
            {
                Sale = new SaleInvoice
                {
                    BuyerId = buyerId,
                    Invoice = new Invoice()
                    {

                    }
                },
                Status = CartStatus.Open
            };
            _liteStore.SaleCarts.Upsert(sale);
            return sale.Id;
        }

        public async Task<SaleInvoice> CompleteSaleAsync(int saleCartId)
        {
            var saleCart = _liteStore.SaleCarts
                .FindById(saleCartId);

            if (saleCart == null ||
                saleCart.Status != CartStatus.Open)
            {
                throw new InvalidOperationException(
                    TransactionMessages.InvalidCart(TransactionType.Sale));
            }

            var saleInvoice = saleCart.Sale
                ?? throw new InvalidOperationException(
                    TransactionMessages.CartNotFound(TransactionType.Sale));

            var saleItems = saleInvoice.Invoice?.InvoiceItems
                ?? throw new InvalidOperationException(
                    TransactionMessages.ItemNotFound(TransactionType.Sale));

            if (saleItems.Count == 0)
                throw new InvalidOperationException(
                    TransactionMessages.CartContainsNoItems(TransactionType.Sale));


            // ----------------------------------------
            // 2. Lock the draft
            // ----------------------------------------

            saleCart.Status = CartStatus.Locked;
            saleCart.UpdatedAt = DateTime.UtcNow;

            _liteStore.SaleCarts.Update(saleCart);


            // ----------------------------------------
            // 3. Start permanent DB transaction
            // ----------------------------------------

            await using var transaction =
                await _unitOfWork.BeginTransactionAsync();

            try
            {

                // ----------------------------------------
                // 4. Load existing Batches in bulk
                // ----------------------------------------

                var existingBatchKeys = saleItems
                    .Where(x => x.BatchId != 0)
                    .Select(x => (x.ProductId, x.BatchId))
                    .Distinct()
                    .ToHashSet();

                var batchIds = existingBatchKeys
                    .Select(x => x.BatchId)
                    .Distinct()
                    .ToList();

                var batches = (await _unitOfWork.ProductBatches
                        .GetByBatchIds(batchIds))
                    .Where(x =>
                        existingBatchKeys.Contains(
                            (x.ProductId, x.Id)))
                    .ToList();

                var batchDict = batches
                    .ToDictionary(x => (x.ProductId, x.Id));


                // ----------------------------------------
                // 5. Prepare collections
                // ----------------------------------------

                var stockMovements = new List<StockMovement>();


                // ----------------------------------------
                // 6. Process purchase items
                // ----------------------------------------

                foreach (var item in saleItems)
                {
                    ProductBatch? batch = null;

                    if (!batchDict.TryGetValue(
                            (item.ProductId, item.BatchId),
                            out batch))
                    {
                        throw new InvalidOperationException(
                            TransactionMessages.BatchNotFound(
                                TransactionType.Sale, item.ProductId, item.BatchId));
                    }

                    // --------------------------------
                    // Increase stock
                    // --------------------------------

                    if (batch.BatchStock.OnHand < item.Quantity)
                    {
                        throw new InvalidOperationException(
                            TransactionMessages.BatchQuantityInsufficient(
                                TransactionType.Sale, item.ProductId, item.BatchId));
                    }

                    batch.BatchStock.OnHand -= item.Quantity;
                    // --------------------------------
                    // Link permanent Batch to item
                    // --------------------------------

                    item.Batch = batch;
                    // --------------------------------
                    // Create stock movement
                    // --------------------------------

                    var movement = new StockMovement
                    {
                        ProductId = item.ProductId,
                        ProductBatch = batch,
                        Type = TransactionType.Sale,
                        Quantity = item.Quantity,
                        ReferenceType = SaleInvoice,
                    };

                    stockMovements.Add(movement);
                }

                // ----------------------------------------
                // 7. Save Purchase
                // ----------------------------------------

                await _unitOfWork.Sales
                    .AddAsync(saleInvoice);

                await _unitOfWork.CommitAsync();


                // ----------------------------------------
                // 8. Purchase ID is now available
                // ----------------------------------------

                foreach (var movement in stockMovements)
                {
                    movement.ReferenceId = saleInvoice.Id;
                }


                // ----------------------------------------
                // 9. Add Stock Movements
                // ----------------------------------------

                if (stockMovements.Count > 0)
                {
                    await _unitOfWork.StockMovements
                        .AddBulkAsync(stockMovements);
                }


                // ----------------------------------------
                // 10. Save Stock Movements
                // ----------------------------------------

                await _unitOfWork.CommitAsync();


                // ----------------------------------------
                // 11. Commit permanent transaction
                // ----------------------------------------

                await transaction.CommitAsync();


                // ----------------------------------------
                // 12. Mark LiteDB cart completed
                // ----------------------------------------

                saleCart.Status = CartStatus.Completed;
                saleCart.UpdatedAt = DateTime.UtcNow;

                _liteStore.SaleCarts.Update(saleCart);
                return saleInvoice;
            }
            catch
            {
                // ----------------------------------------
                // Rollback SQL transaction
                // ----------------------------------------
                await transaction.RollbackAsync();

                // ----------------------------------------
                // Re-open LiteDB draft so user can retry
                // ----------------------------------------

                saleCart.Status = CartStatus.Open;
                saleCart.UpdatedAt = DateTime.UtcNow;

                _liteStore.SaleCarts.Update(saleCart);

                throw;
            }
        }

        public async Task<IEnumerable<SaleInvoice>?> GetSaleByInvoiceAsync(string invoiceNumber)
        {
            var sales = await _unitOfWork.Sales.GetByInvoiceNumberAsync(invoiceNumber);
            return sales;
        }

        public async Task<IEnumerable<SaleInvoice>?> GetAllSalesAsync()
        {
            return await _unitOfWork.Sales.GetAllAsync();
        }

        public async Task<bool> AddSaleItemAsync(int saleCartId, int productId, int batchId, decimal quantity)
        {
            var saleCart = _liteStore.SaleCarts.FindById(saleCartId);
            if (saleCart == null || saleCart.Status != CartStatus.Open)
                throw new InvalidOperationException(TransactionMessages.InvalidCart(TransactionType.Sale));
            var invoice = saleCart.Sale!.Invoice!;

            var batch = await _unitOfWork.ProductBatches.GetByIDAsync(batchId)
                    ?? throw new InvalidOperationException(TransactionMessages.BatchNotFound(TransactionType.Sale, productId, batchId));

            if (batch.BatchStock.OnHand < quantity)
            {
                throw new InvalidOperationException(TransactionMessages.BatchQuantityInsufficient(TransactionType.Sale, productId, batchId));
            }

            invoice.InvoiceItems.Add(new InvoiceItem()
            {
                ProductId = productId,
                BatchId = batchId,
                Quantity = quantity,
                Price = batch.SaleRate
            });

            _liteStore.SaleCarts.Update(saleCart);
            return true;
        }
    }
}