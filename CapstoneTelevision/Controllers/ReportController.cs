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
    public class ReportController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ReportController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        //[Authorize("ReporterOnly")]
        public async Task<IActionResult> GetAllReports()
        {
            var reports = await _context.Reports
                .Select(r => new ReportDTO
                {
                    ReportId = r.ReportId,
                    Type = r.Type,
                    GeneratedDate = r.GeneratedDate,
                    Data = r.Data,
                    CreatedBy = r.CreatedBy
                })
                .ToListAsync();

            return Ok(reports);
        }
    }
}
