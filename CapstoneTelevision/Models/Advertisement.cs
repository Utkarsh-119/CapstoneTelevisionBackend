using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CapstoneTelevision.Models
{
    public class Advertisement
    {
        [Key]
        public int AdId { get; set; }

        [Required]
        [MaxLength(100)]
        public string ClientName { get; set; }

        public TimeSpan SlotTime { get; set; }

        public TimeSpan Duration { get; set; }

        public decimal Rate { get; set; }

        public int? AssignedShowId { get; set; }
        public string Status { get; set; }

        // Relationships
        [ForeignKey("AssignedShowId")]
        public Show AssignedShow { get; set; }
    }
}
