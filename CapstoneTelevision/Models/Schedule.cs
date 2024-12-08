using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CapstoneTelevision.Models
{
    public class Schedule
    {
        [Key]
        public int ScheduleId { get; set; }

        [Required]
        public int ShowId { get; set; }

        [Required]
        public DateTime AirDate { get; set; }

        [Required]
        [MaxLength(20)]
        public string TimeSlot { get; set; }

        public int? AssignedEditorId { get; set; }
        public string Status { get; set; }

        // Relationships
        [ForeignKey("ShowId")]
        public Show Show { get; set; }

        [ForeignKey("AssignedEditorId")]
        public User AssignedEditor { get; set; }
    }
}
