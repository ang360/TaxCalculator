using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TaxCalculator.Models.Models
{
    //TaxJar Tax JSON Response
    public class TaxJarTax
    {
        [JsonProperty("tax")]
        public TaxJarTaxAttributes Tax { get; set; }
    }

    public class TaxJarTaxAttributes
    {
        [JsonProperty("amount_to_collect")]
        public decimal AmountToCollect { get; set; }
    }
}
