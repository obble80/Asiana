using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Asiana.Profile.Services.Account;

namespace Asiana.Shopping.Services.Shipping
{
    public interface IShippingService
    {
        IEnumerable<ShippingMethod> GetShippingMethods(Address address, decimal weight, int width, int height, int depth);
        IEnumerable<ShippingMethod> GetShippingMethods();
        ShippingMethod GetShippingMethod(long id);
    }
}
