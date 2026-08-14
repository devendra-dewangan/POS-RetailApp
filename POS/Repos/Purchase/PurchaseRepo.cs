using Microsoft.EntityFrameworkCore;
using POS.Data;
using POS.Entity.Inovice;

namespace POS.Repos
{
    public class PurchaseRepo : IPurchaseRepo
    {
        private readonly AppDbContext _context;
        public PurchaseRepo(AppDbContext context)
        {
            _context = context;          
        }

        public async Task AddAsync(PurchaseInvoice value)
        {
            await _context.Purchases.AddAsync(value);
        }

        public async Task AddBulkAsync(IEnumerable<PurchaseInvoice> values)
        {
            await _context.Purchases.AddRangeAsync(values);
        }

        public Task DeleteAsync(PurchaseInvoice value)
        {
            return Task.Run(()=>true);
        }

        public async Task<IEnumerable<PurchaseInvoice>?> GetAllAsync()
        {
            return await _context.Purchases.ToListAsync();
        }

        public async Task<PurchaseInvoice?> GetByIDAsync(int id)
        {
            return await _context.Purchases.FirstOrDefaultAsync(x=> x.Id == id);
        }

        public async Task<IEnumerable<PurchaseInvoice>?> GetByInvoiceNumberAsync(string invoiceNumber)
        {
            return await _context.Purchases
                .Where(p => p.Invoice.InvoiceNumber.Contains(invoiceNumber))
                .ToListAsync();
        }

        public async Task<IEnumerable<PurchaseInvoice>?> GetByInvoiceNumbersAsync(IEnumerable<string> invoiceNumbers)
        {
            if (invoiceNumbers == null || invoiceNumbers.Any() == false)
                return [];

            return await _context.Purchases
                .Where(p => invoiceNumbers.Contains(p.Invoice.InvoiceNumber))
                .ToListAsync();
        }

        public Task UpdateAsync(PurchaseInvoice value)
        {
            return Task.Run(()=>true);
        }
    }
}