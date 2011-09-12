using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Asiana.Shopping.Services.Basket;
using Ninject.Modules;
using Asiana.UI.Services;
using Asiana.Shopping.Services.Products;
using Asiana.Shopping.Payments.Paypal;
using NLog;
using Asiana.Shopping.Services.Orders;
using Asiana.Shopping.Services.Shipping;
using Asiana.Shopping.Services.Customers;
using Asiana.Shopping.Services.Data;
using Asiana.Site.Services;

namespace Asiana.UI
{
    public class AsianaModule : NinjectModule 
    {
        public override void Load()
        {
            Bind<IBasketService>().To<BasketService>();
            Bind<IDeepZoomService>().To<DeepZoomService>();
            //Bind<IProductService>().To<ProductService>();
            //Bind<IPayPalService>().To<PayPalService>();
            //Bind<IOrderService>().To<OrderService>();
            //Bind<ICustomerService>().To<CustomerService>();

            //Bind<Fashinon>()
            //    .To<Fashinon>()
            //    .InRequestScope();

            //Bind<IShippingService>()
            //    .To<ShippingService>()
            //    .WithConstructorArgument("definitionPaths", HttpContext.Current.Server.MapPath(@"~\Metadata\Solr\Asiana.xml"));

            Bind<ISiteService>().To<SiteService>();

            Logger logger = LogManager.GetLogger("MyClassName");

            Bind<ILogService>()
                .To<LogService>()
                .WithConstructorArgument("logger", logger);
        }
    }
}