using POS.Entity.Inovice;

namespace POS.Repos
{
    public interface IPurchaseRepo : IRepository<PurchaseInvoice>, IAddBulk<PurchaseInvoice>
    {
        Task<IEnumerable<PurchaseInvoice>?> GetByInvoiceIdsAsync(IEnumerable<int> invoiceIds);
    }
}