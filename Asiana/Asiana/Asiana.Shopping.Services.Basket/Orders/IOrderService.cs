using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Asiana.Profile.Services.Account;
using Asiana.Shopping.Services.Customers;

namespace Asiana.Shopping.Services.Orders
{
    public interface IOrderService
    {
        IEnumerable<Order> GetOrders(Customer customer);
        Order GetCurrentOrder();
        void SetCurrentOrder(Order order);
        void SaveOrder(Order order);
    }
}
