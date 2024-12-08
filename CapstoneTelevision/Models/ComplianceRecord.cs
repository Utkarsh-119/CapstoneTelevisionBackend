using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CapstoneTelevision.Models
{
    public class ComplianceRecord
    {
        [Key]
        public int ComplianceId { get; set; }

        [Required]
        public int ShowId { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [MaxLength(100)]
        public string IssueType { get; set; }

        [MaxLength(100)]
        public string Resolution { get; set; }

        [MaxLength(100)]
        public string CheckedBy { get; set; }

        // Relationships
        [ForeignKey("ShowId")]
        public Show Show { get; set; }
    }
}
