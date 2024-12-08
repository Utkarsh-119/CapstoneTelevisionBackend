using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CapstoneTelevision.Models
{
    public class Show
    {
        [Key]
        public int ShowId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        [MaxLength(50)]
        public string Genre { get; set; }

        public TimeSpan Duration { get; set; }

        public DateTime Schedule { get; set; }

        [Range(0, 5)]
        public double Rating { get; set; }

        [Required]
        public int ProducerId { get; set; }
        public string Status { get; set; }

        // Relationships
        [ForeignKey("ProducerId")]
        public User Producer { get; set; }

        public ICollection<Content> Contents { get; set; }
        public ICollection<Advertisement> Advertisements { get; set; }
        public ICollection<MediaLibrary> MediaLibraries { get; set; }
        public ICollection<Schedule> Schedules { get; set; }
        public ICollection<ViewerFeedback> ViewerFeedbacks { get; set; }
        public ICollection<ComplianceRecord> ComplianceRecords { get; set; }
        public ICollection<Talent> Talents { get; set; }
        public ICollection<CostManagement> CostManagements { get; set; }
    }
}
