using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SecureShop.Models;
using SecureShop.ViewModels;

namespace SecureShop.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(UserManager<ApplicationUser> um, SignInManager<ApplicationUser> sm)
        {
            _userManager = um;
            _signInManager = sm;
        }

        [HttpGet]
        public IActionResult Register() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel vm)
        {
            if (!ModelState.IsValid) return View(vm);

            var user = new ApplicationUser { UserName = vm.Username, Email = vm.Email, EmailConfirmed = true };
            var result = await _userManager.CreateAsync(user, vm.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "Customer");
                await _signInManager.SignInAsync(user, isPersistent: false);
                return RedirectToAction("Index", "Products");
            }

            foreach (var e in result.Errors) ModelState.AddModelError("", e.Description);
            return View(vm);
        }

        [HttpGet]
        public IActionResult Login(string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel vm, string? returnUrl = null)
        {
            if (!ModelState.IsValid) return View(vm);

            ApplicationUser? user = vm.UsernameOrEmail.Contains('@')
                ? await _userManager.FindByEmailAsync(vm.UsernameOrEmail)
                : await _userManager.FindByNameAsync(vm.UsernameOrEmail);

            if (user == null)
            {
                ModelState.AddModelError("", "Invalid credentials.");
                return View(vm);
            }

            var result = await _signInManager.PasswordSignInAsync(user, vm.Password, vm.RememberMe, lockoutOnFailure: true);
            if (result.Succeeded)
                return Redirect(returnUrl ?? Url.Action("Index", "Products")!);

            if (result.IsLockedOut)
                ModelState.AddModelError("", "Account locked due to multiple failed attempts. Try again later.");
            else
                ModelState.AddModelError("", "Invalid credentials.");

            return View(vm);
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync(); // invalidates auth cookie
            return RedirectToAction("Login");
        }

        public IActionResult AccessDenied() => View();
    }
}
