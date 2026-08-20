using POS.Entity.Inovice;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace POS.Entity.Product;

public class Product
{
    public int Id { get; set; }
    
    [Required]
    [StringLength(50)]
    [Display(Name = "Product Name")]
    public string ProductName { get; set; } = string.Empty;
    
    [Required]
    [StringLength(50)]
    [Display(Name = "Product Code")]
    public string ProductCode { get; set; } = string.Empty;
    
    [StringLength(100)]
    [Display(Name = "Barcode")]
    public string Barcode { get; set; } = string.Empty;
    
    [JsonIgnore]
    public ICollection<ProductBatch> ProductBatches { get; set; } = [];

    [JsonIgnore]
    public ICollection<InvoiceItem> Items { get; set; } = [];

    [JsonIgnore]
    public ICollection<StockMovement> StockMovements { get; set; } = [];
}