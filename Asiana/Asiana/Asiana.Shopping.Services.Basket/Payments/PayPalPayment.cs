using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using Asiana.Shopping.Services.Payments;

namespace Asiana.Shopping.Payments.Paypal
{
    public class PayPalPayment : Payment
    {
        public String Token { get; set; }
        public String PayerID { get; set; }
        [Display(Name="PayPal Fee")]
        [DisplayFormat(DataFormatString="{0:c}", HtmlEncode=false)]
        public decimal FeeAmount {get;set;}
        [Display(Name="Reason for Pending Status")]
        public String PendingReason{get;set;}
        [Display(Name="Reason")]
        public String ReasonCode {get;set;}
        [Display(Name="Refunded")]
        public bool Refunded { get; set; }
        [Display(Name="Refund Transaction ID")]
        public String RefundTransactionID { get; set; }
        [Display(Name="Fee Refund Amount")]
        [DisplayFormat(DataFormatString = "{0:c}", HtmlEncode = false)]
        public decimal? FeeRefundAmount { get; set; }
        [Display(Name="Gross Refund Amount")]
        [DisplayFormat(DataFormatString = "{0:c}", HtmlEncode = false)]
        public decimal? GrossRefundAmount { get; set; }
        [Display(Name="Net Refund Amount")]
        [DisplayFormat(DataFormatString = "{0:c}", HtmlEncode = false)]
        public decimal? NetRefundAmount { get; set; }
        [Display(Name="Total Refund Amount")]
        [DisplayFormat(DataFormatString = "{0:c}", HtmlEncode = false)]
        public decimal? TotalRefundAmount { get; set; }
    }
}
