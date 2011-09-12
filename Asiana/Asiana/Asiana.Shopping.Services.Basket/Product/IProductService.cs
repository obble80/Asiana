using System;
using System.Collections.Generic;
namespace Asiana.Shopping.Services.Products
{
    public interface IProductService
    {
        IEnumerable<Product> GetProducts(IEnumerable<string> ids);
    }
}
