namespace POS.Entity.Attendance
{
    public enum PunchType
    {
        In,
        Out
    }
    public enum PunchSource
    {
        Employee,
        Manager,
        Biometric,
        System
    }
    public class AttendancePunch
    {
        public long Id { get; set; }

        public long AttendanceDayId { get; set; }
        public AttendanceDay AttendanceDay { get; set; } = null!;

        public DateTime PunchTime { get; set; }

        public PunchType Type { get; set; } // IN / OUT

        public PunchSource Source { get; set; } = PunchSource.Employee; // Biometric, Manual, Mobile

        public bool IsDeleted { get; set; }

        public string? EditReason { get; set; }
    }
}
