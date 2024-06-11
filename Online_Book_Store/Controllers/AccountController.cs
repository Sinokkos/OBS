using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.General;
using Online_Book_Store.Data;
using Online_Book_Store.Data.Static;
using Online_Book_Store.Models;
using Online_Book_Store.ViewModels;

namespace Online_Book_Store.Controllers
{
    public class AccountController : Controller
    {
        private readonly AppDbContext _context;
        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _signInManager;
        
        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, AppDbContext context) 
        { 
           _context = context;
           _userManager = userManager;
           _signInManager = signInManager;
        
        }

        [AllowAnonymous] // Böylelikle sisteme giriş yapmamış kullanıcılar direkt Login sayfasına yönderilir.
        public async Task<IActionResult> Login()
        {
            var response = new LoginVM();

            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM loginVM)
        {
            if (!ModelState.IsValid)
            {
                return View(loginVM);
            }

            var user = await _userManager.FindByEmailAsync(loginVM.EmailAddress);

            if (user != null)
            {
                // şifreyi kontrol etmem lazım
                var passwordCheck = await _userManager.CheckPasswordAsync(user, loginVM.Password);

                if (passwordCheck)
                {
                    // eğer password doğruysa
                    var result = await _signInManager.PasswordSignInAsync(user, loginVM.Password, false, false);

                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Books");

                    }
                }

                TempData["Error"] = "Yanlış kullanıcı bilgileri...Lütfen kontrol ediniz...";

                return View(loginVM);
            }


            TempData["Error"] = "Yanlış kullanıcı bilgileri...Lütfen kontrol ediniz...";

            return View(loginVM);

        }

        public async Task<IActionResult> Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM registerVM)
        {
            if (!ModelState.IsValid)
            {
                return View(registerVM);
            }

            var user = await _userManager.FindByEmailAsync(registerVM.EmailAddress);

            if (user != null)
            {
                // demek ki benim kayıt etmek istediğim email bilgisi daha önceden kayıtlıymış.

                TempData["Error"] = "Bu mail adresi kullanımda. Lütfen başka bir mail adresi giriniz....";

                return View(registerVM);
            }

            var newUser = new ApplicationUser()
            {
                FullName = registerVM.FullName,
                Email = registerVM.EmailAddress,
                UserName = registerVM.EmailAddress,
                EmailConfirmed = true
            };

            var newUserResponse = await _userManager.CreateAsync(newUser, registerVM.Password);

            if (newUserResponse.Succeeded)
            {
                await _userManager.AddToRoleAsync(newUser, UserRoles.User);

            }

            return View("RegisterCompleted");


        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Index", "Books");
        }

        public IActionResult AccessDenied(string returnURL)
        {
            return View();
        }
    }
}
