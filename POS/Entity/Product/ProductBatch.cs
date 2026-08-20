using POS.Entity.Inovice;
using System.ComponentModel.DataAnnotations;

namespace POS.Entity.Product
{
    public class ProductBatch
    {
        public int Id { get; set; }

        [Display(Name = "Batch Number")]
        public string? BatchNumber { get; set; }
        public DateOnly? ExpiryDate { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "MRP must be a non-negative value")]
        [Display(Name = "MRP")]
        public decimal MRP { get; set; }
        
        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Sale Rate must be a non-negative value")]
        [Display(Name = "Sale Rate")]
        public decimal SaleRate { get; set; }
        
        [Required]
        public int ProductId { get; set; }
        public Product? Product { get; set; }

        public int BatchStockId { get; set; }
        public BatchStock BatchStock { get; set; } = null!;

        public ICollection<InvoiceItem> InvoiceItems { get; set; } = [];
        public ICollection<StockMovement> StockMovements { get; set; } = [];
    }
}
