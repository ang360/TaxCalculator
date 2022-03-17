using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaxCalculator.Models.Models;
using TaxCalculator.Services.Services;

namespace TaxCalculator.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaxCalculatorController : ControllerBase
    {
        private ITaxCalculator _taxCalculator;
        public TaxCalculatorController(ITaxCalculator taxCalculator)
        {
            this._taxCalculator = taxCalculator;
        }


        [Route("GetTaxRatesForLocation/{ZipCode}")]
        [HttpGet]
        public async Task<IActionResult> GetTaxRatesForLocation(string ZipCode, string Country, string State, string City, string Street)
        {
            var location = new Location
            {
                Country = Country,
                State = State,
                City = City,
                Street = Street,
                ZipCode = ZipCode
            };
            return Ok(await this._taxCalculator.GetTaxRatesForLocation(location));
        }

        [Route("GetTaxesForOrder")]
        [HttpPost]
        public void GetTaxesForOrder(Order order)
        {
            //return Ok(this._taxCalculator.GetTaxesForOrder(order));
        }
    }
}
