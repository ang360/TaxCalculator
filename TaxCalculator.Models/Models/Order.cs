using System;
using System.Collections.Generic;
using System.Text;

namespace TaxCalculator.Models.Models
{
    public class Order
    {
        public string FromCountry { get; set; }
        public string FromZip { get; set; }
        public string FromState { get; set; }
        public string FromCity { get; set; }
        public string FromStreet { get; set; }
        public string ToCountry { get; set; }
        public string ToZip { get; set; }
        public string ToState { get; set; }
        public string ToCity { get; set; }
        public string ToStreet { get; set; }
        public float Amount { get; set; }
        public float Shipping { get; set; }
        public string CustomerID { get; set; }
        public string ExemptionType { get; set; }
        public IEnumerable<NexusAddress> NexusAddress { get; set; }
        public IEnumerable<LineItem> LineItem { get; set; }
    }

    public class NexusAddress
    {
        public string Id { get; set; }
        public string Country { get; set; }
        public string Zip { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
    }

    public class LineItem
    {
        public string Id { get; set; }
        public int Quantity { get; set; }
        public string ProductTaxCode { get; set; }
        public float UnitPrice { get; set; }
        public float Discount { get; set; }
    }
}
