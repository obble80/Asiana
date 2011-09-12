using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Asiana.Profile.Services.Account;
using Asiana.Shopping.Services.Customers;

namespace Asiana.Shopping.Services.Basket
{
    public interface IBasketService
    {
        /// <summary>
        /// Gets the basket for an anonymous user
        /// </summary>
        /// <returns></returns>
        Basket GetBasket();

        /// <summary>
        /// Gets the basket for a registered or known user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Basket GetBasket(Customer customer);
    }
}
