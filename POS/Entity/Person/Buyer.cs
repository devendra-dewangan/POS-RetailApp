using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace POS.Entity.Person
{
    public class Buyer
    {
        public int Id { get; set; }
        public string BuyerCode { get; set; } = string.Empty;
        public int PersonId { get; set; }
        public Person Person { get; set; }

        [JsonIgnore]
        public ICollection<Sale> Sales { get; set; } = [];
    }
}
