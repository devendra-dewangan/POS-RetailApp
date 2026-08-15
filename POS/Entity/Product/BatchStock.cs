namespace POS.Entity.Product
{
    public class BatchStock
    {
        public int Id { get; set; }
        public decimal OnHand { get; set; }

        public decimal Reserved { get; set; }

        public decimal Available => OnHand - Reserved;

        public int Balance { get; set; }
        public int ProductBatchId { get; set; }
        public ProductBatch ProductBatch { get; set; } = null!;
    }
}
