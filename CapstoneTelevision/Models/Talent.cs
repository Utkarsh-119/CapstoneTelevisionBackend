using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CapstoneTelevision.Models
{
    public class Talent
    {
        [Key]
        public int TalentId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MaxLength(50)]
        public string Role { get; set; }

        public int? ShowId { get; set; }

        public DateTime ContractStartDate { get; set; }
        public DateTime ContractEndDate { get; set; }

        public string Status { get; set; }

        // Relationships
        [ForeignKey("ShowId")]
        public Show Show { get; set; }
    }
}
