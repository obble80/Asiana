using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Asiana.Profile.Services.Account;
using Asiana.Shopping.Services.Customers;

namespace Asiana.Shopping.Payments.Paypal
{
    public class GetShippingDetailsResponse
    {
        public Address ShippingAddress { get; set; }
        public Customer Customer { get; set; }
    }
}
