using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CapstoneTelevision.Models
{
    public class Notification
    {
        [Key]
        public int NotificationId { get; set; }

        [Required]
        [MaxLength(50)]
        public string Type { get; set; }

        [Required]
        [MaxLength(500)]
        public string Message { get; set; }

        [Required]
        public DateTime Timestamp { get; set; }

        public int? UserId { get; set; }

        // Relationships
        [ForeignKey("UserId")]
        public User User { get; set; }
    }
}
