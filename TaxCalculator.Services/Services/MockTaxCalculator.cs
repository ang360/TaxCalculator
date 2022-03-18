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
            return 0;
        }

        public decimal GetTaxesForOrder(Order order)
        {
            return 0;
        }
    }
}
