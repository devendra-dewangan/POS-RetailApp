using POS.Entity.Person;

namespace POS.Repos
{
    public interface IBuyerRepo : IRepository<Buyer>,IAddBulk<Buyer>
    {
        Task<IEnumerable<Buyer>?> GetByNameAsync(string name);
    }
}