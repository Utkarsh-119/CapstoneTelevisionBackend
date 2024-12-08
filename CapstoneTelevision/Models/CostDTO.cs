namespace CapstoneTelevision.Models
{
    public class CostDTO
    {
        public int CostId { get; set; } 
        public int ShowId { get; set; }
        public string ExpenseType { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string ResponsiblePerson { get; set; }
    }
}
