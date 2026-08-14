using POS.Entity.Attendance;

namespace POS.Repos.Attendance
{
    public interface IAttendancePunchRepo : IRepository<AttendancePunch>
    {
        Task<IEnumerable<AttendancePunch>> GetByAttendanceDayId(long id);
    }
}
