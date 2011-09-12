using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Asiana.UI.Services;
using Asiana.Shopping.Payments.Paypal;
using Asiana.Shopping.Services.Basket;
using Asiana.Shopping.Services.Orders;
using Asiana.Shopping.Services.Shipping;
using Asiana.Shopping.Services.Payments;
using Asiana.Shopping.Services.Data;
using System.Data.Entity.Validation;
using Asiana.Shopping.Services.Customers;

namespace Asiana.UI.Controllers
{
    public class PayPalController : Controller {
    
        private ILogService logService;
        private IBasketService basketService;
        private IPayPalService payPalService;
        private IShippingService shippingService;
        private IOrderService orderService;
        private ICustomerService customerService;
        private Fashinon dbContext;

        public PayPalController(
            ILogService logService, 
            IBasketService basketService, 
            IPayPalService payPalService, 
            IShippingService shippingService,
            IOrderService orderService,
            ICustomerService customerService,
            Fashinon dbContext)
        {
            this.logService = logService;
            this.basketService = basketService;
            this.payPalService = payPalService;
            this.shippingService = shippingService;
            this.orderService = orderService;
            this.customerService = customerService;
            this.dbContext = dbContext;
        }

        //
        // GET: /Paypal/

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult IPN(ExpressCheckoutIPNResponse webAcceptResponse)
        {
            //logService.Log("Hello");
           logService.Log(webAcceptResponse.Custom);
           logService.Log(webAcceptResponse.First_Name);
           logService.Log(webAcceptResponse.Last_Name);
            return View();
        }

        [HttpPost]
        public ActionResult Pay()
        {
            // Get Basket
            var basket = basketService.GetBasket();
            
            // Get PayPal Service
            // Make PayPal Express Checkout Request
            var expressCheckoutResponse = payPalService.ExpressCheckout(basket);
            

            return Redirect(expressCheckoutResponse.RedirectUrl);
        }

       

        public ActionResult Shipping(string token, string payerId)
        {
            // Get the address details from the paypal service
            var response = payPalService.ShippingDetails(token);
            var basket = basketService.GetBasket();


            var customer = dbContext.Customers.Where(x => x.ExternalReference == response.Customer.ExternalReference).SingleOrDefault();

            if (customer == null)
            {
                // We need to save the customer
                dbContext.Customers.Add(response.Customer);
                dbContext.SaveChanges();
            }
            else
            {
                response.Customer = customer;
            }

            // Creates a new paypal payment
            var payment = new PayPalPayment();
            payment.PayerID = payerId;
            payment.Token = token;
            payment.Status = "Pending";
   
            Order order = new Order();
            order.ShippingAddress = response.ShippingAddress;
            order.Customer = response.Customer;
            order.Items = basket.Items;
            order.Payment = payment;
            order.Status = "Initial";
            order.Created = DateTime.Now;
            order.Updated = DateTime.Now;

            // Selects the first shipping method the customer can select another later in order summary
            order.ShippingMethod = shippingService.GetShippingMethods(order.ShippingAddress, order.TotalWeight, 0, 0, 0).First();
            
            //TODO: Remove this it will not be needed much longer!
            orderService.SetCurrentOrder(order);

            //TODO: Establish the customer here
            customerService.EstablishCustomer(order.Customer);

            
            dbContext.Orders.Add(order);

            try
            {
                dbContext.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                var error = ex.ToString();
            }
            //var confirm = payPalService.ConfirmPayment(basket);

            return RedirectToAction("Shipping", "Order");
        }

        public ActionResult Cancel()
        {
            return View("IPN");
        }
    }
}
