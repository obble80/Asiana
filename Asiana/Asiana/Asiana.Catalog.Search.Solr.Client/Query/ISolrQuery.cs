using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Asiana.Catalog.Search.Solr.Client.Query
{
    public interface ISolrQuery
    {
        string ToSolrQuery();
    }
}
