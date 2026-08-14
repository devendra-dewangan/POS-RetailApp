using POS.Entity.Inovice;

namespace POS.Entity;

public class SaleCart
{
    public int Id { get; set; }
    public SaleInvoice? Sale { get; set; }
    public ICollection<InvoiceItem> Items { get; set; } = [];
    public CartStatus Status { get; set; } = CartStatus.Open;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}