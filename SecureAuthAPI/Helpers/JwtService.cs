using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using SecureAuthAPI.Models;

namespace SecureAuthAPI.Helpers
{
    public class JwtService
    {
        private readonly IConfiguration _config;

        public JwtService(IConfiguration config)
        {
            _config = config;
        }

        public string GenerateToken(User user)
        {
            if (user == null) 
                throw new ArgumentNullException(nameof(user));

         
            var keyString = _config["Jwt:Key"] 
                            ?? throw new InvalidOperationException("JWT Key is missing in configuration.");

            var issuer = _config["Jwt:Issuer"] 
                         ?? throw new InvalidOperationException("JWT Issuer is missing in configuration.");

            var audience = _config["Jwt:Audience"] 
                           ?? throw new InvalidOperationException("JWT Audience is missing in configuration.");

            var expireMinutes = _config["Jwt:ExpireMinutes"] ?? "60"; 

            
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username)
            };

            foreach (var role in user.Roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(keyString));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(expireMinutes)),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
