using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GoodsDeliverySystem.Models
{
    public class Road
    {
        public string city { get; private set; }
        public int time { get; private set; }

        public Road(string city, int time)
        {
            this.city = city;
            this.time = time; //time is how long it takes to get to the current city from the previous
                              //city in the particular path
        }
    }
}