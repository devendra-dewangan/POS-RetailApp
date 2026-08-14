using POS.Entity;

namespace POS.Repos
{
    public interface IBatchRepo : IRepository<ProductBatch>, IAddBulk<ProductBatch>
    {
         Task<IEnumerable<ProductBatch>?> GetByPurchaseIdAsync(int purchaseId);
    }
}