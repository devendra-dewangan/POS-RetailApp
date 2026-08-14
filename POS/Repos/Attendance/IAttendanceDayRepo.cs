using POS.Entity.Attendance;

namespace POS.Repos.Attendance
{
    public interface IAttendanceDayRepo : IRepository<AttendanceDay>
    {
        Task<IEnumerable<AttendanceDay>> GetByDate(DateOnly date);
    }
}
