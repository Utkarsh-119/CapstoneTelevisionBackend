using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CapstoneTelevision.Models
{
    public class CostManagement
    {
        [Key]
        public int CostId { get; set; }

        [Required]
        public int ShowId { get; set; }

        [Required]
        [MaxLength(50)]
        public string ExpenseType { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [MaxLength(100)]
        public string ResponsiblePerson { get; set; }

        // Relationships
        [ForeignKey("ShowId")]
        public Show Show { get; set; }
    }
}
