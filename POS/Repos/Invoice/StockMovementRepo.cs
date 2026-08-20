using POS.Data;
using POS.Entity.Product;

namespace POS.Repos.Invoice
{
    public class StockMovementRepo : IStockMovementRepo
    {
        private readonly AppDbContext _context;
        public StockMovementRepo(AppDbContext context)
        {
            _context = context;
        }

        public Task AddBulkAsync(IEnumerable<StockMovement> values)
        {
            return _context.StockMovements.AddRangeAsync(values);
        }
    }
}
