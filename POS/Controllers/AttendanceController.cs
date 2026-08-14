using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using POS.Entity.Attendance;
using POS.Model.Attendance;
using POS.Services.Attendance;

namespace POS.Controllers
{
    [ApiController]
    [Route("api/attendance")]
    public class AttendanceController : ControllerBase
    {
        private readonly IAttendanceService _attendanceService;

        public AttendanceController(IAttendanceService attendanceService)
        {
            _attendanceService = attendanceService;
        }

        // Employee scans/taps
        [HttpPost("punchIn")]
        public async Task<IActionResult> PunchIn()
        {
            var employeeId = GetEmployeeId();

            var result = await _attendanceService
                        .AddPunchAsync(new AddPunchRequest(employeeId,DateTime.Now,PunchType.In,"")
                        , employeeId);
            return Ok(result);
        }

        [HttpPost("punchOut")]
        public async Task<IActionResult> PunchOut()
        {
            var employeeId = GetEmployeeId();

            var result = await _attendanceService
                        .AddPunchAsync(new AddPunchRequest(employeeId, DateTime.Now, PunchType.Out, "")
                        , employeeId);
            return Ok(result);
        }

        // Get attendance for a particular day
        [HttpGet("{employeeId:int}/{date}")]
        public async Task<IActionResult> GetAttendance(
            int employeeId,
            DateOnly date)
        {
            return Ok();
        }

        // Admin manually adds a punch
        [Authorize(Roles = "Admin,HR")]
        [HttpPost("punches")]
        public async Task<IActionResult> AddPunch(
            [FromBody] AddPunchRequest request)
        {
            var adminId = GetEmployeeId();

            var result =
                await _attendanceService.AddPunchAsync(
                    request,
                    adminId);

            return Ok(result);
        }

        // Admin edits an existing punch
        [Authorize(Roles = "Admin,HR")]
        [HttpPut("punches/{punchId:long}")]
        public async Task<IActionResult> EditPunch(
            long punchId,
            [FromBody] EditPunchRequest request)
        {
            var adminId = GetEmployeeId();

            return NoContent();
        }

        // Admin deletes/cancels a punch
        [Authorize(Roles = "Admin,HR")]
        [HttpDelete("punches/{punchId:long}")]
        public async Task<IActionResult> DeletePunch(
            long punchId,
            [FromBody] DeletePunchRequest request)
        {
            var adminId = GetEmployeeId();

            return NoContent();
        }

        private int GetEmployeeId()
        {
            return int.Parse(
                User.FindFirst("employeeId")!.Value);
        }
    }
}
