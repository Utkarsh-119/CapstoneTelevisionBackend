using System.ComponentModel.DataAnnotations;

namespace CapstoneTelevision.Models
{
    public class Report
    {
        [Key]
        public int ReportId { get; set; }

        [Required]
        [MaxLength(50)]
        public string Type { get; set; }

        [Required]
        public DateTime GeneratedDate { get; set; }

        public string Data { get; set; }

        [MaxLength(50)]
        public string CreatedBy { get; set; }
    }
}
