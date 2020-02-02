using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Controllers;
using WebStore.Interfaces.Api;
//using Assert = Xunit.Assert;

namespace WebStore.Tests.Controllers
{
    [TestClass]
    public class WebApiControllerTests
    {
        [TestMethod]
        public async Task Index_Return_Values()
        {
            var expected_values = new[] { "1", "2", "3" };
            var ValueService = new Mock<IValuesService>();
            ValueService
                .Setup(service => service.GetAsync())
                .ReturnsAsync(expected_values);

            var controller = new WebApiController();
            var result = await controller.Index(ValueService.Object);

            Assert.IsInstanceOfType(result, typeof(ViewResult));
            var model = (result as ViewResult).Model;
            Assert.IsInstanceOfType(model, typeof(IEnumerable<string>));
            Assert.AreEqual<int>(expected_values.Length, (model as IEnumerable<string>).Count());
            ValueService.Verify(service => service.GetAsync());
            ValueService.VerifyNoOtherCalls();
        }
    }
}