using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
namespace Asiana.Shopping.Services.Payments
{
    public abstract class Payment
    {
        public long PaymentID { get; set; }
        public String TransactionID { get; set; }
        public String CorrelationID { get;set;}
        public String Status { get; set; }
        [Display(Name="Currency")]
        public String CurrencyCode {get;set;}
        [Display(Name="Amount")]
        public decimal PaymentAmount {get;set;}
        [Display(Name="Payment Type")]
        public String Discriminator { get; set; }
    }
}
