using CapstoneTelevision.Data;
using CapstoneTelevision.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CapstoneTelevision.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SchdulesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public SchdulesController(AppDbContext context)
        {
            _context = context;
        }

        // Get all schedules
        [HttpGet]
        //[Authorize("ChanelManagerOnly")]
        public async Task<IActionResult> GetAllSchedules()
        {
            var schedules = await _context.Schedules
                .Select(s => new ScheduleDTO
                {
                    ScheduleId = s.ScheduleId,
                    ShowId = s.ShowId,
                    AirDate = s.AirDate,
                    TimeSlot = s.TimeSlot,
                    AssignedEditorId = s.AssignedEditorId,
                    Status = s.Status
                })
                .ToListAsync();

            return Ok(schedules);
        }
        [HttpDelete("{scheduleId}")]
        //[Authorize("ChanelManagerOnly")]
        public async Task<IActionResult> DeleteSchedule(int scheduleId)
        {
            var schedule = await _context.Schedules.FindAsync(scheduleId);
            if (schedule == null)
            {
                return NotFound("Schedule not found.");
            }

            // Remove the schedule
            _context.Schedules.Remove(schedule);
            await _context.SaveChangesAsync();

            return Ok("Schedule deleted successfully.");
        }
        // Update an existing schedule
        [HttpPut("{scheduleId}")]
        //[Authorize("ChanelManagerOnly")]
        public async Task<IActionResult> UpdateSchedule(int scheduleId, [FromBody] ScheduleDTO scheduleDTO)
        {
            var existingSchedule = await _context.Schedules.FindAsync(scheduleId);
            if (existingSchedule == null)
            {
                return NotFound("Schedule not found.");
            }

            // Update the schedule fields
            existingSchedule.ShowId = scheduleDTO.ShowId;
            existingSchedule.AirDate = scheduleDTO.AirDate;
            existingSchedule.TimeSlot = scheduleDTO.TimeSlot;
            existingSchedule.AssignedEditorId = scheduleDTO.AssignedEditorId;
            existingSchedule.Status = scheduleDTO.Status;

            // Save changes to the database
            await _context.SaveChangesAsync();

            return Ok("Schedule updated successfully.");
        }
    }
}
