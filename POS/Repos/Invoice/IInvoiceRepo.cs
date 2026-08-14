
namespace POS.Repos.Invoice;
using POS.Entity.Inovice;

public interface IInvoiceRepo : IRepository<Invoice>, IAddBulk<Invoice>
{
}
