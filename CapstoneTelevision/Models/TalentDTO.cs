namespace CapstoneTelevision.Models
{
    public class TalentDTO
    {
        public int TalentId { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
        public int? ShowId { get; set; }
        public DateTime ContractStartDate { get; set; }
        public DateTime ContractEndDate { get; set; }
        public string Status { get; set; }
    }
}
