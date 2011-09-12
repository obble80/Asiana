using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Asiana.Profile.Services.Account;

namespace Asiana.Shopping.Services.Shipping
{
    public class ShippingMethod
    {
        public long ShippingMethodID { get; set; }
        public String Carrier { get; set; }
        public String Service { get; set; }
        public int MinDays { get; set; }
        public int MaxDays { get; set; }
        public decimal Cost { get; set; }
    }
}
