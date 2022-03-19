using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TaxCalculator.Models.Models
{
    //JarTar Rate Jason Response
    public class JarTarRate
    {
        [JsonProperty("rate")]
        public JarTarRateAttributes Rate { get; set; }
    }

    public class JarTarRateAttributes
    {
        [JsonProperty("combined_rate")]
        public decimal CombinedRate { get; set; }

        // European Union
        [JsonProperty("standard_rate")]
        public decimal StandardRate { get; set; }
    }
}
