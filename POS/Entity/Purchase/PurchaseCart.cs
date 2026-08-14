using POS.Entity.Inovice;

namespace POS.Entity;

public enum CartStatus
{
    Open,
    Completed,
    Locked,
    Cancelled,
    Hold,
}

public class PurchaseCart
{
    public int Id { get; set; }
    public CartStatus Status { get; set; } = CartStatus.Open;
    public PurchaseInvoice Purchase { get; set; } = new();
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}