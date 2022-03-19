using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TaxCalculator.Models.Models;
using System.Text.Json;
using Newtonsoft.Json;
using System.Net;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace TaxCalculator.Services.Services
{
    public class TaxJarTaxCalculator : ITaxCalculator
    {

        private readonly IHttpClientFactory _clientFactory; //Http Client Factory to request data
        private readonly IConfiguration _config; //Config file with client name, token, and url

        public TaxJarTaxCalculator(IHttpClientFactory clientFactory, IConfiguration config)
        {
            this._config = config;
            this._clientFactory = clientFactory;
        }


        //Get optional parameters for GetTaxRatesForLocation
        private string GetTaxRatesForLocationOptionalParameters(bool isEmpty, string field, string newParameter)
        {
            if (newParameter == null) return string.Empty;
            if (isEmpty) return "?" + field + "=" + newParameter;
            else return "&" + field + "=" + newParameter;
        }

        //Get Tax Rates for a specific location
        public async Task<decimal> GetTaxRatesForLocation(Location location)
        {
            //Validations
            if (location.ZipCode == null) throw new ArgumentNullException("Zip Code is a required field");
            //Create URL
            string optionalParameters = String.Empty;
            optionalParameters += this.GetTaxRatesForLocationOptionalParameters(optionalParameters == String.Empty, "country", location.Country);
            optionalParameters += this.GetTaxRatesForLocationOptionalParameters(optionalParameters == String.Empty, "city", location.City);
            optionalParameters += this.GetTaxRatesForLocationOptionalParameters(optionalParameters == String.Empty, "street", location.Street);
            optionalParameters += this.GetTaxRatesForLocationOptionalParameters(optionalParameters == String.Empty, "state", location.State);
            var baseURL = _config.GetValue<string>("ClientIdentity:TaxJar:API");
            var URL = baseURL + "rates/" + location.ZipCode + optionalParameters;

            //Request Tax Rates for location to the external API
            var request = new HttpRequestMessage(HttpMethod.Get, URL);
            request.Headers.Add("Authorization", _config.GetValue<string>("ClientIdentity:TaxJar:Token"));
            var client = _clientFactory.CreateClient();
            var response = await client.SendAsync(request);

            //Return Tax Rates for a specific location
            var json = await response.Content.ReadAsStringAsync();
            var responseData = JsonConvert.DeserializeObject<TaxJarRate>(json);
            if (responseData == null || responseData.Rate == null)
                throw new Exception("response is missing JSON");
            return responseData.Rate.CombinedRate + responseData.Rate.StandardRate;
        }

        //Get Taxes for a specific order
        public async Task<decimal> GetTaxesForOrder(Order order)
        {
            //Validations
            if (order.To_Country == null) throw new ArgumentNullException("To Country is a required field");
            //Create URL
            var baseURL = _config.GetValue<string>("ClientIdentity:TaxJar:API");
            //Request Get Taxes For Order to the external API
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(baseURL + "taxes/");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";
            httpWebRequest.Headers.Add("Authorization", _config.GetValue<string>("ClientIdentity:TaxJar:Token"));

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = JsonConvert.SerializeObject(order, Formatting.Indented);

                streamWriter.Write(json);
            }
            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            //Return Taxes for a specific order
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = await streamReader.ReadToEndAsync();
                var responseData = JsonConvert.DeserializeObject<TaxJarTax>(result);
                if (responseData == null || responseData.Tax == null)
                    throw new Exception("response is missing JSON");
                return responseData.Tax.AmountToCollect;
            }
        }
    }
}