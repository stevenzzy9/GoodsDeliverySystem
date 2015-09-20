using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GoodsDeliverySystem.Models
{
    public class ShipmentType
    {
        public int id { get; private set; }
        public string typeName { get; private set; }

        public ShipmentType(int id, string typeName)
        {
            this.id = id;
            this.typeName = typeName;
        }
    }
}