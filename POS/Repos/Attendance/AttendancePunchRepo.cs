using Microsoft.EntityFrameworkCore;
using POS.Data;
using POS.Entity.Attendance;

namespace POS.Repos.Attendance
{
    public class AttendancePunchRepo : IAttendancePunchRepo
    {
        private readonly AppDbContext _context;

        public AttendancePunchRepo( AppDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(AttendancePunch value)
        {
            await _context.AddAsync(value);
        }

        public Task DeleteAsync(AttendancePunch value)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<AttendancePunch>?> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<AttendancePunch?> GetByIDAsync(int id)
        {
            return await _context.AttendancePunches.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<AttendancePunch>> GetByAttendanceDayId(long id)
        {
            return await _context.AttendancePunches.Where(x => x.AttendanceDayId == id).OrderByDescending(x => x.PunchTime).ToArrayAsync();
        }

        public Task UpdateAsync(AttendancePunch value)
        {
            throw new NotImplementedException();
        }
    }
}
