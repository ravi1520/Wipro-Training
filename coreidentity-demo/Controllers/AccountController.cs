// using coreidentity_demo.Data;
using coreidentity_demo.Models;
using coreidentity_demo.Models.ViewModels;
using coreidentity_demo.Services;
using Microsoft.AspNetCore.Http;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace coreidentity_demo.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly AppDbContext _context;

        public AccountController(
            AppDbContext context,
            SignInManager<AppUser> signInManager,
            UserManager<AppUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Roles");
            }
            return View();
        }

        [HttpPost, ActionName("Login")]
        public async Task<IActionResult> LoginPost(LoginViewModel loginModel)
        {
            if (ModelState.IsValid)
            {
                var profile = _context.Profiles.SingleOrDefault(p => p.UserName == loginModel.Email);
                var deactivated = _context.DeactivatedProfiles.FirstOrDefault(e => e.ProfileId == profile.ProfileId);

                if (deactivated != null)
                {
                    ModelState.AddModelError("", $"Your profile has been blocked: {deactivated.Reason}. Please contact administrator admin@artisan.com");
                    return View();
                }

                var result = await _signInManager.PasswordSignInAsync(loginModel.Email, loginModel.Password, loginModel.RememberMe, false);

                if (result.Succeeded)
                {
                    var user = await _userManager.FindByEmailAsync(loginModel.Email);
                    var roles = await _userManager.GetRolesAsync(user);
                    var primaryRole = roles.FirstOrDefault();

                    HttpContext.Session.SetString("ProfileId", profile.ProfileId.ToString());
                    HttpContext.Session.SetString("ProfileRole", primaryRole ?? "Member");
                    HttpContext.Session.SetString("ProfileImage", !string.IsNullOrWhiteSpace(profile.DisplayImageUrl) ? profile.DisplayImageUrl : "favicon.ico");

                    return primaryRole switch
                    {
                        "Vendor" => RedirectToAction("Index", "Roles"),
                        "Artist" => RedirectToAction("BuyerDashBoard", "Roles"),
                        "Buyer" => RedirectToAction("BuyerDashBoard", "Roles"),
                        "Admin" => RedirectToAction("Index", "Roles"),
                        _ => RedirectToAction("Index", "Home")
                    };
                }
            }
            ModelState.AddModelError("", "Failed to Login");
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Register() => View();

        [HttpPost, ActionName("Register")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterPost(RegisterViewModel registerModel)
        {
            if (ModelState.IsValid)
            {
                var user = new AppUser
                {
                    FirstName = registerModel.FirstName,
                    LastName = registerModel.LastName,
                    UserName = registerModel.Email,
                    PhoneNumber = registerModel.PhoneNumber,
                    Email = registerModel.Email
                };

                var profile = new Profile
                {
                    UserName = registerModel.Email,
                    FirstName = registerModel.FirstName,
                    LastName = registerModel.LastName,
                    FullName = $"{registerModel.FirstName} {registerModel.LastName}",
                    PhoneNumber = registerModel.PhoneNumber
                };

                var result = await _userManager.CreateAsync(user, registerModel.Password);
                if (result.Succeeded)
                {
                    if (!await _roleManager.RoleExistsAsync(registerModel.RoleName))
                    {
                        await _roleManager.CreateAsync(new IdentityRole(registerModel.RoleName));
                    }

                    if (!await _userManager.IsInRoleAsync(user, registerModel.RoleName))
                    {
                        await _userManager.AddToRoleAsync(user, registerModel.RoleName);
                    }

                    if (!string.IsNullOrWhiteSpace(user.Email))
                    {
                        var claims = new[]
                        {
                            new Claim(ClaimTypes.Email, user.Email),
                            new Claim(ClaimTypes.GivenName, user.FirstName),
                            new Claim(ClaimTypes.Surname, user.LastName)
                        };
                        await _userManager.AddClaimsAsync(user, claims);
                    }

                    _context.Profiles.Add(profile);
                    await _context.SaveChangesAsync();

                    var resultSignIn = await _signInManager.PasswordSignInAsync(registerModel.Email, registerModel.Password, registerModel.RememberMe, false);
                    if (resultSignIn.Succeeded)
                    {
                        HttpContext.Session.SetString("ProfileId", profile.ProfileId.ToString());
                        HttpContext.Session.SetString("ProfileImage", "favicon.ico");
                        return RedirectToAction("Index", "Roles");
                    }
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View();
        }

        public IActionResult ChangePassword() => View();

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var result = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);

            if (result.Succeeded)
            {
                ViewBag.Message = "Your password has been updated";
            }
            return View();
        }

        public DeactivatedProfile DeactivatedCheck(int id) =>
            _context.DeactivatedProfiles.FirstOrDefault(e => e.ProfileId == id);

        public IActionResult VerifyContact()
        {
            var profile = _context.Profiles.FirstOrDefault(u => u.UserName == User.Identity.Name);
            var model = new VerifyContactViewModel
            {
                Email = profile.UserName,
                PhoneNumber = profile.PhoneNumber,
                Status = profile.ContactVerified
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult VerifyContact(VerifyContactViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.VerificationCode == "5186")
                {
                    var profile = _context.Profiles.FirstOrDefault(u => u.UserName == User.Identity.Name);
                    profile.ContactVerified = "Verified";
                    _context.Profiles.Update(profile);
                    _context.SaveChanges();
                    ViewBag.Message = "Contact Verified";
                }
                else
                {
                    ModelState.AddModelError("", "You entered wrong code. Please enter code sent on your email");
                }
            }
            return View(model);
        }

        public async Task<string> ConfirmContact()
        {
            var email = User.Identity.Name;
            await EmailService.SendEmailAsync(new MailRequest { ToEmail = email, Subject = "Verification Code", Body = "Your Verification Code is:5186" });
            return "Verification Code Sent to your email";
        }

        public IActionResult ForgotPassword() => View();

        [HttpPost]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(model.Email);
                if (user == null)
                {
                    return View("ForgotPasswordConfirmation");
                }

                var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                var callbackUrl = Url.Action("ResetPassword", "Account", new { Email = user.Email, Code = code }, protocol: Request.Scheme);

                await EmailService.SendEmailAsync(new MailRequest
                {
                    ToEmail = user.Email,
                    Subject = "Reset Password",
                    Body = $"Please reset your password by clicking <a href=\"{callbackUrl}\">here</a>"
                });

                return RedirectToAction("ForgotPasswordConfirmation", "Account");
            }
            return View(model);
        }

        [AllowAnonymous]
        public IActionResult ForgotPasswordConfirmation() => View();

        [AllowAnonymous]
        public IActionResult ResetPassword(string email, string code) =>
            code == null ? View("Error") : View();

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = await _userManager.FindByNameAsync(model.Email);
            if (user == null)
                return RedirectToAction("ResetPasswordConfirmation", "Account");

            var result = await _userManager.ResetPasswordAsync(user, model.Code, model.NewPassword);
            if (result.Succeeded)
                return RedirectToAction("ResetPasswordConfirmation", "Account");

            AddErrors(result);
            return View();
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
                ModelState.AddModelError("", error.Description);
        }

        [AllowAnonymous]
        public IActionResult ResetPasswordConfirmation() => View();
    }
}
