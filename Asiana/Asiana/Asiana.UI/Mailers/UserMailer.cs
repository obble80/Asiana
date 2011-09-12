using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ActionMailer.Net;
using System.Net.Mail;
using Asiana.Shopping.Services.Orders;

namespace Asiana.UI.Mailers
{ 
    public class UserMailer : MailerBase, IUserMailer     
	{
		
		public EmailResult ConfirmOrder(Order order)
		{
            To.Add(order.Customer.Email);
            From = "no-reply@mycoolsite.com";
            Subject = "Fashinon Order Confirmation";
            return Email("ConfirmOrder", order);
		}

		
	}
}