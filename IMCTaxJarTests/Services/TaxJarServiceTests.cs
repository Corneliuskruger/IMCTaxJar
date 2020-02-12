using Microsoft.VisualStudio.TestTools.UnitTesting;
using IMCTaxJar.Services;
using System;
using System.Collections.Generic;
using System.Text;
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
            var taxJarService = new TaxJarService();
            Assert.IsTrue(taxJarService.client != null, "TaxJar Client is not created");
            Assert.IsTrue(!string.IsNullOrEmpty(taxJarService.client.apiToken), "TaxJar ApiKey is invalid");
        }

        [TestMethod()]
        public async Task CalculateTaxesForOrderTest()
        {
            var taxJarService = new TaxJarService();

            var order = new Order(taxJarService)
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
            var taxJarService = new TaxJarService();

            var order = new Order(taxJarService)
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
            var taxJarService = new TaxJarService();

            var order = new Order(taxJarService)
            {
                ToZip = "33458"
            };

            var result = await taxJarService.GetRateForLocation(order);

            Assert.IsTrue(result != default, "Tax Rate should not be zero");
            Assert.IsTrue(result == 0.065m, "Jupiter Tax Rate should be 0.065");
        }

    }
}