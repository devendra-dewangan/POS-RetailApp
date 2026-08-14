using Microsoft.EntityFrameworkCore;
using POS.Data;
using POS.Entity;
using POS.Entity.Inovice;

namespace POS.Repos;

public class PurchaseItemRepo : IPurchaseItemRepo
{
    private AppDbContext _context;
    public PurchaseItemRepo(AppDbContext context)
    {
        _context = context;
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