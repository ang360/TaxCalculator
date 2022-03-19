using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TaxCalculator.Models.Models
{
    //TaxJar Rate JSON Response
    public class TaxJarRate
    {
        [JsonProperty("rate")]
        public TaxJarRateAttributes Rate { get; set; }
    }

    public class TaxJarRateAttributes
    {
        [JsonProperty("combined_rate")]
        public decimal CombinedRate { get; set; }

        // European Union
        [JsonProperty("standard_rate")]
        public decimal StandardRate { get; set; }
    }
}
