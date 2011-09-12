using System;
using System.Collections.Generic;
using System.Text;

namespace Asiana.Merchandising
{
    public class ProductSlot
    {
        public ICollection<ProductReference> Products { get; set; }
        public int Limit { get; set; }
    }
}
