using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Asiana.Site.Services
{
    public interface ISiteService
    {
        String GetCurrrentDomain();
        String Theme { get; set; }
        SiteMode Mode { get; set; }

        void PutOffline();
        void PutOnline();
    }
}
