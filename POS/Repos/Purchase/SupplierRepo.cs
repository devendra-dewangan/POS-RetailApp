using Microsoft.EntityFrameworkCore;
using POS.Data;
using POS.Entity.Person;

namespace POS.Repos
{
    public class SupplierRepo : ISupplierRepo
    {
        private AppDbContext _context;
        public SupplierRepo(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }

        public async Task AddAsync(Supplier value)
        {
            await _context.Suppliers.AddAsync(value);
        }

        public async Task AddBulkAsync(IEnumerable<Supplier> values)
        {
            await _context.Suppliers.AddRangeAsync(values);

        }

        public Task DeleteAsync(Supplier value)
        {
            return Task.Run(()=> true);
        }

        public async Task<IEnumerable<Supplier>?> GetAllAsync()
        {
            return await _context.Suppliers.ToListAsync();
        }

        public Task<Supplier?> GetByIDAsync(int id)
        {
            return _context.Suppliers.FirstOrDefaultAsync(x=>x.Id == id);
        }

        public async Task<IEnumerable<Supplier>?> GetByNameAsync(string name)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Supplier>> GetByNamesAsync(IEnumerable<string> supplierNames)
        {
            throw new NotImplementedException();

        }

        public Task UpdateAsync(Supplier value)
        {
           return Task.Run(()=>true);
        }

    }
}
