using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using Asiana.Shopping.Services.Orders;
using ActionMailer.Net;

namespace Asiana.UI.Mailers
{ 
    public interface IUserMailer
    {
        EmailResult ConfirmOrder(Order order);
	}
}