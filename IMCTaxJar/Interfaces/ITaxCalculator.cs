using IMCTaxJar.Models;
using System.Threading.Tasks;

namespace IMCTaxJar.Interfaces
{
    public interface ITaxCalculator
    {
        public Task<decimal> GetRateForLocation(string toZip);

        public Task<decimal> CalculateTaxesForOrder(Order order);

    }
}
