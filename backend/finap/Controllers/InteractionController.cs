using Microsoft.AspNetCore.Mvc;
using finap.DTOs;
using finap.Data;
using finap.Models;

namespace finap.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InteractionsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public InteractionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("{postId}")]
        public IActionResult AddInteraction(int postId, CreateInteractionDto dto, int userId)
        {
            var existing = _context.Interactions
                .FirstOrDefault(i => i.PostId == postId && i.UserId == userId);

            if (existing != null)
            {
                existing.IsLike = dto.IsLike;
                existing.UpdatedAt = DateTime.UtcNow;
            }
            else
            {
                var interaction = new Interaction
                {
                    PostId = postId,
                    UserId = userId,
                    IsLike = dto.IsLike,
                    CreatedAt = DateTime.UtcNow
                };
                _context.Interactions.Add(interaction);
            }

            _context.SaveChanges();
            return Ok(new { message = dto.IsLike ? "Liked" : "Disliked" });
        }
    }
}
