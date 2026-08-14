using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace POS.Entity.Inovice
{
    public class Invoice
    {
        [Required]
        [StringLength(50)]
        public string InvoiceNumber { get; set; } = string.Empty;

        [Required]
        public DateTime InvoiceDate { get; set; }

        [JsonIgnore]
        public ICollection<InvoiceItem> InvoiceItems { get; set; } = [];
    }
}
