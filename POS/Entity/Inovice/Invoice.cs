using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace POS.Entity.Inovice
{
    public class Invoice
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string InvoiceNumber { get; set; } = string.Empty;

        [Required]
        public DateTime InvoiceDate { get; set; }

        [JsonIgnore]
        public ICollection<InvoiceItem> InvoiceItems { get; set; } = [];
    }
}
