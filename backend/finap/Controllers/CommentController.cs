using Microsoft.AspNetCore.Mvc;
using finap.DTOs;
using finap.Models;
using finap.Data;


namespace finap.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CommentsController : ControllerBase
    {
        private readonly Data.ApplicationDbContext _context;

        public CommentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("{postId}")]
        public IActionResult AddComment(int postId, CreateCommentDto dto, int userId)
        {
            var comment = new Comment
            {
                Content = dto.Content,
                PostId = postId,
                UserId = userId,
                CreatedAt = DateTime.UtcNow
            };

            _context.Comments.Add(comment);
            _context.SaveChanges();

            return Ok(new { message = "Comment added" });
        }

        [HttpPut("{id}")]
        public IActionResult UpdateComment(int id, CreateCommentDto dto)
        {
            var comment = _context.Comments.Find(id);
            if (comment == null) return NotFound();

            comment.Content = dto.Content;
            comment.UpdatedAt = DateTime.UtcNow;

            _context.SaveChanges();
            return Ok(new { message = "Comment updated" });
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteComment(int id)
        {
            var comment = _context.Comments.Find(id);
            if (comment == null) return NotFound();

            _context.Comments.Remove(comment);
            _context.SaveChanges();
            return Ok(new { message = "Comment deleted" });
        }
    }
}
