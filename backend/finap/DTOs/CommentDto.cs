using finap.DTOs;

namespace finap.DTOs
{
    // Request when creating/updating a comment
    public class CreateCommentDto
    {
        public string Content { get; set; } = string.Empty;
    }

    // Response DTO
    public class CommentDto
    {
        public int CommentId { get; set; }
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        // Author info
        public required UserDto Author { get; set; }
    }
}
