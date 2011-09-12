using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Asiana.Shopping.Services.Basket;
using Asiana.Shopping.Services.Orders;
using Asiana.Shopping.Services.Payments;

namespace Asiana.Shopping.Payments.Paypal
{
    public class ConfirmPaymentRequest
    {
        private const string PAYMENT_ACTION = "Sale";
        private const string METHOD = "DoExpressCheckoutPayment";

        public string Currency { get; set; }

        private PayPalExecutorService executorService;

        public ConfirmPaymentRequest(PayPalExecutorService executorService)
        {
            this.executorService = executorService;
        }

        public ConfirmPaymentResponse Execute(Order order)
        {
            NVPCodec encoder = new NVPCodec();
            if (order.Payment is PayPalPayment)
            {
                var payment = order.Payment as PayPalPayment;

                encoder["METHOD"] = METHOD;
                encoder["TOKEN"] = payment.Token;
                encoder["PAYMENTACTION"] = "Sale";
                encoder["PAYERID"] = payment.PayerID;
                encoder["AMT"] = order.GrandTotal.ToString("#.##");
                encoder["CURRENCYCODE"] = this.Currency;


                var decoder = executorService.Execute(encoder);

                string strAck = decoder["ACK"].ToLower();
                if (strAck != null && (strAck == "success" || strAck == "successwithwarning"))
                {
                    payment.CorrelationID = decoder["CORRELATIONID"];
                    payment.CurrencyCode = decoder["CURRENCYCODE"];
                    payment.FeeAmount = decimal.Parse(decoder["FEEAMT"]);
                    payment.PaymentAmount = decimal.Parse(decoder["AMT"]);
                    payment.Status = decoder["PAYMENTSTATUS"];
                    payment.PendingReason = decoder["PENDINGREASON"];
                    payment.ReasonCode = decoder["REASONCODE"];
                    payment.TransactionID = decoder["TRANSACTIONID"];
                    return new ConfirmPaymentResponse();
                }
                else
                {

                    throw new PayPalExeception(decoder["L_ERRORCODE0"], decoder["L_SHORTMESSAGE0"], decoder["L_LONGMESSAGE0"]);
                }
            }
            else
            {
                throw new PayPalExeception("The payment method on the order is not valid for paypal confirmation");
            }   
        }
    }
}
