using System.ComponentModel.DataAnnotations;

namespace CapstoneTelevision.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        [MaxLength(50)]
        public string Username { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        [Required]
        [MaxLength(20)]
        public string Role { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public bool IsActive { get; set; }

        // Relationships
        public ICollection<Show> Shows { get; set; }
        public ICollection<MediaLibrary> MediaLibraries { get; set; }
        public ICollection<Notification> Notifications { get; set; }
    }
}
