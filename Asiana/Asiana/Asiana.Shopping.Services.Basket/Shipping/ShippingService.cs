using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Asiana.Profile.Services.Account;
using Asiana.Shopping.Services.Data;

namespace Asiana.Shopping.Services.Shipping
{
    public class ShippingService : IShippingService
    {
        Fashinon dbContext;
        public ShippingService(Fashinon dbContext)
        {
            this.dbContext = dbContext;
        }

        public IEnumerable<ShippingMethod> GetShippingMethods(Address address, decimal weight, int width, int height, int depth)
        {
            return dbContext.ShippingMethods.ToList();
        }

        public IEnumerable<ShippingMethod> GetShippingMethods() {
            return dbContext.ShippingMethods.ToList();
        }

        public ShippingMethod GetShippingMethod(long id)
        {
            return dbContext.ShippingMethods.Where(x => x.ShippingMethodID == id).SingleOrDefault();
        }

        public ShippingMethod CreateShippingMethod(
            string carrier,
            string service,
            decimal minWeight = 0, decimal maxWeight = 0,
            int minWidth = 0, int maxWidth = 0,
            int minHeight = 0, int maxHeight = 0,
            int minDepth = 0, int maxDepth = 0,
            decimal minPrice = 0, decimal maxPrice = 0)
        {
            throw new NotImplementedException();
        }
    }
}
