using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Asiana.UI.Filters
{
    using System.Web;
    using System.Web.Mvc;
    using System.IO.Compression;

    public class MailCSS : ActionFilterAttribute
    {
     
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            filterContext.HttpContext.Response.Filter = new MailCSSStream(filterContext.HttpContext.Response.Filter);
            base.OnActionExecuting(filterContext);
        }
    }
}