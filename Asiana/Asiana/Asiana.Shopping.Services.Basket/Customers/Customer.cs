using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Asiana.Shopping.Services.Orders;
using Asiana.Profile.Services.Account;

namespace Asiana.Shopping.Services.Customers
{
    public class Customer
    {
        /// <summary>
        /// The customers title
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// The customers First Name
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// The customers Last Name
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// The customer type (E.g. registered, anonymous, paypal)
        /// </summary>
        public string CustomerType { get; set; }

        /// <summary>
        /// The unique reference for this customer
        /// </summary>
        public long CustomerID { get; set; }

        /// <summary>
        /// This property is used for references from other providers such as PayPal or FaceBook
        /// </summary>
        public string ExternalReference { get; set; }

        /// <summary>
        /// The login for the customer
        /// </summary>
        public string Login { get; set; }

        /// <summary>
        /// The password for the customer
        /// </summary>
        /// <remarks>This should be a secure hash</remarks>
        public string Password { get; set; }

        /// <summary>
        /// The email address for the customer
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// The date and time this customer instance was initially created
        /// </summary>
        public DateTime DateCreated { get; set; }

        /// <summary>
        /// The default contact address for the customer
        /// </summary>
        public Address ContactAddress { get; set; }

        /// <summary>
        /// The orders placed by this customer
        /// </summary>
        public ICollection<Order> Orders { get; set; }

        /// <summary>
        /// The addresses saved by this customer
        /// </summary>
        public ICollection<Address> Addresses { get; set; }

        public String Name {
            get
            {
                if (!String.IsNullOrWhiteSpace(this.Title))
                {
                    return String.Format("{0}. {1} {2}", this.Title, this.FirstName, this.LastName);
                }
                else
                {
                    return String.Format("{0} {1}", this.FirstName, this.LastName);
                }
            }
        }
    }
}
