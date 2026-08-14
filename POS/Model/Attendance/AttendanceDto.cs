using POS.Entity.Attendance;

namespace POS.Model.Attendance
{
    public enum Status
    {
        Okay,
        Failed,
    }
    public record AddPunchRequest(
     int EmployeeId,
     DateTime PunchTime,
     PunchType Type,
     string Reason);

    public record EditPunchRequest(
        DateTime PunchTime,
        PunchType Type,
        string Reason);

    public record DeletePunchRequest(
        long PunchId,
        string Reason);

    public record PunchResponse(
        long PunchId,
        string Message);
    public record AttendanceResponce(
            string Message,
         AttendancePunch[] AttendancePunches,
         Status Status
         );
}
