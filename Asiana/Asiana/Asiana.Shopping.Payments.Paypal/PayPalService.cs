using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Asiana.Shopping.Services.Basket;
using Asiana.Shopping.Services.Orders;
using Asiana.Site.Services;

namespace Asiana.Shopping.Payments.Paypal
{
    public class PayPalService :IPayPalService
    {
       
        private bool sandBox = true;

        public PayPalService()
        {
        }

        private PayPalExecutorService GetExecutor()
        {
            if (sandBox)
            {
                //TODO: Get this from a service locator
                PayPalExecutorService executorService = new PayPalExecutorService("www.sandbox.paypal.com",
                    "https://api-3t.sandbox.paypal.com/nvp",
                    "seller_1304321426_biz_api1.gmail.com",
                    "1304321435",
                    "AMOeq0WCZpnGEtR-lNwLoqHOcjCdAn6LzhVI-Ksbm-wgCC1vF5VTyiqq");

                return executorService;
            }
            else
            {
                //TODO: Get this from a service locator
                PayPalExecutorService executorService = new PayPalExecutorService("www.paypal.com",
                    "https://api-3t.paypal.com/nvp",
                    "adamlukecarden_api1.hotmail.com",
                    "5SM3CFXFDGTP2V5S",
                    "Ai6OsGd.AggboYhKRwPVEn5tTAgXAZt.x074vGZzMUKPy5zCs8ACQrw.");

                return executorService;
            }
        }
        
        public ExpressCheckoutResponse ExpressCheckout(Basket basket)
        {

            var executor = GetExecutor();
            ExpressCheckoutRequest request = new ExpressCheckoutRequest(executor);
            request.Currency = "GBP";

            return request.Execute(basket);
        }

        public GetShippingDetailsResponse ShippingDetails(string token)
        {
            var executor = GetExecutor();

            GetShippingDetailsRequest request = new GetShippingDetailsRequest(executor);
            request.Token = token;
            return request.Execute();
        }

        //TODO: This should be an order!
        public ConfirmPaymentResponse ConfirmPayment(Order order)
        {
            var executor = GetExecutor();

            ConfirmPaymentRequest request = new ConfirmPaymentRequest(executor);
            request.Currency = "GBP";
            return request.Execute(order);
        }

        public RefundPaymentResponse RefundPayment(PayPalPayment payment)
        {
            var executor = GetExecutor();

            RefundPaymentRequest request = new RefundPaymentRequest(executor);
            request.Currency = "GBP";
            return request.Execute(payment);
        }
    }
}
