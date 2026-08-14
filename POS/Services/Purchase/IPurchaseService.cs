using POS.Entity.Inovice;
using POS.Model;

namespace POS.Services
{
    public interface IPurchaseService
    {
        Task<int> AddPurchaseAsync(int supplierId);
        Task<IEnumerable<PurchaseInvoice>> GetPurchaseByInvoiceAsync(string invoiceNumber);
        Task<PurchaseInvoice?> AddPurchaseItemAsync(int purchaseDraftId, AddPurchaseItemRequestDto request);
        Task<PurchaseInvoice> CompletePurchaseAsync(int purchaseCartId);
        Task<IEnumerable<PurchaseInvoice>> GetAllPurchasesAsync();
    }
}