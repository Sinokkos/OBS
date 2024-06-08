using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.General;
using Online_Book_Store.Data;
using Online_Book_Store.Models;

namespace Online_Book_Store.Controllers
{
    public class AccountController : Controller
    {
        private readonly AppDbContext _context;
        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> userManager;

        private readonly SignInManager<ApplicationUser> signInManager;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, AppDbContext context) 
        { 
           _context = context;
           _userManager = userManager;
           _signInManager = signInManager;
        
        }
    }
}
