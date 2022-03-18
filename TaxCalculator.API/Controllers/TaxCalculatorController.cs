using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
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
        private ITaxCalculator _taxCalculator;
        private readonly IConfiguration _config;

        public TaxCalculatorController(ServiceResolver _taxCalculator, IConfiguration config)
        {
            _config = config;
            this._taxCalculator = _taxCalculator(_config.GetValue<string>("AppIdentitySettings:Client"));
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
