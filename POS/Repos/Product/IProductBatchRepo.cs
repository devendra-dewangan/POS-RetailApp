using POS.Entity;

namespace POS.Repos
{
    public interface IProductBatchRepo : IRepository<ProductBatch>, IAddBulk<ProductBatch>
    {
         Task<IEnumerable<ProductBatch>?> GetByPurchaseIdAsync(int purchaseId);
    }
}