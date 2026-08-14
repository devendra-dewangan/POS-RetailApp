using Microsoft.EntityFrameworkCore;
using POS.Data;
using POS.Entity.Attendance;

namespace POS.Repos.Attendance
{
    public class AttendanceDayRepo : IAttendanceDayRepo
    {
        private readonly AppDbContext _context;

        public AttendanceDayRepo(AppDbContext appContext) 
        {
            _context = appContext;
        }
        public async Task AddAsync(AttendanceDay value)
        {
            await _context.AttendanceDays.AddAsync(value);
        }

        public async Task DeleteAsync(AttendanceDay value)
        {

            throw new NotImplementedException();
        }

        public async Task<IEnumerable<AttendanceDay>?> GetAllAsync()
        {
            return await _context.AttendanceDays.ToArrayAsync();
        }

        public Task<AttendanceDay?> GetByIDAsync(int id)
        {
            return _context.AttendanceDays.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<AttendanceDay>> GetByDate(DateOnly date)
        {
            return await _context.AttendanceDays.Where(x=> x.Date == date).ToArrayAsync();
        }

        public Task UpdateAsync(AttendanceDay value)
        {
            throw new NotImplementedException();
        }
    }
}
