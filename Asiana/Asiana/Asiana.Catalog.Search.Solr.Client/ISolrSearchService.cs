using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Asiana.Catalog.Search.Solr.Client.Query;

namespace Asiana.Catalog.Search.Solr.Client
{
    public interface ISolrSearchService
    {
        SolrResponse Execute(SolrQuery query);
        IEnumerable<SolrDocument> Execute(DocumentQuery query);
    }
}
