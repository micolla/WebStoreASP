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
        #region HelpMethods

        private Mock<ICartStore> GenerateCartStore()
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
            return _CartStore;
        }
        #endregion

        [TestMethod]
        public void AddToCart_Creates_New_Record()
        {
            Mock<ICartStore> _CartStore = GenerateCartStore();

            int expectedItems = 3;
            int expectedQuanitity = 1;
            var provider = new CookieCartProvider(_CartStore.Object);
            provider.AddToCart(3);
            Assert.AreEqual<int>(expectedItems, provider.GetCartItems().Count());
            Assert.AreEqual<int>(expectedQuanitity, provider.GetCartItems().First(i=>i.ProductId==3).Quantity);
        }

        [TestMethod]
        public void AddToCart_Add_To_Existing()
        {
            Mock<ICartStore> _CartStore = GenerateCartStore();

            int expectedItems = 2;
            int expectedQuanitity = 4;
            var provider = new CookieCartProvider(_CartStore.Object);
            provider.AddToCart(2);
            Assert.AreEqual<int>(expectedItems, provider.GetCartItems().Count());
            Assert.AreEqual<int>(expectedQuanitity, provider.GetCartItems().First(i => i.ProductId == 2).Quantity);
        }

        [TestMethod]
        public void ClearCart_Valid_Result()
        {
            Mock<ICartStore> _CartStore = GenerateCartStore();

            int expectedItems = 0;
            var provider = new CookieCartProvider(_CartStore.Object);
            provider.ClearCart();
            Assert.AreEqual<int>(expectedItems, provider.GetCartItems().Count());
        }

        [TestMethod]
        public void DecreaseFromCart_Decrease_Value()
        {
            Mock<ICartStore> _CartStore = GenerateCartStore();

            int expectedQnty = 2;
            var provider = new CookieCartProvider(_CartStore.Object);
            provider.DecreaseFromCart(2);
            Assert.AreEqual<int>(expectedQnty, provider.GetCartItems().FirstOrDefault(i=>i.ProductId==2).Quantity);
        }

        [TestMethod]
        public void DecreaseFromCart_Delete_Product()
        {
            Mock<ICartStore> _CartStore = GenerateCartStore();

            int expectedItems = 1;
            var provider = new CookieCartProvider(_CartStore.Object);
            provider.DecreaseFromCart(1);
            Assert.AreEqual<int>(expectedItems, provider.GetCartItems().Count());
            Assert.IsNull(provider.GetCartItems().FirstOrDefault(i => i.ProductId == 1));
        }

        [TestMethod]
        public void DeleteFromCart_Delete_Product()
        {
            Mock<ICartStore> _CartStore = GenerateCartStore();

            int expectedItems = 1;
            var provider = new CookieCartProvider(_CartStore.Object);
            provider.DeleteFromCart(2);
            Assert.AreEqual<int>(expectedItems, provider.GetCartItems().Count());
            Assert.IsNull(provider.GetCartItems().FirstOrDefault(i => i.ProductId == 2));
        }

    }
}
