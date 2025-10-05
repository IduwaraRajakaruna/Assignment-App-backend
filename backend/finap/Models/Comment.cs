using System.ComponentModel.DataAnnotations;

namespace finap.Models
{
    public class Comment
    {
        public int CommentId { get; set; }

        [Required]
        public string Content { get; set; } = string.Empty;

        public DateTime Created { get; set; } = DateTime.UtcNow;
        public DateTime? Updated { get; set; }

        // Foreign Keys
        public int UserId { get; set; }
        public int PostId { get; set; }

        // Navigation
        public User Author { get; set; } = null!;
        public Post Post { get; set; } = null!;
        public DateTime CreatedAt { get; internal set; }
        public DateTime? UpdatedAt { get; internal set; }
    }
}
