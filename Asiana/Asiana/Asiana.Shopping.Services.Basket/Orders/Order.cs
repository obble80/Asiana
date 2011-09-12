using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using Asiana.Profile.Services.Account;
using Asiana.Shopping.Services.Payments;
using Asiana.Shopping.Services.Products;
using Asiana.Shopping.Services.Shipping;
using Asiana.Shopping.Services.Customers;

namespace Asiana.Shopping.Services.Orders
{
    public class Order
    {
        public long OrderID { get; set; }
        public string Status { get; set; }
        public long CustomerID { get; set; }

        public virtual Address ShippingAddress { get; set; }
        public virtual Address BillingAddress { get; set; }
        public virtual Payment Payment { get; set; }
        public virtual ShippingMethod ShippingMethod { get; set; }
        public virtual ICollection<Product> Items { get; set; }
        public virtual Customer Customer { get; set; }

        public DateTime? Created { get; set; }
        public DateTime? Updated { get; set; }
        public DateTime? Completed { get; set; }


        public decimal TotalWeight
        {
            get
            {
                decimal weight = 0;
                foreach (var item in Items)
                {
                    weight = weight + item.Weight;
                }

                return weight;
            }
        }

        public decimal SubTotal
        {
            get
            {

                // TODO: All kinds of code related to promotions
                decimal total = 0;
                foreach (var item in Items)
                {
                    total = total + item.Total;
                }

                return total;
            }
        }

        public decimal Tax
        {
            get
            {
                var subTotal = this.SubTotal + this.ShippingMethod.Cost;
                var taxTotal = (this.TaxRate / 100) * subTotal;

                return taxTotal;
            }
        }

        public decimal TaxRate
        {
            get
            {
                //TODO: Use tax service
                return 20M;
            }
        }

        public decimal GrandTotal
        {
            get
            {
                //TODO: What about discounts
                decimal total = 0;
                foreach (var item in Items)
                {
                    total = total + item.Total;
                }

                total = total + this.ShippingMethod.Cost;

                return total;
            }
        }
        
   
    }
}
