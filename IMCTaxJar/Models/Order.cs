using IMCTaxJar.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMCTaxJar.Models
{
    public class Order
    {
        public ITaxService TaxService;

        public IList<Product> Products { get; set; } = new List<Product>();

        public string Country { get; set; } = "US";
        public string FromZip { get; set; }
        public string FromState { get; set; }
        private decimal? TaxAmount { get; set; }
        private decimal? TaxPercentage { get; set; }
        public string ToState { get; set; }
        public string ToZip { get; set; }


        public Order(ITaxService taxService)
        {
            TaxService = taxService;
        }

        public void AddProduct(Product product)
        {
            if (product != null)
            {
                Products.Add(product);
                TaxAmount = null;
            }
        }


        public async Task<decimal> GetTaxPercentage()
        {
            if (TaxPercentage == null)
            {
                if (TaxService != null)
                {
                    var result = await TaxService.GetRateForLocation(this);
                    TaxPercentage = result;
                }
            }

            if (TaxPercentage.HasValue)
                return TaxPercentage.Value;
            else
                return 0;
        }

        public async Task<decimal> GetTaxAmount()
        {
            if (TaxAmount == null)
            {
                if (TaxService != null)
                {
                    var result = await TaxService.CalculateTaxesForOrder(this);
                    TaxAmount = result;
                }
            }
            
            return TaxAmount.Value;

        }

        public async Task<decimal> GetTotal()
        {
            var result = GetSubTotal();
            result += await GetTaxAmount();
            return result;
        }

        public  decimal GetSubTotal()
        {
            var result = Products.Sum(p => p.Price);
            return result;
        }
        
    }
}
