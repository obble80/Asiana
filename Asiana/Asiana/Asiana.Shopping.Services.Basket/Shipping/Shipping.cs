using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Asiana.Shopping.Services.Shipping
{
    public class Shipping
    {
        public ShippingMethod Method { get; set; }
        public decimal Cost { get; set; }
    }
}
