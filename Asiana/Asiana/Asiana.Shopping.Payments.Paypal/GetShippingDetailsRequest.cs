using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Asiana.Shopping.Services.Basket;
using Asiana.Profile.Services.Account;
using Asiana.Shopping.Services.Customers;

namespace Asiana.Shopping.Payments.Paypal
{
    public class GetShippingDetailsRequest
    {
        public string Token { get; set; }

        private const string METHOD = "GetExpressCheckoutDetails";

        private PayPalExecutorService executorService;

        public GetShippingDetailsRequest(PayPalExecutorService executorService)
        {
            this.executorService = executorService;
        }

        public GetShippingDetailsResponse Execute()
        {
            NVPCodec encoder = new NVPCodec();

            encoder["METHOD"] = METHOD;
            encoder["TOKEN"] = this.Token;

            var decoder = executorService.Execute(encoder);

            string strAck = decoder["ACK"].ToLower();
            if (strAck != null && (strAck == "success" || strAck == "successwithwarning"))
            {
                var address = new Address()
                {
                    Name = decoder["SHIPTONAME"],
                    AddressLineOne = decoder["SHIPTOSTREET"],
                    AddressLineTwo = decoder["SHIPTOSTREET2"],
                    Town = decoder["SHIPTOCITY"],
                    County = decoder["SHIPTOSTATE"],
                    PostCode = decoder["SHIPTOZIP"],
                    Country = decoder["SHIPTOCOUNTRYNAME"],
                    CountryCode = decoder["SHIPTOCOUNTRYCODE"]
                };

                var customer = new Customer() {
                    FirstName = decoder["FIRSTNAME"],
                    LastName = decoder["LASTNAME"],
                    Email = decoder["EMAIL"],
                    ContactAddress = address,
                    ExternalReference = decoder["PAYERID"],
                    CustomerType = "PayPal",
                    DateCreated = DateTime.Now
                };

                customer.Addresses = new List<Address>();
                customer.Addresses.Add(address);

                return new GetShippingDetailsResponse()
                {
                    ShippingAddress = address,
                    Customer = customer
                };
            }
            else
            {

                throw new PayPalExeception(decoder["L_ERRORCODE0"], decoder["L_SHORTMESSAGE0"], decoder["L_LONGMESSAGE0"]);
            }
        }
    }
}
