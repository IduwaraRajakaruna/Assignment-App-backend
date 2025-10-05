using finap.DTOs;

namespace finap.DTOs
{
    // Request when creating/updating a post
    public class CreatePostDto
    {
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
    }

    // Response DTO for showing posts
    public class PostDto
    {
        public int PostId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        // Author info
        public required UserDto Author { get; set; }

        // Counts
        public int LikeCount { get; set; }
        public int DislikeCount { get; set; }

        // Nested comments
        public List<CommentDto> Comments { get; set; } = new List<CommentDto>();
    }
}
