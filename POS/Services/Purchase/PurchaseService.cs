using POS.Entity.Product;
using POS.Entity.Inovice;
using POS.Model;
using POS.Repos;
using POS.Entity;

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
            // ----------------------------------------
            // 1. Get draft from LiteDB
            // ----------------------------------------

            var purchaseCart = _liteStore.PurchaseCarts
                .FindById(purchaseCartId);

            if (purchaseCart == null ||
                purchaseCart.Status != CartStatus.Open)
            {
                throw new InvalidOperationException(
                    "Invalid purchase cart.");
            }

            var purchase = purchaseCart.Purchase
                ?? throw new InvalidOperationException(
                    "Purchase not found.");

            var purchaseItems = purchase.Invoice?.InvoiceItems
                ?? throw new InvalidOperationException(
                    "Purchase items not found.");

            if (purchaseItems.Count == 0)
                throw new InvalidOperationException(
                    "Purchase contains no items.");


            // ----------------------------------------
            // 2. Lock the draft
            // ----------------------------------------

            purchaseCart.Status = CartStatus.Locked;
            purchaseCart.UpdatedAt = DateTime.UtcNow;

            _liteStore.PurchaseCarts.Update(purchaseCart);


            // ----------------------------------------
            // 3. Start permanent DB transaction
            // ----------------------------------------

            await using var transaction =
                await _unitOfWork.BeginTransactionAsync();

            try
            {
                // ----------------------------------------
                // 4. Load Products in bulk
                // ----------------------------------------

                var productIds = purchaseItems
                    .Select(x => x.ProductId)
                    .Distinct()
                    .ToList();

                var products = await _unitOfWork.Products
                    .GetByIdsAsync(productIds);

                var productDict = products
                    .ToDictionary(x => x.Id);


                // ----------------------------------------
                // 5. Load existing Batches in bulk
                // ----------------------------------------

                var existingBatchKeys = purchaseItems
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
                // 6. Prepare collections
                // ----------------------------------------

                var stockMovements = new List<StockMovement>();

                // Used when multiple cart items refer to
                // the same NEW batch.
                var newBatchDict =
                    new Dictionary<(int ProductId, string BatchNumber), ProductBatch>();


                // ----------------------------------------
                // 7. Process purchase items
                // ----------------------------------------

                foreach (var item in purchaseItems)
                {
                    // --------------------------------
                    // Product validation
                    // --------------------------------

                    if (!productDict.TryGetValue(
                            item.ProductId,
                            out var product))
                    {
                        throw new InvalidOperationException(
                            $"Product not found: {item.ProductId}");
                    }


                    ProductBatch? batch = null;


                    // --------------------------------
                    // Existing batch
                    // --------------------------------

                    if (item.BatchId != 0)
                    {
                        if (!batchDict.TryGetValue(
                                (item.ProductId, item.BatchId),
                                out batch))
                        {
                            throw new InvalidOperationException(
                                $"Batch not found. " +
                                $"ProductId: {item.ProductId}, " +
                                $"BatchId: {item.BatchId}");
                        }
                    }


                    // --------------------------------
                    // New batch
                    // --------------------------------

                    if (batch == null)
                    {
                        if (item.Batch == null)
                        {
                            throw new InvalidOperationException(
                                $"Batch information is missing " +
                                $"for product: {item.ProductId}");
                        }

                        (int ProductId, string BatchNumber) newBatchKey =
                            (item.ProductId, item.Batch.BatchNumber);

                        // Check whether we already created this
                        // new batch while processing another item.
                        if (!newBatchDict.TryGetValue(
                                newBatchKey,
                                out batch))
                        {
                            batch = new ProductBatch
                            {
                                ProductId = item.ProductId,

                                BatchNumber =
                                    item.Batch.BatchNumber,

                                MRP =
                                    item.Batch.MRP,

                                SaleRate =
                                    item.Batch.SaleRate,

                                BatchStock = new BatchStock
                                {
                                    Reserved = 0,
                                    Balance = 0,
                                    OnHand = 0
                                }
                            };

                            newBatchDict[newBatchKey] = batch;
                        }
                    }


                    // --------------------------------
                    // Make sure BatchStock exists
                    // --------------------------------

                    batch.BatchStock ??= new BatchStock
                    {
                        Reserved = 0,
                        Balance = 0,
                        OnHand = 0
                    };


                    // --------------------------------
                    // Increase stock
                    // --------------------------------

                    batch.BatchStock.OnHand += item.Quantity;


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

                        Type = StockMovementType.Purchase,

                        Quantity = item.Quantity,

                        ReferenceType = "PurchaseInvoice"
                    };

                    stockMovements.Add(movement);
                }




                // ----------------------------------------
                // 9. Save Purchase
                // ----------------------------------------

                await _unitOfWork.Purchases
                    .AddAsync(purchase);

                await _unitOfWork.CommitAsync();


                // ----------------------------------------
                // 10. Purchase ID is now available
                // ----------------------------------------

                foreach (var movement in stockMovements)
                {
                    movement.ReferenceId = purchase.Id;
                }


                // ----------------------------------------
                // 11. Add Stock Movements
                // ----------------------------------------

                if (stockMovements.Count > 0)
                {
                    await _unitOfWork.StockMovements
                        .AddBulkAsync(stockMovements);
                }


                // ----------------------------------------
                // 12. Save Stock Movements
                // ----------------------------------------

                await _unitOfWork.CommitAsync();


                // ----------------------------------------
                // 13. Commit permanent transaction
                // ----------------------------------------

                await transaction.CommitAsync();


                // ----------------------------------------
                // 14. Mark LiteDB cart completed
                // ----------------------------------------

                purchaseCart.Status = CartStatus.Completed;
                purchaseCart.UpdatedAt = DateTime.UtcNow;

                _liteStore.PurchaseCarts.Update(purchaseCart);
                return purchase;
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

                purchaseCart.Status = CartStatus.Open;
                purchaseCart.UpdatedAt = DateTime.UtcNow;

                _liteStore.PurchaseCarts.Update(purchaseCart);

                throw;
            }
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

            var batch = (await _unitOfWork.ProductBatches.GetByProductIdAsync(request.ProductId))
                    .FirstOrDefault(b => b.BatchNumber == request.Batch.BatchNumber)
                    ?? new ProductBatch()
                    {
                        BatchNumber = request.Batch.BatchNumber,
                        ProductId = request.ProductId,
                        MRP = request.Batch.MRP,
                        SaleRate = request.Batch.SalePrice,
                    };

            // ✅ Create new PurchaseItem
            var purchaseItem = new InvoiceItem
            {
                ProductId = request.ProductId,
                Quantity = request.Batch.Quantity,
                Price = request.Batch.UnitPrice,
                Batch = batch
            };

            purchase.Invoice!.InvoiceItems.Add(purchaseItem);
            purchaseCart.UpdatedAt = DateTime.UtcNow;

            // Save in LiteDB (draft)
            _liteStore.PurchaseCarts.Update(purchaseCart);

            return purchase;
        }
    }
}