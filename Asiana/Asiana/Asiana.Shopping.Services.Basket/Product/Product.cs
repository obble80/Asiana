using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Asiana.Shopping.Services.Promotions;

namespace Asiana.Shopping.Services.Products
{
    public class Product
    {
        private List<Promotion> appliedPromotions = new List<Promotion>();
        private List<Promotion> applicablePromotions = new List<Promotion>();

        public List<Promotion> AppliedPromotions
        {
            get { return appliedPromotions; }
            set { appliedPromotions = value; }
        }
       
        public List<Promotion> ApplicablePromotions
        {
            get { return applicablePromotions; }
            set { applicablePromotions = value; }
        }

        public String ProductID { get; set; }

        public long LineItemID { get; set; }
        public long OrderID { get;set;}
        public String Name { get; set; }
        public String Image { get; set; }
        public Decimal Price { get; set; }
        
        public Int32 Quantity { get; set; }
        public decimal Weight { get; set; }
        
        public decimal Total
        {
            get
            {
                return Price * Quantity;
            }
        }

        public override string ToString()
        {
            return String.Format("{0}x {1}", Quantity, Name);
        }
    }
}