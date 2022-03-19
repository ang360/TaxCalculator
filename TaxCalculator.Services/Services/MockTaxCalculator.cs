using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TaxCalculator.Models.Models;

namespace TaxCalculator.Services.Services
{
    //This is a mock of another service Calculator
    public class MockTaxCalculator : ITaxCalculator
    {
        public async Task<decimal> GetTaxRatesForLocation(Location location)
        {
            //To be implemented
            throw new NotImplementedException("This method is not implemented");
        }

        public async Task<decimal> GetTaxesForOrder(Order order)
        {
            //To be implemented
            throw new NotImplementedException("This method is not implemented");
        }
    }
}
