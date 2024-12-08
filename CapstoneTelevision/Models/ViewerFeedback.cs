using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CapstoneTelevision.Models
{
    public class ViewerFeedback
    {
        [Key]
        public int FeedbackId { get; set; }

        [Required]
        public int ShowId { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        [MaxLength(1000)]
        public string Feedback { get; set; }

        [Range(0, 5)]
        public double Rating { get; set; }

        [MaxLength(50)]
        public string SubmittedBy { get; set; }

        // Relationships
        [ForeignKey("ShowId")]
        public Show Show { get; set; }
    }
}
