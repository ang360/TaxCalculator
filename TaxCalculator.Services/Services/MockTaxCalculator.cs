using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TaxCalculator.Models.Models;

namespace TaxCalculator.Services.Services
{
    public class MockTaxCalculator : ITaxCalculator
    {
        public async Task<decimal> GetTaxRatesForLocation(Location location)
        {
            throw new NotImplementedException("This method is not implemented");
        }

        public async Task<decimal> GetTaxesForOrder(Order order)
        {
            throw new NotImplementedException("This method is not implemented");
        }
    }
}
