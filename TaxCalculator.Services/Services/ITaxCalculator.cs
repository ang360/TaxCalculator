using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TaxCalculator.Models.Models;

namespace TaxCalculator.Services.Services
{
    public interface ITaxCalculator
    {
        public Task<decimal> GetTaxRatesForLocation(Location location);
        public decimal GetTaxesForOrder(Order order);
    }
}
