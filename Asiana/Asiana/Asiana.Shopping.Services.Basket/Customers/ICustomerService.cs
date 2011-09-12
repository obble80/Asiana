using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Asiana.Shopping.Services.Customers
{
    public interface ICustomerService
    {
        Customer GetAnonymousCustomer();
        Customer GetCurrent();
        void EstablishCustomer(Customer customer);
    }
}
