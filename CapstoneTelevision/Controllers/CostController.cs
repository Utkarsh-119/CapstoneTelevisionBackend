using CapstoneTelevision.Data;
using CapstoneTelevision.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace CapstoneTelevision.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CostController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CostController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllCosts()
        {
            var costs = await _context.CostManagements
                .Select(c => new CostDTO
                {
                    CostId = c.CostId,
                    ShowId = c.ShowId,
                    ExpenseType = c.ExpenseType,
                    Amount = c.Amount,
                    Date = c.Date,
                    ResponsiblePerson = c.ResponsiblePerson
                })
                .ToListAsync();

            return Ok(costs);
        }

        // Create a new cost
        [HttpPost]
        public async Task<IActionResult> CreateCost([FromBody] CostDTO newCost)
        {
            // Validate input
            if (string.IsNullOrWhiteSpace(newCost.ExpenseType) || newCost.Amount <= 0)
                return BadRequest("ExpenseType and valid Amount are required.");

            var cost = new CostManagement
            {
                ShowId = newCost.ShowId,
                ExpenseType = newCost.ExpenseType,
                Amount = newCost.Amount,
                Date = newCost.Date,
                ResponsiblePerson = newCost.ResponsiblePerson
            };

            _context.CostManagements.Add(cost);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAllCosts), new { id = cost.CostId }, newCost);
        }
    }
}
