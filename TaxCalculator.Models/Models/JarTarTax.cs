using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TaxCalculator.Models.Models
{
    //JarTar Tax Jason Response
    public class JarTarTax
    {
        [JsonProperty("tax")]
        public JarTarTaxAttributes Tax { get; set; }
    }

    public class JarTarTaxAttributes
    {
        [JsonProperty("amount_to_collect")]
        public decimal AmountToCollect { get; set; }
    }
}
