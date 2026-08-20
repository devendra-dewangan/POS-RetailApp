using POS.Entity.Inovice;

namespace POS.Entity.Product
{
    public enum StockMovementType
    {
        OpeningStock,
        Purchase,
        Sale,
        PurchaseReturn,
        SalesReturn,
        Adjustment,
        Damage,
        Expired
    }
    public class StockMovement
    {
        public long Id { get; set; }
        public StockMovementType Type { get; set; }
        public decimal Quantity { get; set; }
        public string ReferenceType { get; set; } = null!;
        public long ReferenceId { get; set; }
        public Invoice? Invoice { get; set; }

        public int? CreatedByUserId { get; set; }
        public DateTime CreatedAt { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;

        public int ProductBatchId { get; set; }
        public ProductBatch ProductBatch { get; set; } = null!;
    }
}
