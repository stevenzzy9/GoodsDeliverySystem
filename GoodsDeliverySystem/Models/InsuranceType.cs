using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GoodsDeliverySystem.Models
{
    public class InsuranceType
    {
        public int id { get; private set; }
        public string type { get; private set; }

        public InsuranceType(int id, string type)
        {
            this.id = id;
            this.type = type;
        }
    }
}