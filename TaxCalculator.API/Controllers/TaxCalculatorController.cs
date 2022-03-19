using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Threading.Tasks;
using TaxCalculator.Loggers;
using TaxCalculator.Models.Models;
using TaxCalculator.Services.Services;
using static TaxCalculator.API.Startup;

namespace TaxCalculator.API.Controllers
{
    //Handling Errors Globally with the Built-In Middleware
    //The system can still use try-catch when you want to handle the exceptions 

    //Controller for our Tax Calculator service
    [Route("api/[controller]")]
    [ApiController]
    public class TaxCalculatorController : ControllerBase
    {
        private ServiceResolver taxCalculator; //Used to determine which Calculator service to use
        private ITaxCalculatorLogger _iTaxCalculatorLogger; //Custom Logger to log events

        public TaxCalculatorController(ServiceResolver taxCalculator, ITaxCalculatorLogger taxCalculatorLogger)
        {
            this.taxCalculator = taxCalculator;
            this._iTaxCalculatorLogger = taxCalculatorLogger;
        }

        //Get Tax Rates for a specific location
        [Route("GetTaxRatesForLocation/{ZipCode}")]
        [HttpGet]
        public async Task<IActionResult> GetTaxRatesForLocation(string ZipCode, string Country, string State, string City, string Street)
        {
            //Log http request
            this._iTaxCalculatorLogger.LogEvent("TaxCalculatorController.GetTaxRatesForLocation has been called");
            //Simple validation
            if (ZipCode == null) 
                return BadRequest("Zip Code is a required field");
            if (!Request.Headers.ContainsKey("ClientID") || Request.Headers["ClientID"] == String.Empty)
                return BadRequest("ClientID Header is a required field");
            
            var location = new Location
            {
                ZipCode = ZipCode,
                Country = Country,
                State = State,
                City = City,
                Street = Street,
            };

            //Get Tax Rates for a specific location using the specified Calculator service
            //The calculator service to be implemented depends on the client. The http request has a header "ClientID" with the ID of the client
            try
            {
                return Ok(await this.taxCalculator(Request.Headers["ClientID"]).GetTaxRatesForLocation(location));
            }
            catch (NotImplementedException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //Get Taxes for a specific order
        [HttpPost]
        [Route("GetTaxesForOrder")]
        public async Task<IActionResult> GetTaxesForOrder([FromBody]Order order)
        {
            //Log http request
            this._iTaxCalculatorLogger.LogEvent("TaxCalculatorController.GetTaxesForOrder has been called");
            //Simple validation
            if (order.To_Country == null)
                return BadRequest("To Country is a required field");
            if (!Request.Headers.ContainsKey("ClientID") || Request.Headers["ClientID"] == String.Empty)
                return BadRequest("ClientID Header is a required field");

            //Get Taxes for a specific order using the specified Calculator service
            //The calculator service to be implemented depends on the client. The http request has a header "ClientID" with the ID of the client
            try
            {
                return Ok(await this.taxCalculator(Request.Headers["ClientID"]).GetTaxesForOrder(order));
            }
            catch (NotImplementedException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
