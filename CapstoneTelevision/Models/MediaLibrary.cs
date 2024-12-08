using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CapstoneTelevision.Models
{
    public class MediaLibrary
    {
        [Key]
        public int MediaId { get; set; }

        [Required]
        [MaxLength(255)]
        public string FileName { get; set; }

        [Required]
        [MaxLength(50)]
        public string Type { get; set; }

        public DateTime UploadedDate { get; set; }

        [MaxLength(255)]
        public string Tags { get; set; }

        public int? ShowId { get; set; }
        public int? UploaderId { get; set; }

        // Relationships
        [ForeignKey("ShowId")]
        public Show Show { get; set; }
        [ForeignKey("UploaderId")]
        public User Uploader { get; set; }
    }
}
