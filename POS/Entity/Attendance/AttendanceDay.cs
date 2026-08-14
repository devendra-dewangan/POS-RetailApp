namespace POS.Entity.Attendance
{
    public class AttendanceDay
    {
        public long Id { get; set; }

        public int EmployeeId { get; set; }
        public DateOnly Date { get; set; }
        public ICollection<AttendancePunch> Punches { get; set; } = [];
    }
}
