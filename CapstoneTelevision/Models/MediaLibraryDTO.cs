namespace CapstoneTelevision.Models
{
    public class MediaLibraryDTO
    {
        public int MediaId { get; set; } 
        public string FileName { get; set; }
        public string Type { get; set; }
        public DateTime UploadedDate { get; set; }
        public string Tags { get; set; }
    }
}
