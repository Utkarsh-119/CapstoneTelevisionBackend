namespace CapstoneTelevision.Models
{
    public class ViewerFeedbackDTO
    {
        public int FeedbackId { get; set; }
        public int ShowId { get; set; }
        public DateTime Date { get; set; }
        public string Feedback { get; set; }
        public double Rating { get; set; }
        public string SubmittedBy { get; set; }
    }
}
