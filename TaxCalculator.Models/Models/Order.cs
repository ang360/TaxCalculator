using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TaxCalculator.Models.Models
{
    public class Order
    {
        [JsonProperty("transaction_id")]
        public string Transaction_Id { get; set; }

        [JsonProperty("transaction_date")]
        public string Transaction_Date { get; set; }

        [JsonProperty("provider", NullValueHandling = NullValueHandling.Ignore)]
        public string Provider { get; set; }

        [JsonProperty("exemption_type", NullValueHandling = NullValueHandling.Ignore)]
        public string Exemption_Type { get; set; }

        [JsonProperty("from_country")]
        public string From_Country { get; set; }

        [JsonProperty("from_zip")]
        public string From_Zip { get; set; }

        [JsonProperty("from_state")]
        public string From_State { get; set; }

        [JsonProperty("from_city")]
        public string From_City { get; set; }

        [JsonProperty("from_street")]
        public string From_Street { get; set; }

        [JsonProperty("to_country")]
        public string To_Country { get; set; }

        [JsonProperty("to_zip")]
        public string To_Zip { get; set; }

        [JsonProperty("to_state")]
        public string To_State { get; set; }

        [JsonProperty("to_city")]
        public string To_City { get; set; }

        [JsonProperty("to_street")]
        public string To_Street { get; set; }

        [JsonProperty("amount", NullValueHandling = NullValueHandling.Ignore)]
        public decimal Amount { get; set; }

        [JsonProperty("shipping", NullValueHandling = NullValueHandling.Ignore)]
        public decimal Shipping { get; set; }

        [JsonProperty("sales_tax", NullValueHandling = NullValueHandling.Ignore)]
        public decimal SalesTax { get; set; }

        [JsonProperty("customer_id")]
        public string Customer_Id { get; set; }

        [JsonProperty("line_items")]
        public IEnumerable<LineItem> Line_Items { get; set; }
    }

    public class LineItem
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("quantity")]
        public int Quantity { get; set; }

        [JsonProperty("product_identifier")]
        public string Product_Identifier { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("product_tax_code")]
        public string Product_Tax_Code { get; set; }

        [JsonProperty("unit_price")]
        public decimal Unit_Price { get; set; }

        [JsonProperty("discount")]
        public decimal Discount { get; set; }

        [JsonProperty("sales_tax")]
        public decimal Sales_Tax { get; set; }
    }
}
