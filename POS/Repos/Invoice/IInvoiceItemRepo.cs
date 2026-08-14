using POS.Entity;
using POS.Entity.Inovice;

namespace POS.Repos.Invoice
{
    public interface IInvoiceItemRepo : IRepository<InvoiceItem>,IAddBulk<InvoiceItem>
    {
        
    }
}