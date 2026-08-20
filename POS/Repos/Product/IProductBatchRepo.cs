using POS.Entity.Product;

namespace POS.Repos
{
    public interface IProductBatchRepo : IRepository<ProductBatch>, IAddBulk<ProductBatch>
    {
         Task<IEnumerable<ProductBatch>> GetByPurchaseIdAsync(int purchaseId);
         Task<IEnumerable<ProductBatch>> GetByProductIdAsync(int productId);
        Task<IEnumerable<ProductBatch>> GetByBatchIds(IEnumerable<int> batchIds);
    }
}