using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebStore.Domain.Entity.Identity;
using WebStore.Domain.ViewModels;
using WebStore.Interfaces.DataProviders;
using WebStore.Interfaces.Api;

namespace WebStore.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartDataProvider _cartDataProvider;
        private readonly IProductService _productData;

        public CartController(ICartDataProvider cartDataProvider, IProductService productDataProvider, UserManager<User> userManager)
        {
            _cartDataProvider = cartDataProvider;
            _productData = productDataProvider;
        }
        public IActionResult Details() => View(new DetailsCartViewModel { CartViewModel = GetCartViewModel(), OrderViewModel = new OrderViewModel() });

        private CartViewModel GetCartViewModel()
        {
            var cartItems = _cartDataProvider.GetCartItems();
            if (cartItems == null) return new CartViewModel();
            var products = _productData.GetProducts(new Domain.Entity.ProductFilter
            { Ids = cartItems.Select(i => i.ProductId) }).ToList();
            return new CartViewModel
            {
                Items = products.Select(p => new CartItemViewModel(p.Id, p.Name, p.Price, cartItems.Where(i => i.ProductId == p.Id).Select(i => i.Quantity).FirstOrDefault(), p.ImageUrl)).ToList()
            };
        }

        public IActionResult AddToCart(int productId)
        {
            _cartDataProvider.AddToCart(productId);
            return RedirectToAction("Details");
        }

        public IActionResult DecrimentFromCart(int productId)
        {
            _cartDataProvider.DecreaseFromCart(productId);
            return RedirectToAction("Details");
        }

        public IActionResult RemoveFromCart(int productId)
        {
            _cartDataProvider.DeleteFromCart(productId);
            return RedirectToAction("Details");
        }

        public IActionResult RemoveAll()
        {
            _cartDataProvider.ClearCart();
            return RedirectToAction("Details");
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> CheckOutAsync(OrderViewModel Model, [FromServices] IOrderService OrderService)
        {
            if (!ModelState.IsValid)
                return View(nameof(Details), new DetailsCartViewModel
                {
                    CartViewModel = GetCartViewModel(),
                    OrderViewModel = Model
                });
            var order = await OrderService.CreateOrderAsync(
                new Domain.DTO.Orders.OrderDTO
                {
                    Name = Model.Name,
                    Address = Model.Address,
                    Phone = Model.Phone,
                    Date = DateTime.Now,
                    OrderItems = _cartDataProvider.GetCartItems().Select(i => 
                        new Domain.DTO.Orders.OrderItemDTO
                        {
                            ProductId = i.ProductId,
                            Quantity = i.Quantity
                        })
                },
                User.Identity.Name);

            _cartDataProvider.ClearCart();

            return RedirectToAction("OrderConfirmed", new { id = order.Id });
        }

        public IActionResult OrderConfirmed(int id)
        {
            ViewBag.OrderId = id;
            return View();
        }

        #region API

        public IActionResult AddToCartAPI(int id)
        {
            _cartDataProvider.AddToCart(id);
            return Json(new { id, message = $"Товар id:{id} успешно добавлен в корзину" });
        }

        public IActionResult DecrementFromCartAPI(int id)
        {
            _cartDataProvider.DecreaseFromCart(id);
            return Json(new { id, message = $"Количество товара id:{id} в корзине уменьшено на 1" });
        }

        public IActionResult RemoveFromCartAPI(int id)
        {
            _cartDataProvider.DeleteFromCart(id);
            return Json(new { id, message = $"Товар id:{id} удалён из корзины" });
        }

        public IActionResult RemoveAllAPI()
        {
            _cartDataProvider.ClearCart();
            return Json(new { message = "Корзина очищена" });
        }

        #endregion

    }
}