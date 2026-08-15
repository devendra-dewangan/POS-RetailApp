namespace POS.Entity.Inovice;
using POS.Entity.Product;
public class InvoiceItem
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public Product? Product { get; set; }
    public int BatchId { get; set; }
    public ProductBatch? Batch { get; set; }
    public decimal Quantity { get; set; }
    public decimal Price { get; set; }

}
