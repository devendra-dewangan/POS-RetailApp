using POS.Entity.Attendance;
using POS.Model.Attendance;

namespace POS.Services.Attendance
{
    public interface IAttendanceService
    {
        Task<int> AddPunchAsync(
                AddPunchRequest request,
                int employeeId);
        Task<IEnumerable<AttendanceDay>> GetAttendanceAsync(DateOnly date);
        Task<IEnumerable<AttendancePunch>> GetAttendancePunchesAsync(int attendanceDayId);
    }
}
