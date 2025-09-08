using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using JWTAuthentication.Models;

namespace JWTAuthentication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _config;

        public LoginController(IConfiguration config)
        {
            _config = config;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Login([FromBody] UserModel login)
        {
            var user = Authenticate(login);

            if (user != null)
            {
                var tokenString = GenerateJWTToken(user);
                return Ok(new { Token = tokenString });
            }

            return Unauthorized("Invalid credentials");
        }

        private string GenerateJWTToken(UserModel userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, userInfo.UserName),
                new Claim(ClaimTypes.Email, userInfo.Email ?? ""),
                new Claim(ClaimTypes.Role, "Admin") // demo role
            };

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private UserModel? Authenticate(UserModel login)
        {
            // Demo authentication - replace with real DB check
            if (login.UserName == "Ravi" && login.Password == "password123")
            {
                return new UserModel
                {
                    UserName = "Ravi",
                    Email = "test@gmail.com",
                    Password = login.Password
                };
            }

            return null;
        }
    }
}
