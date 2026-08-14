using POS.Entity.Inovice;

namespace POS.Repos
{
    public interface IPurchaseRepo : IRepository<PurchaseInvoice>, IAddBulk<PurchaseInvoice>
    {
        Task<IEnumerable<PurchaseInvoice>?> GetByInvoiceNumbersAsync(IEnumerable<string> invoiceNumbers);
        Task<IEnumerable<PurchaseInvoice>?> GetByInvoiceNumberAsync(string invoiceNumber);
    }
}