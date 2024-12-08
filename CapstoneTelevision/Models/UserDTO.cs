using System.ComponentModel.DataAnnotations;

namespace CapstoneTelevision.Models
{
    public class UserDTO
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        [MaxLength(50)]
        public string Username { get; set; }

        
        public string? Password { get; set; }

        [Required]
        [MaxLength(20)]
        public string Role { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
