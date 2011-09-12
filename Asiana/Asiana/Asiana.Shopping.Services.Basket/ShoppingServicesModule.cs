using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ninject.Modules;
using Asiana.Shopping.Services.Products;
using Asiana.Shopping.Services.Orders;
using Asiana.Shopping.Services.Customers;
using Asiana.Shopping.Services.Data;
using Asiana.Shopping.Services.Shipping;
using System.Web;

namespace Asiana.Shopping.Services
{
    public class ShoppingServicesModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IProductService>().To<ProductService>();
            Bind<IOrderService>().To<OrderService>();
            Bind<ICustomerService>().To<CustomerService>();

            Bind<Fashinon>()
                .To<Fashinon>()
                .InRequestScope();

            Bind<IShippingService>()
                .To<ShippingService>()
                .WithConstructorArgument("definitionPaths", HttpContext.Current.Server.MapPath(@"~\Metadata\Solr\Asiana.xml"));
        }

    }
}
