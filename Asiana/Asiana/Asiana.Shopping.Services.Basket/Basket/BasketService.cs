using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Asiana.Profile.Services.Account;
using Asiana.Shopping.Services.Data;
using Asiana.Shopping.Services.Customers;

namespace Asiana.Shopping.Services.Basket
{
    public class BasketService : IBasketService
    {
        private Fashinon dbContext;

        public BasketService(Fashinon dbContext)
        {
            this.dbContext = dbContext;
        }

        #region IBasketService Members

        public Basket GetBasket()
        {
            Basket basket;

            basket = HttpRuntime.Cache.Get("basket") as Basket; //TODO: Get real user

            if (basket != null)
            {
                return basket;
            }
            else
            {
                basket = new Basket();
                HttpRuntime.Cache.Insert("basket", basket);
                return basket;
            }
        }

        public Basket GetBasket(Customer customer)
        {
            Basket basket;

            basket = HttpRuntime.Cache.Get("basket") as Basket; //TODO: Get real user

            if (basket != null) {
                return basket; 
            }
            else {
                basket = new Basket();
                HttpRuntime.Cache.Insert("basket", basket);
                return basket;
            }


        }

        #endregion
    }
}
