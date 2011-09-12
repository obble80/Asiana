using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Asiana.Catalog.Search.Solr.Client
{
    public class SolrResponseHeader
    {
        public IEnumerable<String> Facets { get; set; }
        public IEnumerable<FieldValuePair> FacetQueries { get; set; }
        public IEnumerable<String> Queries { get; set; }
        public IEnumerable<FieldValuePair> SelectedFacets { get; set; }
        public Int32 Results { get; set; }
        public Int32 Pages { get; set; }
        public Int32 PageSize { get; set; }
        public Int32 Page { get; set; }
    }
}
