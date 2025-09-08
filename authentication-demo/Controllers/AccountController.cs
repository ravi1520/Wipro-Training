using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using authentication_demo.ViewModels;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;

namespace authentication_demo.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;

        public AccountController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login() => View();

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var result = await _signInManager.PasswordSignInAsync(
                model.Email, model.Password, isPersistent: false, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                return RedirectToAction("LoginSuccess");
            }

            ModelState.AddModelError("", "Invalid login attempt");
            return View(model);
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("LogoutSuccess");
        }

        [AllowAnonymous]
        public IActionResult LoginSuccess() => View();

        [AllowAnonymous]
        public IActionResult LogoutSuccess() => View();
    }
}
