namespace CapstoneTelevision.Models
{
    public class ScheduleDTO
    {
        public int ScheduleId { get; set; }
        public int ShowId { get; set; }
        public DateTime AirDate { get; set; }
        public string TimeSlot { get; set; }
        public int? AssignedEditorId { get; set; }
        public string Status { get; set; }
    }
}
