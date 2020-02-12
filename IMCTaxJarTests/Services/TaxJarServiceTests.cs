using Microsoft.VisualStudio.TestTools.UnitTesting;
using IMCTaxJar.Models;
using System.Threading.Tasks;

namespace IMCTaxJar.Services.Tests
{
    [TestClass()]
    public class TaxJarServiceTests
    {
        [TestMethod()]
        public void TaxJarServiceTest()
        {
            var taxJarService = new TaxJarCalculator();
            Assert.IsTrue(taxJarService.client != null, "TaxJar Client is not created");
            Assert.IsTrue(!string.IsNullOrEmpty(taxJarService.client.apiToken), "TaxJar ApiKey is invalid");
        }

        [TestMethod()]
        public async Task CalculateTaxesForOrderTest()
        {
            var taxJarService = new TaxJarCalculator();

            var order = new Models.Order()
            {
                FromState = "FL",
                Country = "US",
                ToState = "FL",
                ToZip = "33458"
            };

            order.AddProduct(new Product() { Price = 10m });

            var result = await taxJarService.CalculateTaxesForOrder(order);

            Assert.IsTrue(result != default, "Tax should not be zero");
            Assert.IsTrue(result == 0.65m, "Tax on $10 should be 65c");
        }

        [TestMethod()]
        public async Task CalculateTaxesForZeroValueOrderTest()
        {
            var taxJarService = new TaxJarCalculator();

            var order = new Models.Order()
            {
                FromState = "FL",
                Country = "US",
                ToState = "FL",
                ToZip = "33458"
            };

            var result = await taxJarService.CalculateTaxesForOrder(order);

            Assert.IsTrue(result == default, "Tax should be zero");
        }


        [TestMethod()]
        public async Task GetTaxRateForLocationTest()
        {
            var taxJarService = new TaxJarCalculator();
            var toZip = "33458";

            var result = await taxJarService.GetRateForLocation(toZip);

            Assert.IsTrue(result != default, "Tax Rate should not be zero");
            Assert.IsTrue(result == 0.065m, "Jupiter Tax Rate should be 0.065");
        }

    }
}