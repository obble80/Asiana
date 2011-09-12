using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Asiana.Shopping.Services.Data;
using System.Web;

namespace Asiana.Shopping.Services.Customers
{
    public class CustomerService : ICustomerService
    {
        private Fashinon dbContext;

        public CustomerService(Fashinon dbContext)
        {
            this.dbContext = dbContext;
        }
        #region ICustomerService Members

        public Customer GetAnonymousCustomer()
        {
            var context = new Fashinon();
            var customer = new Customer();

            customer.FirstName = "Anonymous";
            customer.LastName = "Anonymous";

            context.Customers.Add(customer);
            context.SaveChanges();



            return customer;
        }


        public Customer GetCurrent()
        {
            HttpContext context = HttpContext.Current;

            // Try and get the customer id from the session
            var customerCookie = context.Request["Customer"];
            long customerId = 0;
            Customer customer = null;

            if (long.TryParse(customerCookie, out customerId))
            {

                // Now try and get the customer from the cache (we want to avoid database lookups)
                customer = context.Cache.Get(String.Format("Customer:{0}", customerId)) as Customer;

                // We have to attach the customer to the context for change tracking
                dbContext.Customers.Attach(customer);

                if (customer == null)
                {
                    // Now we try and get the customer from the database, remebering to cache it!
                   
                    customer = dbContext.Customers.Where(x => x.CustomerID == customerId).SingleOrDefault();
                    context.Cache.Insert(String.Format("Customer:{0}", customer), customer);

                }
            }

            return customer;
        }

        /// <summary>
        /// Sets a cookie for the customer and caches the customer object
        /// </summary>
        /// <param name="customer">The customer to establish</param>
        public void EstablishCustomer(Customer customer)
        {
            HttpContext context = HttpContext.Current;

            context.Response.Cookies.Add(new HttpCookie("Customer",customer.CustomerID.ToString())
            {
                HttpOnly = true,
                Expires = DateTime.Now.AddDays(14), //TODO: Make configurable?
            });

            context.Cache.Insert(String.Format("Customer:{0}", customer.CustomerID), customer);
        }

        #endregion
    }
}
