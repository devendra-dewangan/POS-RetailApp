namespace POS.Repos;
using POS.Entity.Inovice;

public interface IPurchaseItemRepo : IRepository<InvoiceItem>,IAddBulk<InvoiceItem>
{
}