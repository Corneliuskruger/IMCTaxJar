using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IMCTaxJar.Models.Tests
{
    [TestClass()]
    public class OrderTests
    {
        [TestMethod()]
        public void AddProductTest()
        {
            var order = new Order();
            order.AddProduct(new Product());
            Assert.IsTrue(order.Products.Count == 1);
        }
    }
}
