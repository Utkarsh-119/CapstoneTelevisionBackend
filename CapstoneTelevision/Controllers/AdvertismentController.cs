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
    public class AdvertismentController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AdvertismentController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        //[Authorize("ChanelManagerOnly")]
        public async Task<IActionResult> GetAllAdvertisements()
        {
            var ads = await _context.Advertisements
                .Select(a => new AdvertisementDTO
                {
                    AdId = a.AdId,
                    ClientName = a.ClientName,
                    SlotTime = a.SlotTime,
                    Duration = a.Duration,
                    Rate = a.Rate
                })
                .ToListAsync();

            return Ok(ads);
        }
        // Post a new advertisement
        [HttpPost]
        //[Authorize(Roles ="Advertiser,Channel Manager")]
        public async Task<IActionResult> CreateAdvertisement([FromBody] AdvertisementDTO newAd)
        {
            // Validate input
            if (string.IsNullOrWhiteSpace(newAd.ClientName))
                return BadRequest("Client name is required.");

            var advertisement = new Advertisement
            {
                ClientName = newAd.ClientName,
                SlotTime = newAd.SlotTime,
                Duration = newAd.Duration,
                Rate = newAd.Rate,
                Status = "Scheduled" // Default status
            };

            _context.Advertisements.Add(advertisement);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAllAdvertisements), new { id = advertisement.AdId }, newAd);
        }

        // Update an existing advertisement
        [HttpPut("{adId}")]
        [Authorize("ChanelManagerOnly")]
        public async Task<IActionResult> UpdateAdvertisement(int adId, [FromBody] AdvertisementDTO updatedAd)
        {
            var advertisement = await _context.Advertisements.FindAsync(adId);
            if (advertisement == null)
            {
                return NotFound("Advertisement not found.");
            }

            // Update fields
            advertisement.ClientName = updatedAd.ClientName;
            advertisement.SlotTime = updatedAd.SlotTime;
            advertisement.Duration = updatedAd.Duration;
            advertisement.Rate = updatedAd.Rate;

            await _context.SaveChangesAsync();

            return Ok("Advertisement updated successfully.");
        }

        // Delete an advertisement by ID
        [HttpDelete("{adId}")]
        [Authorize("ChanelManagerOnly")]
        public async Task<IActionResult> DeleteAdvertisement(int adId)
        {
            var advertisement = await _context.Advertisements.FindAsync(adId);
            if (advertisement == null)
            {
                return NotFound("Advertisement not found.");
            }

            _context.Advertisements.Remove(advertisement);
            await _context.SaveChangesAsync();

            return Ok("Advertisement deleted successfully.");
        }
    }
}
