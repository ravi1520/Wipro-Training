using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class SecureController : ControllerBase
{
    [HttpGet("user")]
    [Authorize(Roles = "User")]
    public IActionResult GetUserData() => Ok("Hello User, you accessed a protected endpoint!");

    [HttpGet("admin")]
    [Authorize(Roles = "Admin")]
    public IActionResult GetAdminData() => Ok("Hello Admin, this is sensitive admin-only data!");
}
