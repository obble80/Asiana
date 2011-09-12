using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Asiana.Shopping.Services.Products;
using Asiana.Profile.Services.Account;
using Asiana.Shopping.Services.Customers;

namespace Asiana.Shopping.Services.Basket
{
    public class Basket
    {
        private List<Product> items = new List<Product>();
        private Customer customer;
        public bool TaxInclusive { get; set; }

        public Basket()
        {
            this.TaxInclusive = true; //TODO: From site services or products
        }

        public Basket(Customer customer)
        {
            this.TaxInclusive = true; //TODO: From site services or products
        }

        public void AddToBasket(Product product, Int32 quantity)
        {
            Product entry = items.Where(x => x.ProductID == product.ProductID).FirstOrDefault();
            if (entry != null)
            {
                entry.Quantity = entry.Quantity + quantity;
            }
            else
            {
                product.Quantity = product.Quantity + quantity;
                items.Add(product); // TODO: Get an id for the basket entry
            }
        }

        public decimal SubTotal
        {
            get
            {

                // TODO: All kinds of code related to promotions
                decimal total = 0;
                foreach (var item in items)
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
                var taxTotal = 0M;
                if (!this.TaxInclusive)
                {
                    var subTotal = this.SubTotal + this.DeliveryTotal;
                    taxTotal = (this.TaxRate / 100) * subTotal;
                }

                return taxTotal;
            }
        }

        public decimal DeliveryTotal
        {
            get
            {
                //TODO: Use delivery service
                return 4.95M;
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

        public decimal Discounts
        {
            get
            {
                return 0M;
            }
        }
        public decimal GrandTotal
        {
            get
            {
                //TODO: Use tax service
                return this.SubTotal + this.DeliveryTotal + this.Tax;
            }
        }

        public List<Product> Items
        {
            get { return items; }
            set { items = value; }
        }

        public Customer Customer
        {
            get
            {
                return customer;
            }

            set
            {
                customer = value;
            }
        }
    }
}
