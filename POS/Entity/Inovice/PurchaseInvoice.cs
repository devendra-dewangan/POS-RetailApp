using POS.Entity.Person;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace POS.Entity.Inovice;

public class PurchaseInvoice
{
    public int Id { get; set; }
    
    [Required]
    public int SupplierId { get; set; }
    
    public Supplier? Supplier { get; set; }

    [Required]
    public int InvoiceId { get; set; }
    public Invoice? Invoice { get; set; }
}
