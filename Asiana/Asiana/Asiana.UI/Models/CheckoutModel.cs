using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Asiana.Shopping.Services.Orders;
using System.Xml.Linq;
using Asiana.Shopping.Services.Shipping;

namespace Asiana.UI.Models
{
    public class CheckoutModel
    {
        public Order Order { get; set; }
        public XElement TopNavigation { get; set; }
        public IEnumerable<ShippingMethod> ShippingMethods { get; set; }
    }
}