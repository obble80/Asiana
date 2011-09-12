using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Asiana.UI.Extensions
{
    public static class UrlExtensions
    {
        public static string RemoteUrl(
            this UrlHelper urlHelper, 
            string url, 
            bool forceUnsecure) {
            HttpContextBase context = urlHelper.RequestContext.HttpContext;
            HttpRequestBase request = context.Request;
            return request.Url.Scheme 
                + System.Uri.SchemeDelimiter 
                + request.Url.Host 
                + (request.Url.IsDefaultPort ? "" : ":" + request.Url.Port)
                + url;
        }
    }
}