namespace finap.Models
{
    public class Interaction
    {
        public int InteractionId { get; set; }

        public string Type { get; set; } = "Like"; // "Like" or "Unlike"
        public DateTime Created { get; set; } = DateTime.UtcNow;
        public DateTime? Updated { get; set; }

        // Foreign Keys
        public int UserId { get; set; }
        public int PostId { get; set; }

        // Navigation
        public User User { get; set; } = null!;
        public Post Post { get; set; } = null!;
        public bool IsLike { get; set; }
        public DateTime UpdatedAt { get; internal set; }
        public DateTime CreatedAt { get; internal set; }
    }
}
