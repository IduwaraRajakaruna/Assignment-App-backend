using finap.DTOs;

namespace finap.DTOs
{
    // Request when liking/disliking a post
    public class CreateInteractionDto
    {
        public bool IsLike { get; set; } // true = Like, false = Dislike
    }

    // Response DTO
    public class InteractionDto
    {
        public int InteractionId { get; set; }
        public bool IsLike { get; set; }
        public DateTime CreatedAt { get; set; }

        // User info
        public required UserDto User { get; set; }
    }
}
