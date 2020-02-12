using Microsoft.VisualStudio.TestTools.UnitTesting;
using IMCTaxJar.Models;
using System;
using System.Collections.Generic;
using System.Text;
using IMCTaxJar.Services;
using System.Threading.Tasks;

namespace IMCTaxJar.Models.Tests
{
    [TestClass()]
    public class OrderTests
    {

        [TestMethod()]
        public void ConstructorTest()
        {
            var order = new Order(new TaxJarService());
            Assert.IsTrue(order.TaxService != null);
        }

        [TestMethod()]
        public void AddProductTest()
        {
            var order = new Order(new TaxJarService());
            order.AddProduct(new Product());
            Assert.IsTrue(order.Products.Count == 1);
        }

        [TestMethod()]
        public async Task GetTaxPercentageTest()
        {
            var order = new Order(new TaxJarService());
            order.ToZip = "33458";
            var result = await order.GetTaxPercentage();
            Assert.IsTrue(result != default(decimal));
        }

        [TestMethod()]
        public async Task GetTaxPercentageWithoutServiceTest()
        {
            var order = new Order(new TaxJarService());
            order.TaxService = null;
            order.ToZip = "33458";
            var result = await order.GetTaxPercentage();
            Assert.IsTrue(result == default(decimal));
        }

        [TestMethod()]
        public async Task GetTaxAmountWithZeroTest()
        {
            var order = new Order(new TaxJarService());
            order.ToZip = "33458";
            order.ToState = "FL";
            order.FromZip = "33458";
            order.FromState = "FL";
            var result = await order.GetTaxAmount();
            Assert.IsTrue(result == default(decimal));
        }

        [TestMethod()]
        public async Task GetTaxAmountWithPriceTest()
        {
            var order = new Order(new TaxJarService());
            order.ToZip = "33458";
            order.ToState = "FL";
            order.FromZip = "33458";
            order.FromState = "FL";
            order.AddProduct(new Product() { Id = 1, Name = "Test",  Price = 10m });
            var result = await order.GetTaxAmount();
            Assert.IsTrue(result != default(decimal));
        }

        [TestMethod()]
        public async Task GetTaxAmountActualTest()
        {
            var order = new Order(new TaxJarService());
            order.ToZip = "33458";
            order.ToState = "FL";
            order.FromZip = "33458";
            order.FromState = "FL";
            order.AddProduct(new Product() { Price = 10m });
            var result = await order.GetTaxAmount();
            Assert.IsTrue(result == 0.65m);
        }

        [TestMethod()]
        public async Task GetTotalAmountActualTest()
        {
            var order = new Order(new TaxJarService());
            order.ToZip = "33458";
            order.ToState = "FL";
            order.FromZip = "33458";
            order.FromState = "FL";
            order.AddProduct(new Product() { Price = 10m });
            var result = await order.GetTotal();
            Assert.IsTrue(result == 10.65m);
        }


        [TestMethod()]
        public void GetSubTotalTest()
        {
            var order = new Order(new TaxJarService());
            order.AddProduct(new Product() { Id = 1, Price = 10m });

            var subtotal = order.GetSubTotal();
            Assert.IsTrue(subtotal == 10m);
        }
    }
}
