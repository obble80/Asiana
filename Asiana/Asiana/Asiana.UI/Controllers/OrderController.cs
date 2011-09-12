using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Asiana.Shopping.Services.Basket;
using Asiana.UI.Models;
using System.Xml.Linq;
using Asiana.Shopping.Services.Orders;
using Asiana.Shopping.Services.Shipping;
using Asiana.Shopping.Services.Payments;
using Asiana.Shopping.Payments.Paypal;
using Asiana.Shopping.Services.Data;
using Asiana.UI.Mailers;
using ActionMailer.Net;

namespace Asiana.UI.Controllers
{
    public class OrderController : Controller
    {

        IOrderService orderService;
        IShippingService shippingService;
        IPayPalService payPalService;
        Fashinon dbContext;

        public OrderController(IOrderService orderService, IShippingService shippingService, IPayPalService payPalService, Fashinon dbContext)
        {
            this.orderService = orderService;
            this.shippingService = shippingService;
            this.payPalService = payPalService;
            this.dbContext = dbContext;
        }
        //
        // GET: /Order/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Pay()
        {
            var order = orderService.GetCurrentOrder();

            if (order.Payment is PayPalPayment)
            {
                var payPalResponse = payPalService.ConfirmPayment(order);
                order.Status = "Placed";
                order.Updated = DateTime.Now;
                dbContext.SaveChanges();
            }

            IUserMailer mailer = new UserMailer();
             mailer.ConfirmOrder(order).Deliver();
            

            return RedirectToAction("Confirm");
        }
        public ActionResult Confirm()
        {
            var order = orderService.GetCurrentOrder();

            XElement navigation = XElement.Load(Server.MapPath(@"~\MetaData\Navigation.xml"));
            var model = new CheckoutModel() { Order = order, TopNavigation = navigation };

            return View(model);
        }

        public ActionResult Shipping()
        {
            var order = orderService.GetCurrentOrder();
            var shippingMethods = shippingService.GetShippingMethods(order.ShippingAddress, order.TotalWeight, 0, 0, 0);
            XElement navigation = XElement.Load(Server.MapPath(@"~\MetaData\Navigation.xml"));
            var model = new CheckoutModel() { Order = order, TopNavigation = navigation, ShippingMethods = shippingMethods };
            return View(model);
        }

        public ActionResult UpdateShipping(long shippingMethodId)
        {
            var shippingMethod = shippingService.GetShippingMethod(shippingMethodId);

            var order = orderService.GetCurrentOrder();
            var shippingMethods = shippingService.GetShippingMethods(order.ShippingAddress, order.TotalWeight, 0, 0, 0);
            order.ShippingMethod = shippingMethod;

            orderService.SaveOrder(order);

            XElement navigation = XElement.Load(Server.MapPath(@"~\MetaData\Navigation.xml"));

            var model = new CheckoutModel() { Order = order, TopNavigation = navigation, ShippingMethods = shippingMethods };
            return PartialView("OrderSummary", model);
        }
    }
}
