using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CapstoneTelevision.Models
{
    public class Content
    {
        [Key]
        public int ContentId { get; set; }

        [Required]
        public int ShowId { get; set; }

        public int EpisodeNumber { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        public DateTime AirDate { get; set; }

        public int? EditorId { get; set; }
        public string Status { get; set; }

        // Relationships
        [ForeignKey("ShowId")]
        public Show Show { get; set; }
        [ForeignKey("EditorId")]
        public User Editor { get; set; }
    }
}
