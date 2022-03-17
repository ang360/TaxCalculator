using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
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
        public IActionResult GetTaxesForOrder()
        {
            using (var reader = new StreamReader(Request.Body))
            {
                var body = reader.ReadToEnd();
                Order order = JsonConvert.DeserializeObject<Order>(body);
                return Ok(this._taxCalculator.GetTaxesForOrder(order));
            }
        }
    }
}
