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
    public class ShowsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ShowsController(AppDbContext context)
        {
            _context = context;
        }

        // Get all shows
        [HttpGet]
        //[Authorize("ChanelManagerOnly")]
        public async Task<IActionResult> GetAllShows()
        {
            var shows = await _context.Shows
                .Select(s => new ShowDTO
                {
                    ShowId = s.ShowId,
                    Title = s.Title,
                    Genre = s.Genre,
                    Duration = s.Duration
                })
                .ToListAsync();

            return Ok(shows);
        }

        // Get shows by producer ID
        [HttpGet("producer/{producerId}")]
        //[Authorize("ChanelManagerOnly")]
        public async Task<IActionResult> GetShowsByProducer(int producerId)
        {
            var shows = await _context.Shows
                .Where(s => s.ProducerId == producerId)
                .Select(s => new ShowDTO
                {
                    ShowId = s.ShowId,
                    Title = s.Title,
                    Genre = s.Genre,
                    Duration = s.Duration
                })
                .ToListAsync();

            if (!shows.Any())
            {
                return NotFound("No shows found for the given producer ID.");
            }

            return Ok(shows);
        }

        // Delete a show
        [HttpDelete("{showId}")]
        //[Authorize("ChanelManagerOnly")]
        public async Task<IActionResult> DeleteShow(int showId)
        {
            var show = await _context.Shows.FindAsync(showId);
            if (show == null)
            {
                return NotFound("Show not found.");
            }

            _context.Shows.Remove(show);
            await _context.SaveChangesAsync();

            return Ok("Show deleted successfully.");
        }

        // Update an existing show
        [HttpPut("{showId}")]
        //[Authorize("ChanelManagerOnly")]
        public async Task<IActionResult> UpdateShow(int showId, [FromBody] ShowDTO updatedShow)
        {
            var existingShow = await _context.Shows.FindAsync(showId);
            if (existingShow == null)
            {
                return NotFound("Show not found.");
            }

            // Update fields
            existingShow.Title = updatedShow.Title;
            existingShow.Genre = updatedShow.Genre;
            existingShow.Duration = updatedShow.Duration;

            await _context.SaveChangesAsync();

            return Ok("Show updated successfully.");
        }

        
        // Create a new show
        [HttpPost]
        //[Authorize("ChanelManagerOnly")]
        public async Task<IActionResult> CreateShow([FromBody] ShowDTO newShow)
        {
            var show = new Show
            {
                Title = newShow.Title,
                Genre = newShow.Genre,
                Duration = newShow.Duration,
                Schedule = DateTime.Now, // Default schedule
                ProducerId = 1, // Set producer ID as needed
                Rating = 0,
                Status = "NotScheduled"
            };

            _context.Shows.Add(show);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAllShows), new { id = show.ShowId }, new ShowDTO
            {
                ShowId = show.ShowId,
                Title = show.Title,
                Genre = show.Genre,
                Duration = show.Duration
            });
        }
    }
}
