using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Asiana.Site.Services
{
    public class SiteService : ISiteService
    {
      
        public String GetCurrrentDomain()
        {
            var request = HttpContext.Current.Request;
            return request.Url.Scheme + System.Uri.SchemeDelimiter + request.Url.Host + (request.Url.IsDefaultPort ? "" : ":" + request.Url.Port);
        }
        public String Theme { get; set; }
        public SiteMode Mode { get; set; }

        public void PutOffline()
        {
        }

        public void PutOnline()
        {
        }


        
    }
}
