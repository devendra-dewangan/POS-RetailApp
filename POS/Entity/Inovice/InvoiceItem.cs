namespace POS.Entity.Inovice
{
    public class InvoiceItem
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public Product? Product { get; set; }
        public int BatchId { get; set; }
        public ProductBatch? Batch { get; set; }

        public int Quantity { get; set; }
        public Decimal Price { get; set; }

    }
}
