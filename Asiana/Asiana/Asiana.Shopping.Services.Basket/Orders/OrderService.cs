using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Asiana.Shopping.Services.Data;
using Asiana.Shopping.Services.Customers;

namespace Asiana.Shopping.Services.Orders
{
    public class OrderService : IOrderService
    {
        private Fashinon dbContext;

        public OrderService(Fashinon dbContext)
        {
            this.dbContext = dbContext;
        }

        #region IOrderService Members

        public IEnumerable<Order> GetOrders(Customer customer)
        {
            throw new NotImplementedException();
        }

        public Order GetCurrentOrder()
        {
            Order order;

            
            order = HttpRuntime.Cache.Get("order") as Order; //TODO: Get real user
            dbContext.Set<Order>().Attach(order);

            return order;            
        }

        public void SetCurrentOrder(Order order)
        {
           HttpRuntime.Cache.Insert("order", order);
        }

        public void SaveOrder(Order order)
        {
            dbContext.Update(order);
        }

        #endregion
    }
}
