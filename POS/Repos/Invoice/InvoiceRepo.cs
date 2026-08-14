
using Microsoft.EntityFrameworkCore;
using POS.Data;
using POS.Entity.Inovice;

namespace POS.Repos.Invoice
{
    public class InvoiceRepo : IInvoiceRepo
    {
        private AppDbContext _context;

        public InvoiceRepo(AppDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Entity.Inovice.Invoice value)
        {
            await _context.Invoices.AddAsync(value);
        }

        public async Task AddBulkAsync(IEnumerable<Entity.Inovice.Invoice> values)
        {
            await _context.Invoices.AddRangeAsync(values);
        }

        public async Task DeleteAsync(Entity.Inovice.Invoice value)
        {
            _context.Invoices.Remove(value);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Entity.Inovice.Invoice>?> GetAllAsync()
        {
            return await _context.Invoices.ToArrayAsync();
        }

        public async Task<Entity.Inovice.Invoice?> GetByIDAsync(int id)
        {
            return await _context.Invoices.FindAsync(id);
        }

        public async Task<IEnumerable<Entity.Inovice.Invoice>> GetInvoiceByInvoiceNumber(string invoiceNumber)
        {
            return await _context.Invoices.Where(i => i.InvoiceNumber == invoiceNumber).ToListAsync();
        }

        public async Task UpdateAsync(Entity.Inovice.Invoice value)
        {
            _context.Invoices.Update(value);
            await _context.SaveChangesAsync();
        }
    }
}