using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Asiana.Shopping.Payments.Paypal
{
    public class RefundPaymentRequest
    {
        private const string METHOD = "RefundTransaction";

        public string Currency { get; set; }

        private PayPalExecutorService executorService;

        public RefundPaymentRequest(PayPalExecutorService executorService)
        {
            this.executorService = executorService;
        }

        public RefundPaymentResponse Execute(PayPalPayment payment,
            bool partial = false,
            decimal amount = 0M,
            string note = null)
        {
            NVPCodec encoder = new NVPCodec();
            encoder["TRANSACTIONID"] = payment.TransactionID;
            encoder["METHOD"] = METHOD;

            if (partial)
            {
                encoder["REFUNDTYPE"] = "Partial";
                encoder["AMT"] = amount.ToString("#.##"); // TODO: Reference Decimal Format for PayPal
                encoder["CURRENCYCODE"] = this.Currency;
            }
            else
            {
                encoder["REFUNDTYPE"] = "Full";
            }


            var decoder = executorService.Execute(encoder);

            string strAck = decoder["ACK"].ToLower();
            if (strAck != null && (strAck == "success" || strAck == "successwithwarning"))
            {
                payment.RefundTransactionID = decoder["REFUNDTRANSACTIONID"];
                payment.FeeRefundAmount = decimal.Parse(decoder["FEEREFUNDAMT"]);
                payment.GrossRefundAmount = decimal.Parse(decoder["GROSSREFUNDAMT"]);
                payment.NetRefundAmount = decimal.Parse(decoder["NETREFUNDAMT"]);
                //payment.TotalRefundAmount = decimal.Parse(decoder["TOTALREFUNDEDAMT"]);
                payment.Refunded = true;
                return new RefundPaymentResponse() { RefundPayment = payment };
            }
            else
            {

                throw new PayPalExeception(decoder["L_ERRORCODE0"], decoder["L_SHORTMESSAGE0"], decoder["L_LONGMESSAGE0"]);
            }
        }

    }

}
