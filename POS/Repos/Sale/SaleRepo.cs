using Microsoft.EntityFrameworkCore;
using POS.Data;
using POS.Entity.Inovice;

namespace POS.Repos
{
    public class SaleRepo : ISaleRepo
    {
        private AppDbContext _context;
        public SaleRepo(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }

        public async Task AddAsync(SaleInvoice value)
        {
            await _context.Sales.AddAsync(value);
        }

        public async Task AddBulkAsync(IEnumerable<SaleInvoice> values)
        {
            await _context.Sales.AddRangeAsync(values);

        }

        public Task DeleteAsync(SaleInvoice value)
        {
            return Task.Run(()=> true);
        }

        public async Task<IEnumerable<SaleInvoice>?> GetAllAsync()
        {
            return await _context.Sales.ToListAsync();
        }

        public Task<SaleInvoice?> GetByIDAsync(int id)
        {
            return _context.Sales.FirstOrDefaultAsync(x=>x.Id == id);
        }

        public Task UpdateAsync(SaleInvoice value)
        {
           return Task.Run(()=>true);
        }

        public async Task<IEnumerable<SaleInvoice>?> GetByInvoiceNumberAsync(string invoiceNumber)
        {
            return await _context.Sales
                .Where(p => p.Invoice.InvoiceNumber.Contains(invoiceNumber))
                .ToListAsync();
        }
    }
}
