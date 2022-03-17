using System;
using System.Collections.Generic;
using System.Text;

namespace TaxCalculator.Models.Models
{
    public class Location
    {
        public string Country { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string ZipCode { get; set; }
    }
}
