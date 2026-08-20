using POS.Entity.Inovice;

namespace POS.Services
{
    public interface ISaleService
    {
        Task<int> AddSaleAsync(int buyerId);
        Task<IEnumerable<SaleInvoice>?> GetSaleByInvoiceAsync(string invoiceNumber);
        Task<IEnumerable<SaleInvoice>?> GetAllSalesAsync();
    }
}