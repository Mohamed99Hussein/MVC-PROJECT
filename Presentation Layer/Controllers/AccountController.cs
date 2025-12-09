using Business_Logic_Layer.Services.AccountService;
using Business_Logic_Layer.ViewModels.AccountViewModels;
using Data_Access_Layer.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Presentation_Layer.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService accountService;
        private readonly SignInManager<ApplicationUser> signInManager;

        public AccountController(IAccountService accountService,SignInManager<ApplicationUser> signInManager)
        {
            this.accountService = accountService;
            this.signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public async Task<ActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("InvalidLogin", "Your Email or Password Isn't Valid.");
                return View(model);
            }

            // ✅ بدل استخدام accountService.ValidateUser، نستخدم UserManager مباشرة
            var user = await signInManager.UserManager.FindByEmailAsync(model.Email);

            if (user == null)
            {
                ModelState.AddModelError("InvalidLogin", "User not found.");
                return View(model);
            }

            // ✅ نسجل الدخول بالبريد وكلمة السر
            var result = await signInManager.PasswordSignInAsync(user.UserName, model.Password, model.RememberMe, false);

            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }

            if (result.IsLockedOut)
            {
                ModelState.AddModelError("InvalidLogin", "Your account is locked. Try again later.");
                return View(model);
            }

            if (result.IsNotAllowed)
            {
                ModelState.AddModelError("InvalidLogin", "Your account is not allowed to sign in.");
                return View(model);
            }

            ModelState.AddModelError("InvalidLogin", "Invalid email or password.");
            return View(model);
        }
    }
}
