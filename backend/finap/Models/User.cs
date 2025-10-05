using Microsoft.Extensions.Hosting;
using Microsoft.VisualBasic;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using finap.Models;
using finap.DTOs;

namespace finap.Models
{
    public class User
    {
        public int UserId { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required, EmailAddress, MaxLength(150)]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string PasswordHash { get; set; } = string.Empty;

        [Required]
        [MaxLength(20)]
        public string Role { get; set; } = "Employee"; // "Admin" or "Employee"

        [Required]
        public DateTime StartDate { get; set; } = DateTime.UtcNow;

        // Navigation
        public ICollection<Post> Posts { get; set; } = new List<Post>();
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
        public ICollection<Interaction> Interactions { get; set; } = new List<Interaction>();

        public static implicit operator User(UserDto v)
        {
            throw new NotImplementedException();
        }
    }
}
