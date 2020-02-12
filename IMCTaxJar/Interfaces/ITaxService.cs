using IMCTaxJar.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IMCTaxJar.Interfaces
{
    public interface ITaxService
    {
        public Task<decimal> GetRateForLocation(Order order);

        public Task<decimal> CalculateTaxesForOrder(Order order);

    }
}
