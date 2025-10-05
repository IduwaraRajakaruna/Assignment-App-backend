using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using finap.DTOs;
using finap.Models;
using finap.Data;
using System.Linq;

namespace finap.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PostController(ApplicationDbContext context)
        {
            _context = context;
        }

        // CREATE POST
        [HttpPost]
        public IActionResult CreatePost([FromBody] CreatePostDto dto, int userId)
        {
            var author = _context.Users.Find(userId);
            if (author == null) return NotFound("User not found");

            var post = new Post
            {
                Title = dto.Title,
                Content = dto.Content,
                UserId = userId,
                Author = author,
                CreatedAt = DateTime.UtcNow
            };

            _context.Posts.Add(post);
            _context.SaveChanges();

            return Ok(new { message = "Post created", postId = post.PostId });
        }


        // GET ALL POSTS
        [HttpGet]
        public IActionResult GetPosts(string? authorName, string? sortBy)
        {
            var query = _context.Posts
                .Include(p => p.Author)
                .Include(p => p.Comments).ThenInclude(c => c.Author)
                .Include(p => p.Interactions)
                .AsQueryable();

            if (!string.IsNullOrEmpty(authorName))
                query = query.Where(p => p.Author.Name.Contains(authorName));

            if (sortBy == "recent")
                query = query.OrderByDescending(p => p.CreatedAt);
            else if (sortBy == "likes")
                query = query.OrderByDescending(p => p.Interactions.Count(i => i.IsLike));

            var posts = query.Select(p => new PostDto
            {
                PostId = p.PostId,
                Title = p.Title,
                Content = p.Content,
                CreatedAt = p.CreatedAt,
                UpdatedAt = p.UpdatedAt,
                Author = new UserDto
                {
                    UserId = p.Author.UserId,
                    Name = p.Author.Name,
                    Email = p.Author.Email,
                    Role = p.Author.Role,
                    StartDate = p.Author.StartDate
                },
                LikeCount = p.Interactions.Count(i => i.IsLike),
                DislikeCount = p.Interactions.Count(i => !i.IsLike),
                Comments = p.Comments.Select(c => new CommentDto
                {
                    CommentId = c.CommentId,
                    Content = c.Content,
                    CreatedAt = c.CreatedAt,
                    UpdatedAt = c.UpdatedAt,
                    Author = new UserDto
                    {
                        UserId = c.Author.UserId,
                        Name = c.Author.Name,
                        Email = c.Author.Email,
                        Role = c.Author.Role,
                        StartDate = c.Author.StartDate
                    }
                }).ToList()
            }).ToList();

            return Ok(posts);
        }

        // UPDATE POST
        [HttpPut("{id}")]
        public IActionResult UpdatePost(int id, [FromBody] CreatePostDto dto)
        {
            var post = _context.Posts.Find(id);
            if (post == null) return NotFound("Post not found");

            post.Title = dto.Title;
            post.Content = dto.Content;
            post.UpdatedAt = DateTime.UtcNow;

            _context.SaveChanges();

            return Ok(new { message = "Post updated" });
        }

        // DELETE POST
        [HttpDelete("{id}")]
        public IActionResult DeletePost(int id)
        {
            var post = _context.Posts.Find(id);
            if (post == null) return NotFound("Post not found");

            _context.Posts.Remove(post);
            _context.SaveChanges();

            return Ok(new { message = "Post deleted" });
        }
    }
}

