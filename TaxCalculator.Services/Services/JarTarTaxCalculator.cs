using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TaxCalculator.Models.Models;
using System.Text.Json;
using Newtonsoft.Json;

namespace TaxCalculator.Services.Services
{
    public class JarTarCalculator : ITaxCalculator
    {
        IHttpClientFactory _clientFactory;
        private string token = "Token token=\"5da2f821eee4035db4771edab942a4cc\"";
        private string taxJarURL = "https://api.taxjar.com/v2/";
        private string rateControllerURL = "rates/";
        private string taxControllerURL = "tax/";
        public JarTarCalculator(IHttpClientFactory clientFactory)
        {
            this._clientFactory = clientFactory;
        }

        private string GetOptionalParameter(bool isEmpty, string field, string newParameter)
        {
            if (newParameter == null) return string.Empty;
            if (isEmpty) return "?" + field + "=" + newParameter;
            else return "&" + field + "=" + newParameter;
        }

        public async Task<decimal> GetTaxRatesForLocation(Location location)
        {
            string optionalParameters = String.Empty;
            optionalParameters += this.GetOptionalParameter(optionalParameters == String.Empty, "country", location.Country);
            optionalParameters += this.GetOptionalParameter(optionalParameters == String.Empty, "city", location.City);
            optionalParameters += this.GetOptionalParameter(optionalParameters == String.Empty, "street", location.Street);
            optionalParameters += this.GetOptionalParameter(optionalParameters == String.Empty, "state", location.State);

            var URL = this.taxJarURL + this.rateControllerURL + location.ZipCode + optionalParameters;
            var request = new HttpRequestMessage(HttpMethod.Get, URL);
            request.Headers.Add("Authorization", token);
            var client = _clientFactory.CreateClient();
            var response = await client.SendAsync(request);
            var json = await response.Content.ReadAsStringAsync();
            var responseData = JsonConvert.DeserializeObject<JarTarRate>(json);
            return responseData.Rate.CombinedRate + responseData.Rate.StandardRate;
        }

        public void GetTaxesForOrder(Order order)
        {
        }
    }
}
