using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TaxCalculator.Models.Models
{
    public class Location
    {
        public string Country { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        [Required()]
        [MinLength(5)]
        public string ZipCode { get; set; }
    }
}
