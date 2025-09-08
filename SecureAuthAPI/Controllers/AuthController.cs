using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SecureAuthAPI.Data;
using SecureAuthAPI.Helpers;
using SecureAuthAPI.Models;

namespace SecureAuthAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")] // base route = /api/auth
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _db;
        private readonly JwtService _jwt;

        public AuthController(AppDbContext db, JwtService jwt)
        {
            _db = db;
            _jwt = jwt;
        }

        // 🔹 User login with username/password
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest req)
        {
            if (req == null || string.IsNullOrWhiteSpace(req.Username) || string.IsNullOrWhiteSpace(req.Password))
            {
                return BadRequest(new { error = "Username and password are required." });
            }

            var user = await _db.Users.FirstOrDefaultAsync(u => u.Username == req.Username);

            if (user == null || !BCrypt.Net.BCrypt.Verify(req.Password, user.PasswordHash))
            {
                return Unauthorized(new { error = "Invalid credentials" });
            }

            var token = _jwt.GenerateToken(user);
            return Ok(new
            {
                token,
                expires_in = 3600,
                user = new { user.Id, user.Username, user.Roles }
            });
        }

        // 🔹 OAuth login (mock implementation)
        [HttpPost("oauth")]
        public IActionResult OAuthLogin([FromBody] OAuthRequest req)
        {
            if (req == null || string.IsNullOrWhiteSpace(req.Provider) || string.IsNullOrWhiteSpace(req.Token))
            {
                return BadRequest(new { error = "Provider and token are required." });
            }

            // ⚡ In real-world: validate token with Google/Facebook API
            var user = new User
            {
                Id = 2,
                Username = "google_user",
                Roles = new List<string> { "User" }
            };

            var token = _jwt.GenerateToken(user);

            return Ok(new
            {
                token,
                expires_in = 3600,
                user = new { user.Id, user.Username, user.Roles }
            });
        }
    }

    // ✅ DTOs
    public class LoginRequest
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }

    public class OAuthRequest
    {
        public string Provider { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
    }
}
