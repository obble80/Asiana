using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Asiana.Catalog.Search.Solr.Client
{
    public class SolrFacet
    {
        public string Name { get; set; } 
        public SolrResponse Response { get; set;}
        public virtual IEnumerable<SolrFacetValue> Values { get; set; }
    }
}
