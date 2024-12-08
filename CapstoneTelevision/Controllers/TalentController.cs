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
    public class TalentController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TalentController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        //[Authorize("DirectorOnly")]
        public async Task<IActionResult> GetAllTalents()
        {
            var talents = await _context.Talents
                .Select(t => new TalentDTO
                {
                    TalentId = t.TalentId,
                    Name = t.Name,
                    Role = t.Role,
                    ShowId = t.ShowId,
                    ContractStartDate = t.ContractStartDate,
                    ContractEndDate = t.ContractEndDate,
                    Status = t.Status
                })
                .ToListAsync();

            return Ok(talents);
        } 
    }
}
