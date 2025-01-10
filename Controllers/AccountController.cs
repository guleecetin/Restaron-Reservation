using RestoranRezervasyonu.Models;

namespace RestoranRezervasyonu.Controllers
{
    using global::RestoranRezervasyonu.Utility;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    namespace RestoranRezervasyonu.Controllers
    {
        public class AccountController : Controller
        {
            private readonly UygulamaDbContext _context;
            private readonly IPasswordHasher<ApplicationUser> _passwordHasher;
            private readonly SignInManager<ApplicationUser> _signInManager;
            private readonly UserManager<ApplicationUser> _userManager;

            public AccountController(
                UygulamaDbContext context,
                IPasswordHasher<ApplicationUser> passwordHasher,
                SignInManager<ApplicationUser> signInManager,
                UserManager<ApplicationUser> userManager)
            {
                _context = context;
                _passwordHasher = passwordHasher;
                _signInManager = signInManager;
                _userManager = userManager;
            }

            // Mevcut Login işlemleri
            [HttpGet]
            public IActionResult Login(string returnUrl = null)
            {
                ViewData["ReturnUrl"] = returnUrl;
                return View();
            }

            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
            {
                if (ModelState.IsValid)
                {
                    // Email ile giriş yapılıyor
                    var user = await _userManager.FindByEmailAsync(model.Email);
                    if (user != null)
                    {
                        var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, lockoutOnFailure: false);

                        if (result.Succeeded)
                        {
                            TempData["SuccessMessage"] = $"Welcome {user.FullName}!";
                            return RedirectToLocal(returnUrl);
                        }
                    }

                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    TempData["ErrorMessage"] = "Login failed. Please check your information.";
                }

                return View(model);
            }

            // Yeni Register işlemleri
            [HttpGet]
            public IActionResult Register(string returnUrl = null)
            {
                ViewData["ReturnUrl"] = returnUrl;
                return View();
            }

            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Register(ApplicationUser model, string returnUrl = null)
            {
                ViewData["ReturnUrl"] = returnUrl;
                if (ModelState.IsValid)
                {
                    var user = new ApplicationUser
                    {
                        FullName = model.FullName,
                        Email = model.Email,
                        PhoneNumber = model.PhoneNumber,
                        UserName = model.Email
                    };

                    var result = await _userManager.CreateAsync(user, model.Password);

                    if (result.Succeeded)
                    {
                        // Kullanıcıya "Kullanici" rolünü ata
                        await _userManager.AddToRoleAsync(user, UserRoles.Role_Kullanici);

                        // Kullanıcıyı otomatik olarak giriş yap
                        await _signInManager.SignInAsync(user, isPersistent: false);

                        TempData["SuccessMessage"] = "Registration completed successfully!";
                        return RedirectToLocal(returnUrl);
                    }

                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }

                return View(model);
            }

            private IActionResult RedirectToLocal(string returnUrl)
            {
                if (Url.IsLocalUrl(returnUrl))
                {
                    return Redirect(returnUrl);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }

            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Logout()
            {
                await _signInManager.SignOutAsync();
                TempData["SuccessMessage"] = "Successfully exited.";
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
        }
    }
}
