using POS.Constants;
using POS.Entity.Inovice;

namespace POS.Entity.Product
{
    public class StockMovement
    {
        public long Id { get; set; }
        public TransactionType Type { get; set; }
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
