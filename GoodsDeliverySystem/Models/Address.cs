using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GoodsDeliverySystem.Models
{
    public class Address
    {
        public string street { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public int zip { get; set; }

        public Address(string street, string city, string state, int zip)
        {
            this.street = street;
            this.city = city;
            this.state = state;
            this.zip = zip;
        }
    }
}