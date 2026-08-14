using POS.Entity.Inovice;
using System.Text.Json.Serialization;
namespace POS.Entity.Person;

public class Supplier
{
    public int Id { get; set; }
    public string SupplierCode { get; set; } = string.Empty;
    public int PersonId { get; set; }
    public Person Person { get; set; }

    [JsonIgnore]
    public ICollection<PurchaseInvoice> Purchases { get; set; } = [];
}
