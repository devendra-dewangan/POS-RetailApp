using Microsoft.EntityFrameworkCore;
using POS.Data;
using POS.Entity;
using POS.Entity.Inovice;

namespace POS.Repos
{
    public class InvoiceItemRepo : IInvoiceItemRepo
    {
        private AppDbContext _context;
        public InvoiceItemRepo(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }

        public async Task AddAsync(InvoiceItem value)
        {
            await _context.InvoiceItems.AddAsync(value);
        }

        public async Task AddBulkAsync(IEnumerable<InvoiceItem> values)
        {
            await _context.InvoiceItems.AddRangeAsync(values);

        }

        public Task DeleteAsync(InvoiceItem value)
        {
            return Task.Run(()=> true);
        }

        public async Task<IEnumerable<InvoiceItem>?> GetAllAsync()
        {
            return await _context.InvoiceItems.ToListAsync();
        }

        public Task<InvoiceItem?> GetByIDAsync(int id)
        {
            return _context.InvoiceItems.FirstOrDefaultAsync(x=>x.Id == id);
        }

        public Task UpdateAsync(InvoiceItem value)
        {
           return Task.Run(()=>true);
        }
    }
}
