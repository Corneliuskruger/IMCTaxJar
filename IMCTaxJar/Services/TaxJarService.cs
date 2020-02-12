using IMCTaxJar.Interfaces;
using IMCTaxJar.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Taxjar;

namespace IMCTaxJar.Services
{
    public class TaxJarService : ITaxService
    {
        public TaxjarApi client;

        public TaxJarService()
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

            return 0;
        }


        public async Task<decimal> GetRateForLocation(Models.Order order)
        {
            var result = await client.RatesForLocationAsync(order.ToZip);


            if (result != null)
                return result.CombinedRate;

            return 0m;
        }
    }
}
