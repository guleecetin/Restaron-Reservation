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
            private readonly IPasswordHasher<User> _passwordHasher;
            private readonly SignInManager<User> _signInManager;
            private readonly UserManager<User> _userManager;

            public AccountController(
                UygulamaDbContext context,
                IPasswordHasher<User> passwordHasher,
                SignInManager<User> signInManager,
                UserManager<User> userManager)
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
                ViewData["ReturnUrl"] = returnUrl;
                if (ModelState.IsValid)
                {
                    var result = await _signInManager.PasswordSignInAsync(model.Email,
                        model.Password, model.RememberMe, lockoutOnFailure: false);

                    if (result.Succeeded)
                    {
                        return RedirectToLocal(returnUrl);
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Geçersiz giriş denemesi.");
                        return View(model);
                    }
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
            public async Task<IActionResult> Register(User model, string returnUrl = null)
            {
                ViewData["ReturnUrl"] = returnUrl;
                if (ModelState.IsValid)
                {
                    // E-posta kontrolü
                    if (await _context.Users.AnyAsync(u => u.Email == model.Email))
                    {
                        ModelState.AddModelError("Email", "Bu e-posta adresi zaten kayıtlı");
                        return View(model);
                    }

                    // Şifreyi hashle
                    model.Password = _passwordHasher.HashPassword(null, model.Password);

                    // Veritabanına kaydet
                    _context.Users.Add(model);
                    await _context.SaveChangesAsync();

                    // Başarılı kayıt mesajı
                    TempData["SuccessMessage"] = "Kayıt başarıyla tamamlandı!";

                    return RedirectToLocal(returnUrl);
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
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
        }
    }
}
