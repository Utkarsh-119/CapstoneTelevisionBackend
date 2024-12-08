using CapstoneTelevision.Data;
using CapstoneTelevision.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CapstoneTelevision.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ViewerFeedbackController : ControllerBase
    {

        private readonly AppDbContext _context;

        public ViewerFeedbackController(AppDbContext context)
        {
            _context = context;
        }
        // Get all feedbacks
        [HttpGet]
        public async Task<IActionResult> GetAllFeedbacks()
        {
            var feedbacks = await _context.ViewerFeedbacks
                .Select(f => new ViewerFeedbackDTO
                {
                    FeedbackId = f.FeedbackId,
                    ShowId = f.ShowId,
                    Date = f.Date,
                    Feedback = f.Feedback,
                    Rating = f.Rating,
                    SubmittedBy = f.SubmittedBy
                })
                .ToListAsync();

            return Ok(feedbacks);
        }
    }
}
