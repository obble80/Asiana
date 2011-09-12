using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Asiana.Catalog.Search.Solr.Client
{
    public class SolrFacetValue
    {
        public string Name { get; set; }
        public string Count { get; set; }
        public SolrFacet Facet { get; set; }
    }
}
