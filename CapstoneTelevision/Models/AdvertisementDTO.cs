namespace CapstoneTelevision.Models
{
    public class AdvertisementDTO
    {
        public int AdId { get; set; } 
        public string ClientName { get; set; }
        public TimeSpan SlotTime { get; set; }
        public TimeSpan Duration { get; set; }
        public decimal Rate { get; set; }
    }
}
