using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Asiana.Shopping.Payments.Paypal
{
    public class ExpressCheckoutResponse
    {
        public string Token { get; set; }
        public string Message { get; set; }
        public string RedirectUrl { get; set; }
    }
}
