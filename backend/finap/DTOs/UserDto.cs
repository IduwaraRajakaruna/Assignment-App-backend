namespace finap.DTOs
{
    // For creating a new user (Admin only)
    public class CreateUserDto
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty; // plain, hash later
        public string Role { get; set; } = "Employee"; // default Employee
        public DateTime StartDate { get; set; } = DateTime.UtcNow;
    }

    // For returning user info
    public class UserDto
    {
        public int UserId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
    }
}
