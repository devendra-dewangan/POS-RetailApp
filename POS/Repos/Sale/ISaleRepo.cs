using POS.Entity.Inovice;

namespace POS.Repos
{
    public interface ISaleRepo : IRepository<SaleInvoice>, IAddBulk<SaleInvoice>
    {
        Task<IEnumerable<SaleInvoice>?> GetByInvoiceNumberAsync(string invoiceNumber);
    }
}