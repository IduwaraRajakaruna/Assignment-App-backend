using System.ComponentModel.DataAnnotations;

namespace finap.Models
{
    public class Post
    {
        public int PostId { get; set; }

        [Required, MaxLength(200)]
        public string Title { get; set; } = string.Empty;

        [Required]
        public string Content { get; set; } = string.Empty;

        public DateTime Created { get; set; } = DateTime.UtcNow;
        public DateTime Updated { get; set; }

        // Foreign Key
        public int UserId { get; set; }

        // Navigation
        public required User Author { get; set; }

        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
        public ICollection<Interaction> Interactions { get; set; } = new List<Interaction>();
        public DateTime CreatedAt { get; internal set; }
        public DateTime UpdatedAt { get; internal set; }
    }
}
