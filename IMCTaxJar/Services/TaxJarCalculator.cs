using IMCTaxJar.Interfaces;
using System.Threading.Tasks;
using Taxjar;

namespace IMCTaxJar.Services
{
    public class TaxJarCalculator : ITaxCalculator
    {
        public TaxjarApi client;

        public TaxJarCalculator()
        {
            var apiKey = ConfigurationService.AppSetting["TaxJarApiKey"];
            client = new TaxjarApi(apiKey);
        }

        public async Task<decimal> CalculateTaxesForOrder(Models.Order order)
        {
            var taxOrder = new Taxjar.Order()
            {
                FromCountry = "US",
                FromState = order.FromState,
                ToCountry = order.Country,
                ToState = order.ToState,
                ToZip = order.ToZip,
                Shipping = 0,
                Amount = order.GetSubTotal()
            };

            var result = await client.TaxForOrderAsync(taxOrder);

            if (result != null)
            {
                return result.AmountToCollect;
            }
            return 0m;
        }


        public async Task<decimal> GetRateForLocation(string toZip)
        {
            var result = await client.RatesForLocationAsync(toZip);


            if (result != null)
                return result.CombinedRate;

            return 0m;
        }
    }
}
