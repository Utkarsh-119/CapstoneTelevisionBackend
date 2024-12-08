namespace CapstoneTelevision.Models
{
    public class ComplianceRecordDTO
    {
        public int ComplianceId { get; set; } 
        public int ShowId { get; set; }
        public DateTime Date { get; set; }
        public string IssueType { get; set; }
        public string Resolution { get; set; }
        public string CheckedBy { get; set; }
    }
}
