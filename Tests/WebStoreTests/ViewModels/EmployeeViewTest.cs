using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using WebStore.ViewModels;

namespace WebStoreTests
{
    [TestClass]
    public class EmployeeViewTest
    {
        [TestMethod]
        public void CreateEmptyEmployee()
        {
            var employeeView = new EmployeeView();
            Assert.IsNotNull(employeeView);
        }
        [TestMethod]
        public void CreateFilledEmployee()
        {
            var employeeView = new EmployeeView
                                    {
                                        Id = 1,
                                        FirstName = "Николай",
                                        LastName = "Донцов",
                                        HiringDate = DateTime.Parse("2016-05-01"),
                                        BirthDay = DateTime.Parse("1990-12-22")
                                    };
            Assert.AreEqual<DateTime>(employeeView.HiringDate, DateTime.Parse("2016-05-01"));
            Assert.AreEqual<DateTime>(employeeView.BirthDay, DateTime.Parse("1990-12-22"));
            Assert.AreEqual<String>(employeeView.FirstName, "Николай");
            Assert.AreEqual<String>(employeeView.LastName, "Донцов");
            Assert.AreEqual<int>(employeeView.Id, 1);
        }
    }
}
