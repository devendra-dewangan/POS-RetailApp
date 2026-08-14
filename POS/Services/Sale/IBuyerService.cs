using POS.Entity.Person;

namespace POS.Services
{
    public interface IBuyerService
    {
        Task<Buyer> AddBuyerAsync(string name);
        Task<Buyer?> GetBuyerByNameAsync(string name);
        Task<IEnumerable<Buyer>> GetAllBuyersAsync();
    }
}