using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TaxCalculator.Models.Models
{
    public class Order
    {
        //public string FromCountry { get; set; }
        //public string FromZip { get; set; }
        //public string FromState { get; set; }
        //public string FromCity { get; set; }
        //public string FromStreet { get; set; }
        //public string ToCountry { get; set; }
        //public string ToZip { get; set; }
        //public string ToState { get; set; }
        //public string ToCity { get; set; }
        //public string ToStreet { get; set; }
        //public float Amount { get; set; }
        //public float Shipping { get; set; }
        //public string CustomerID { get; set; }
        //public string ExemptionType { get; set; }
        [JsonProperty("from_country")]
        public string FromCountry { get; set; }
        [JsonProperty("from_zip")]
        public string FromZip { get; set; }
        [JsonProperty("from_state")]
        public string FromState { get; set; }
        [JsonProperty("from_city")]
        public string FromCity { get; set; }
        [JsonProperty("from_street")]
        public string FromStreet { get; set; }
        [JsonProperty("to_country")]
        public string ToCountry { get; set; }
        [JsonProperty("to_zip")]
        public string ToZip { get; set; }
        [JsonProperty("to_state")]
        public string ToState { get; set; }
        [JsonProperty("to_city")]
        public string ToCity { get; set; }
        [JsonProperty("to_street")]
        public string ToStreet { get; set; }
        [JsonProperty("amount")]
        public float Amount { get; set; }
        [JsonProperty("shipping")]
        public float Shipping { get; set; }
        [JsonProperty("customer_id")]
        public string CustomerID { get; set; }
        [JsonProperty("exemption_type")]
        public string ExemptionType { get; set; }
    }
}
