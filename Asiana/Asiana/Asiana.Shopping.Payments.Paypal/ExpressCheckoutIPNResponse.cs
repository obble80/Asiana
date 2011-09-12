using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Asiana.Shopping.Payments.Paypal
{
    public class ExpressCheckoutIPNResponse
    {
        public string Custom { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public string TXN_ID { get; set; } 
    }
}
