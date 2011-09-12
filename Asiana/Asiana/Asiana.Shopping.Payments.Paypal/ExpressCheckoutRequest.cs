using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Asiana.Shopping.Services.Basket;
using System.Web;

namespace Asiana.Shopping.Payments.Paypal
{
    public class ExpressCheckoutRequest
    {
        public string Currency { get; set; }
        public string CancelUrl { get; set; }
        public string ReturnUrl { get; set; }

        private const string PAYMENT_ACTION = "Sale";
        private const string METHOD = "SetExpressCheckout";

        private PayPalExecutorService executorService;

        public ExpressCheckoutRequest(PayPalExecutorService executorService)
        {
            this.executorService = executorService;
        }

        public ExpressCheckoutResponse Execute(Basket basket)
        {
            HttpContext context = HttpContext.Current;
            HttpRequest request = context.Request;

            string domain = request.Url.Scheme + System.Uri.SchemeDelimiter + request.Url.Host + (request.Url.IsDefaultPort ? "" : ":" + request.Url.Port);

            //TODO: This needs to come from elsewhere
            this.ReturnUrl = domain + "/paypal/shipping";
            this.CancelUrl = domain + "/paypal/cancel";
            this.Currency = "GBP";

            NVPCodec encoder = new NVPCodec();

            encoder["METHOD"] = METHOD;
            encoder["RETURNURL"] = this.ReturnUrl;
            encoder["CANCELURL"] = this.CancelUrl;
            encoder["AMT"] = basket.SubTotal.ToString("#.##");
            encoder["PAYMENTACTION"] = PAYMENT_ACTION;
            encoder["CURRENCYCODE"] = this.Currency;

            ////Optional Shipping Address entered on the merchant site
            //encoder["SHIPTONAME"] = shipToName;
            //encoder["SHIPTOSTREET"] = shipToStreet;
            //encoder["SHIPTOSTREET2"] = shipToStreet2;
            //encoder["SHIPTOCITY"] = shipToCity;
            //encoder["SHIPTOSTATE"] = shipToState;
            //encoder["SHIPTOZIP"] = shipToZip;
            //encoder["SHIPTOCOUNTRYCODE"] = shipToCountryCode;

            var decoder = executorService.Execute(encoder);

            string strAck = decoder["ACK"].ToLower();
            if (strAck != null && (strAck == "success" || strAck == "successwithwarning"))
            {
                var token = decoder["TOKEN"];

                return new ExpressCheckoutResponse() { Token = token, RedirectUrl = "https://" + executorService.Host + "/cgi-bin/webscr?cmd=_express-checkout" + "&token=" + token };
            }
            else
            {

                throw new PayPalExeception(decoder["L_ERRORCODE0"], decoder["L_SHORTMESSAGE0"], decoder["L_LONGMESSAGE0"]);
            }
        }
    }
}
