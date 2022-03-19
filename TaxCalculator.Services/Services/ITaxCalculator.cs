using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TaxCalculator.Models.Models;

namespace TaxCalculator.Services.Services
{
    //Interface for the Tax Calculator Service
    public interface ITaxCalculator
    {
        //Get Tax Rates for a specific location
        public Task<decimal> GetTaxRatesForLocation(Location location);
        //Get Taxes for a specific order
        public Task<decimal> GetTaxesForOrder(Order order);
    }
}
