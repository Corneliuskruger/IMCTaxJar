using IMCTaxJar.Interfaces;
using System.Threading.Tasks;

namespace IMCTaxJar.Services
{
    public class TaxService
    {
        ITaxCalculator Calculator;
        public TaxService(ITaxCalculator calculator)
        {
            this.Calculator = calculator;
        }

        public async Task<decimal> GetTaxPercentage(string toZip)
        {
            if (Calculator != null)
            {
                var result = await Calculator.GetRateForLocation(toZip);
                return result;
            }

            return 0;
        }

        public async Task<decimal> GetTaxAmount(Models.Order order)
        {
            if (Calculator != null)
            {
                var result = await Calculator.CalculateTaxesForOrder(order);
                return result;
            }

            return 0;
        }

        public async Task<decimal> GetOrderTotal(Models.Order order)
        {
            var result = order.GetSubTotal();
            result += await Calculator.CalculateTaxesForOrder(order);
            return result;
        }
    }
}
