using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Asiana.Shopping.Payments.Paypal
{
    public class PayPalExeception : Exception
    {
        public string Code { get; set; }
        public string DetailedMaessage { get; set; }

        public PayPalExeception()
            : base()
        {
        }

        public PayPalExeception(string message)
            : base(message)
        {
        }

        public PayPalExeception(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public PayPalExeception(string code, string message, string detailedMessage)
            : base(message)
        {
            this.Code = code;
            this.DetailedMaessage = detailedMessage;
        }

        public PayPalExeception(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
