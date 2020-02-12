using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using IMCTaxJar.Models;

namespace IMCTaxJar.Services.Tests
{
    [TestClass()]
    public class TaxServiceTests
    {
        [TestMethod()]
        public async Task GetTaxPercentageTest()
        {
            var service = new TaxService(new TaxJarCalculator());
            var toZip = "33458";
            var result = await service.GetTaxPercentage(toZip);
            Assert.IsTrue(result != default(decimal));
        }

        [TestMethod()]
        public async Task GetTaxAmountWithZeroTest()
        {
            var service = new TaxService(new TaxJarCalculator());
            var order = new Models.Order();
            order.ToZip = "33458";
            order.ToState = "FL";
            order.FromZip = "33458";
            order.FromState = "FL";
            var result = await service.GetTaxAmount(order);
            Assert.IsTrue(result == default(decimal));
        }

        [TestMethod()]
        public async Task GetTaxAmountWithPriceTest()
        {
            var service = new TaxService(new TaxJarCalculator());
            var order = new Models.Order();
            order.ToZip = "33458";
            order.ToState = "FL";
            order.FromZip = "33458";
            order.FromState = "FL";
            order.AddProduct(new Product() { Id = 1, Name = "Test", Price = 10m });

            var result = await service.GetTaxAmount(order);
            Assert.IsTrue(result != default(decimal));
        }

        [TestMethod()]
        public async Task GetTaxAmountActualTest()
        {
            var service = new TaxService(new TaxJarCalculator());
            var order = new Models.Order();
            order.ToZip = "33458";
            order.ToState = "FL";
            order.FromZip = "33458";
            order.FromState = "FL";
            order.AddProduct(new Product() { Price = 10m });

            var result = await service.GetTaxAmount(order);
            Assert.IsTrue(result == 0.65m);
        }

        [TestMethod()]
        public async Task GetTotalAmountActualTest()
        {
            var service = new TaxService(new TaxJarCalculator());
            var order = new Models.Order();
            order.ToZip = "33458";
            order.ToState = "FL";
            order.FromZip = "33458";
            order.FromState = "FL";
            order.AddProduct(new Product() { Price = 10m });

            var result = await service.GetOrderTotal(order);
            Assert.IsTrue(result == 10.65m);
        }


        [TestMethod()]
        public void GetSubTotalTest()
        {
            var service = new TaxService(new TaxJarCalculator());
            var order = new Models.Order();
            order.AddProduct(new Product() { Id = 1, Price = 10m });

            var subtotal = order.GetSubTotal();
            Assert.IsTrue(subtotal == 10m);
        }
    }
}