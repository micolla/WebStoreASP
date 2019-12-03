using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebStore.Model.Entity.Identity;
using WebStore.ViewModels.Identity;

namespace WebStore.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AccountController(UserManager<User> userManager,SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Registration() => View(new RegistrationViewModel());

        [HttpPost,ValidateAntiForgeryToken]
        public async Task<IActionResult> Registration(RegistrationViewModel newUser)
        {
            if (!ModelState.IsValid) return View(newUser);

            User user = new User { UserName = newUser.Login };

            var createResult = await _userManager.CreateAsync(user, newUser.Password);
            if (createResult.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, Role.Roles.User.ToString());
                await _signInManager.SignInAsync(user, false);
                return RedirectToAction("Index", "Home");
            }
            foreach (var error in createResult.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
            return View(newUser);
        }

        public IActionResult Login() => View(new LoginViewModel());

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel loginUser)
        {
            if (!ModelState.IsValid) return View(loginUser);

            var loginResult = await _signInManager.PasswordSignInAsync(loginUser.Login, loginUser.Password, loginUser.RememberMe, false);

            if(loginResult.Succeeded)
            {
                if (Url.IsLocalUrl(loginUser.ReturnUrl))
                    return Redirect(loginUser.ReturnUrl);
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "Неверное имя пользователя, или пароль");

            return View(loginUser);
        
        }
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

    }
}