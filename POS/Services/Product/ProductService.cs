using POS.Entity.Product;
using POS.Repos;

namespace POS.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<ProductService> _logger;

        public ProductService(IUnitOfWork uow, ILogger<ProductService> logger)
        {
            _unitOfWork = uow;
            _logger = logger;
        }

        public async Task<Product> AddProductAsync(string productName, string barcode , decimal MRP)
        {
            var transaction = await _unitOfWork.BeginTransactionAsync();
            var product = new Product
            {
                ProductName = productName,
                ProductCode = "P00-" + DateTime.UtcNow.Ticks, // Simple code generation
                Barcode = barcode
            };

            var batch = new ProductBatch
            {
                Product = product, 
                BatchNumber = "DEFAULT",
                MRP = MRP, 
                SaleRate = MRP 
            };
            product.ProductBatches.Add(batch);
            try
            {
                await _unitOfWork.Products.AddAsync(product);
                await _unitOfWork.CommitAsync();
                await transaction.CommitAsync();
                _logger.LogInformation("Product added successfully: {ProductName}", productName);
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, "Error adding product: {ProductName}", productName);
                
            }
            return product;
        }

        public async Task<IEnumerable<Product>?> GetProductByNameAsync(string productName)
        {
            return await _unitOfWork.Products.GetByNameAsync(productName);
        }

        public async Task<IEnumerable<Product>?> GetProductByBarcodeAsync(string barcode)
        {
            return await _unitOfWork.Products.GetByBarcodeAsync(barcode);
        }

        public async Task<IEnumerable<Product>?> GetAllProductsAsync()
        {
            return await _unitOfWork.Products.GetAllAsync() ?? [];
        }
    }
}