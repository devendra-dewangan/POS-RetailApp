using POS.Entity.Attendance;
using POS.Model.Attendance;
using POS.Repos;

namespace POS.Services.Attendance
{
    public class AttendanceService : IAttendanceService
    {
        private readonly IUnitOfWork _unitOfWork;
        private ILogger<AttendanceService> _logger;
        public AttendanceService(ILogger<AttendanceService> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }
        public async Task<int> AddPunchAsync(
                AddPunchRequest request,
                int employeeId)
        {
            var date = DateOnly.FromDateTime(DateTime.Now);

            var attendance = (await GetAttendanceAsync(date))
                .FirstOrDefault(x => x.EmployeeId == employeeId);

            if (attendance == null)
            {
                if (request.Type != PunchType.In)
                    throw new InvalidOperationException(
                        "First punch must be IN.");

                attendance = new AttendanceDay
                {
                    EmployeeId = employeeId,
                    Date = date
                };

                await _unitOfWork.AttendanceDays.AddAsync(attendance);
            }
            else
            {
                var punches =
                    await _unitOfWork.AttendancePunches
                        .GetByAttendanceDayId(attendance.Id);

                var lastPunch = punches
                    .OrderByDescending(x => x.PunchTime)
                    .FirstOrDefault();

                if (lastPunch != null)
                {
                    if (request.PunchTime <= lastPunch.PunchTime)
                        throw new InvalidOperationException(
                            "Punch time must be after the previous punch.");

                    if (lastPunch.Type == request.Type)
                    {
                        throw new InvalidOperationException(
                            $"After {lastPunch.Type}, the next punch must be " +
                            $"{(lastPunch.Type == PunchType.In
                                ? PunchType.Out
                                : PunchType.In)}.");
                    }
                }
                else if (request.Type != PunchType.In)
                {
                    throw new InvalidOperationException(
                        "First punch must be IN.");
                }
            }

            var punch = new AttendancePunch
            {
                AttendanceDay = attendance,
                PunchTime = request.PunchTime,
                Type = request.Type
            };

            await _unitOfWork.AttendancePunches.AddAsync(punch);

            return await _unitOfWork.CommitAsync();
        }


        public async Task<IEnumerable<AttendanceDay>> GetAttendanceAsync(DateOnly date)
        {
            return await _unitOfWork.AttendanceDays.GetByDate(date);
        }

        public async Task<IEnumerable<AttendancePunch>> GetAttendancePunchesAsync(int attendanceDayId)
        {
            return await _unitOfWork.AttendancePunches.GetByAttendanceDayId(attendanceDayId);
        }
    }
}