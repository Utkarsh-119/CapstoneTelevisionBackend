using CapstoneTelevision.Data;
using CapstoneTelevision.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CapstoneTelevision.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComplianceRecordController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ComplianceRecordController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllComplianceRecords()
        {
            var complianceRecords = await _context.ComplianceRecords
                .Select(c => new ComplianceRecordDTO
                {
                    ComplianceId = c.ComplianceId,
                    ShowId = c.ShowId,
                    Date = c.Date,
                    IssueType = c.IssueType,
                    Resolution = c.Resolution,
                    CheckedBy = c.CheckedBy
                })
                .ToListAsync();

            return Ok(complianceRecords);
        }

        // Add a new compliance record
        [HttpPost]
        public async Task<IActionResult> AddComplianceRecord([FromBody] ComplianceRecordDTO newCompliance)
        {
            // Validate input
            if (string.IsNullOrWhiteSpace(newCompliance.IssueType) || string.IsNullOrWhiteSpace(newCompliance.Resolution))
                return BadRequest("IssueType and Resolution are required.");

            var complianceRecord = new ComplianceRecord
            {
                ShowId = newCompliance.ShowId,
                Date = newCompliance.Date,
                IssueType = newCompliance.IssueType,
                Resolution = newCompliance.Resolution,
                CheckedBy = newCompliance.CheckedBy
            };

            _context.ComplianceRecords.Add(complianceRecord);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAllComplianceRecords), new { id = complianceRecord.ComplianceId }, newCompliance);
        }
    }
}
