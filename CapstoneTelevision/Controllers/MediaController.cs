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
    public class MediaController : ControllerBase
    {
        private readonly AppDbContext _context;

        public MediaController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        //[Authorize("ContentProducerOnly")]
        public async Task<IActionResult> GetAllMedia()
        {
            var mediaList = await _context.MediaLibraries
                .Select(m => new MediaLibraryDTO
                {
                    MediaId = m.MediaId,
                    FileName = m.FileName,
                    Type = m.Type,
                    UploadedDate = m.UploadedDate,
                    Tags = m.Tags
                })
                .ToListAsync();

            return Ok(mediaList);
        }
        [HttpPost]
        //[Authorize("ContentProducerOnly")]
        public async Task<IActionResult> CreateMedia([FromBody] MediaLibraryDTO newMedia)
        {
            // Validate input
            if (string.IsNullOrWhiteSpace(newMedia.FileName) || string.IsNullOrWhiteSpace(newMedia.Type))
                return BadRequest("FileName and Type are required.");

            var media = new MediaLibrary
            {
                FileName = newMedia.FileName,
                Type = newMedia.Type,
                UploadedDate = newMedia.UploadedDate,
                Tags = newMedia.Tags
            };

            _context.MediaLibraries.Add(media);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAllMedia), new { id = media.MediaId }, newMedia);
        }

        // Update existing media
        [HttpPut("{mediaId}")]
        [Authorize("ContentProducerOnly")]
        public async Task<IActionResult> UpdateMedia(int mediaId, [FromBody] MediaLibraryDTO updatedMedia)
        {
            var existingMedia = await _context.MediaLibraries.FindAsync(mediaId);
            if (existingMedia == null)
            {
                return NotFound("Media not found.");
            }

            // Update fields
            existingMedia.FileName = updatedMedia.FileName;
            existingMedia.Type = updatedMedia.Type;
            existingMedia.UploadedDate = updatedMedia.UploadedDate;
            existingMedia.Tags = updatedMedia.Tags;

            await _context.SaveChangesAsync();

            return Ok("Media updated successfully.");
        }

        // Delete media by MediaId
        [HttpDelete("{mediaId}")]
        [Authorize("ContentProducerOnly")]
        public async Task<IActionResult> DeleteMedia(int mediaId)
        {
            var media = await _context.MediaLibraries.FindAsync(mediaId);
            if (media == null)
            {
                return NotFound("Media not found.");
            }

            _context.MediaLibraries.Remove(media);
            await _context.SaveChangesAsync();

            return Ok("Media deleted successfully.");
        }
    }
}
