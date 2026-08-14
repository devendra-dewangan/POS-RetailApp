using POS.Entity.Person;
using System.ComponentModel.DataAnnotations;

namespace POS.Entity.Inovice
{
    public class SaleInvoice
    {
        public int Id { get; set; }
        
        [Required]
        public int BuyerId { get; set; }
        
        public Buyer? Buyer { get; set; }

        [Required]
        public int InvoiceId { get; set; }
        public Invoice? Invoice { get; set; }
    }
}
