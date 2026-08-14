using Microsoft.EntityFrameworkCore;
using POS.Data;
using POS.Entity;

namespace POS.Repos
{
    public class ProductBatchRepo : IProductBatchRepo
    {
        private AppDbContext _context;
        public ProductBatchRepo(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }

        public async Task AddAsync(ProductBatch value)
        {
            await _context.Batches.AddAsync(value);
        }

        public async Task AddBulkAsync(IEnumerable<ProductBatch> values)
        {
            await _context.Batches.AddRangeAsync(values);

        }

        public Task DeleteAsync(ProductBatch value)
        {
            return Task.Run(()=> true);
        }

        public async Task<IEnumerable<ProductBatch>?> GetAllAsync()
        {
            return await _context.Batches.ToListAsync();
        }

        public Task<ProductBatch?> GetByIDAsync(int id)
        {
            return _context.Batches.FirstOrDefaultAsync(x=>x.Id == id);
        }

        public async Task<IEnumerable<ProductBatch>?> GetByPurchaseIdAsync(int purchaseId)
        {
            return await _context.Batches
                .Where(b => b.InvoiceItemId == purchaseId)
                .ToListAsync();
        }

        public Task UpdateAsync(ProductBatch value)
        {
           return Task.Run(()=>true);
        }
    }
}
