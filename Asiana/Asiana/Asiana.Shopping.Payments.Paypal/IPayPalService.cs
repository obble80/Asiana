using System;
using Asiana.Shopping.Services.Basket;
using Asiana.Shopping.Services.Orders;
namespace Asiana.Shopping.Payments.Paypal
{
    public interface IPayPalService
    {
        ExpressCheckoutResponse ExpressCheckout(Basket basket);
        GetShippingDetailsResponse ShippingDetails(string token);
        ConfirmPaymentResponse ConfirmPayment(Order order);
        RefundPaymentResponse RefundPayment(PayPalPayment payment);
    }
}
