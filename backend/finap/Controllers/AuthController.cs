using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using finap.Data;
using finap.Models;
using finap.DTOs;
using System.Security.Cryptography;
using System.Text;
using System;

namespace finap.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AuthController(ApplicationDbContext context)
        {
            _context = context;
        }

        // 🔹 Signup Endpoint
        [HttpPost("signup")]
        public async Task<IActionResult> Signup(SignupDto dto)
        {
            if (await _context.Users.AnyAsync(u => u.Email == dto.Email))
                return BadRequest("Email already exists.");

            var user = new User
            {
              
                Email = dto.Email,
                PasswordHash = HashPassword(dto.Password)
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok(new { message = "User registered successfully!" });
        }

        // 🔹 Login Endpoint
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == dto.Email);
            if (user == null) return Unauthorized("Invalid email or password.");

            if (!VerifyPassword(dto.Password, user.PasswordHash))
                return Unauthorized("Invalid email or password.");

            // Return success response with user data
            return Ok(new
            {
                message = "Login successful!",
                userId = user.UserId,
                name = user.Name,
                role = user.Role
            });
        }

        // 🔹 Helper Methods
        private string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(bytes);
        }

        private bool VerifyPassword(string password, string storedHash)
        {
            return HashPassword(password) == storedHash;
        }
    }
}
