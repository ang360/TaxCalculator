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
    public class JarTarTaxCalculator : ITaxCalculator
    {

        IHttpClientFactory _clientFactory;
        private readonly IConfiguration _config;

        public JarTarTaxCalculator(IHttpClientFactory clientFactory, IConfiguration config)
        {
            _config = config;
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

            var URL = _config.GetValue<string>("ClientIdentity:JarTar:API") + "rates/" + location.ZipCode + optionalParameters;
            var request = new HttpRequestMessage(HttpMethod.Get, URL);
            request.Headers.Add("Authorization", _config.GetValue<string>("ClientIdentity:JarTar:Token"));
            var client = _clientFactory.CreateClient();
            var response = await client.SendAsync(request);
            var json = await response.Content.ReadAsStringAsync();
            var responseData = JsonConvert.DeserializeObject<JarTarRate>(json);
            return responseData.Rate.CombinedRate + responseData.Rate.StandardRate;
        }

        public async Task<decimal> GetTaxesForOrder(Order order)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(_config.GetValue<string>("ClientIdentity:JarTar:API") + "taxes/");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";
            httpWebRequest.Headers.Add("Authorization", "Token token=" + _config.GetValue<string>("ClientIdentity:JarTar:Token"));

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = JsonConvert.SerializeObject(order, Formatting.Indented);

                streamWriter.Write(json);
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = await streamReader.ReadToEndAsync();
                var responseData = JsonConvert.DeserializeObject<JarTarTax>(result);
                var test = responseData.Tax.AmountToCollect;
                return test;
            }
        }
    }
}