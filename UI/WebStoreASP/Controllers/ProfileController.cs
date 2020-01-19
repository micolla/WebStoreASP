using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebStore.Domain.Entity.Identity;
using WebStore.Interfaces.DataProviders;
using WebStore.Domain.ViewModels;

namespace WebStore.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly UserManager<User> userManager;

        public ProfileController(UserManager<User> userManager)
        {
            this.userManager = userManager;
        }
        public IActionResult Index(ProfileViewModel profileViewModel)
        {
            if (profileViewModel.Name is null)
            {
                var user = userManager.FindByNameAsync(User.Identity.Name).Result;
                profileViewModel = new ProfileViewModel
                {
                    Name = user.UserName,
                    Password = user.PasswordHash
                };
            }
            return View(profileViewModel ?? new ProfileViewModel());
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveChangesAsync(ProfileViewModel profileViewModel)
        {
            if (!ModelState.IsValid) return RedirectToAction(nameof(Index), profileViewModel);
            var user = userManager.FindByNameAsync(User.Identity.Name).Result;
            if (user != null)
            {
                var _passwordValidator =
                    HttpContext.RequestServices.GetService(typeof(IPasswordValidator<User>)) as IPasswordValidator<User>;
                var _passwordHasher =
                    HttpContext.RequestServices.GetService(typeof(IPasswordHasher<User>)) as IPasswordHasher<User>;

                IdentityResult result =
                    await _passwordValidator.ValidateAsync(userManager, user, profileViewModel.Password);
                if (result.Succeeded)
                {
                    user.PasswordHash = _passwordHasher.HashPassword(user, profileViewModel.Password);
                    await userManager.UpdateAsync(user);
                    return RedirectToAction("Index");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Пользователь не найден");
            }
            return RedirectToAction(nameof(Index), profileViewModel);
        }

        public IActionResult UserOrders([FromServices] IOrderDataProvider orderDataProvider) => View(orderDataProvider
                .GetUserOrders(User.Identity.Name)
                .Select(order => new UserOrderViewModel
                {
                    Id = order.Id,
                    Name = order.Date.ToString(),
                    Address = order.Address,
                    Phone = order.Phone,
                    TotalSum = order.OrderItems.Sum(o => o.Quantity * o.Price)
                }
                ));
    }
}