using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Threading.Tasks;
using TaxCalculator.Models.Models;
using TaxCalculator.Services.Services;
using static TaxCalculator.API.Startup;

namespace TaxCalculator.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaxCalculatorController : ControllerBase
    {
        private ServiceResolver taxCalculator;

        public TaxCalculatorController(ServiceResolver taxCalculator)
        {
            this.taxCalculator = taxCalculator;
        }

        [Route("GetTaxRatesForLocation/{ZipCode}")]
        [HttpGet]
        public async Task<IActionResult> GetTaxRatesForLocation(string ZipCode, string Country, string State, string City, string Street)
        {
            var location = new Location
            {
                ZipCode = ZipCode,
                Country = Country,
                State = State,
                City = City,
                Street = Street,
            };
            return Ok(await this.taxCalculator(Request.Headers["ClientID"]).GetTaxRatesForLocation(location));
        }

        [HttpPost]
        [Route("GetTaxesForOrder")]
        public async Task<IActionResult> GetTaxesForOrder([FromBody]Order order)
        {
            return Ok(await this.taxCalculator(Request.Headers["ClientID"]).GetTaxesForOrder(order));
        }
    }
}
