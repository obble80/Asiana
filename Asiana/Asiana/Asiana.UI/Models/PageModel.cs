using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Asiana.Catalog.Search.Solr.Client;
using Asiana.Shopping.Services.Basket;
using System.Xml.Linq;
using Asiana.Profile.Services.Reviews;

namespace Asiana.UI.Models
{
    public class PageModel
    {
        public SolrResponse Response { get; set; }
        public XElement TopNavigation { get; set; }
        public Basket Basket { get; set; }
        public ProductReview Review { get; set; }
        public dynamic Product { get; set; }
    }
}