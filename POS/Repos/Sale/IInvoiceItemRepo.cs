using POS.Entity;
using POS.Entity.Inovice;

namespace POS.Repos
{
    public interface IInvoiceItemRepo : IRepository<InvoiceItem>,IAddBulk<InvoiceItem>
    {
        
    }
}