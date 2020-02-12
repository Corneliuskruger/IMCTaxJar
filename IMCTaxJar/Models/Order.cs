using IMCTaxJar.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IMCTaxJar.Models
{
    public class Order
    {
        public IList<Product> Products { get; set; } = new List<Product>();

        public string Country { get; set; } = "US";
        public string FromZip { get; set; }
        public string FromState { get; set; }
        private decimal? TaxAmount { get; set; }
        private decimal? TaxPercentage { get; set; }
        public string ToState { get; set; }
        public string ToZip { get; set; }


        public void AddProduct(Product product)
        {
            if (product != null)
            {
                Products.Add(product);
                TaxAmount = null;
            }
        }

        public  decimal GetSubTotal()
        {
            var result = Products.Sum(p => p.Price);
            return result;
        }
    }
}
