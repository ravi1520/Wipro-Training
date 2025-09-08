using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace SecureAuthAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SecureController : ControllerBase
    {
        // 🔹 Accessible to any authenticated user
        [Authorize]
        [HttpGet("data")]
        public IActionResult GetSecureData()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "unknown";
            var username = User.FindFirst(ClaimTypes.Name)?.Value ?? "anonymous";

            return Ok(new
            {
                message = "Secure data accessed successfully.",
                data = new
                {
                    user_id = userId,
                    username,
                    secure_info = "This is some sensitive user data."
                }
            });
        }

        // 🔹 Only accessible to Admin role
        [Authorize(Roles = "Admin")]
        [HttpGet("admin/dashboard")]
        public IActionResult GetAdminDashboard()
        {
            var username = User.FindFirst(ClaimTypes.Name)?.Value ?? "unknown";
            return Ok(new
            {
                message = $"Welcome to Admin Dashboard, {username}!"
            });
        }
    }
}
