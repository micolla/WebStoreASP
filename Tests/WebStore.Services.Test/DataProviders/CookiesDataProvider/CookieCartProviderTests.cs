using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;
using WebStore.Domain.Entity;
using WebStore.Interfaces.DataProviders;
using WebStore.Services.DataProviders.CookiesDataProvider;

namespace WebStore.Services.Test.DataProviders.CookiesDataProvider
{
    [TestClass]
    public class CookieCartProviderTests
    {
        [TestMethod]
        public void AddToCart_Creates_New_Record()
        {
            var _FullCart = new Cart
            {
                Items = new List<CartItem>
                {
                    new CartItem { ProductId = 1, Quantity = 1 },
                    new CartItem { ProductId = 2, Quantity = 3 }
                }
            };
            var _CartStore = new Mock<ICartStore>();
            _CartStore
                .Setup(s => s.Cart)
                .Returns(_FullCart);

            int expectedItems = 3;
            int expectedQuanitity = 1;
            var provider = new CookieCartProvider(_CartStore.Object);
            provider.AddToCart(3);
            Assert.AreEqual<int>(expectedItems, provider.GetCartItems().Count());
            Assert.AreEqual<int>(expectedQuanitity, provider.GetCartItems().First(i=>i.ProductId==3).Quantity);
        }

        [TestMethod]
        public void AddToCart_Creates_Add_To_Existing()
        {
            var _FullCart = new Cart
            {
                Items = new List<CartItem>
                {
                    new CartItem { ProductId = 1, Quantity = 1 },
                    new CartItem { ProductId = 2, Quantity = 3 }
                }
            };
            var _CartStore = new Mock<ICartStore>();
            _CartStore
                .Setup(s => s.Cart)
                .Returns(_FullCart);

            int expectedItems = 2;
            int expectedQuanitity = 4;
            var provider = new CookieCartProvider(_CartStore.Object);
            provider.AddToCart(2);
            Assert.AreEqual<int>(expectedItems, provider.GetCartItems().Count());
            Assert.AreEqual<int>(expectedQuanitity, provider.GetCartItems().First(i => i.ProductId == 2).Quantity);
        }

    }
}
