using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Asiana.Catalog.Search.Solr.Client.Query
{
    public class SolrQuery
    {
        private List<FieldValuePair> facetQueries = new List<FieldValuePair>();
        private int page = 0;
        private int pageSize = 10;

        public int Page
        {
            get { return page; }
            set { page = value; }
        }

        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = value; }
        }

        public string SearchTerm { get; set; }
        public string ID { get; set; }
        

        public List<FieldValuePair> FacetQueries
        {
            get { return facetQueries; }
            set { facetQueries = value; }
        }

        public string ToSolrQuery()
        {
            StringBuilder query = new StringBuilder();

            query.Append("&q=");

            if (String.IsNullOrEmpty(this.SearchTerm) && String.IsNullOrEmpty(this.ID))
            {
                query.Append("*:*");
            }
            else if (!String.IsNullOrEmpty(this.SearchTerm) && String.IsNullOrEmpty(this.ID))
            {
                query.Append(this.SearchTerm);
            }
            else
            {
                query.Append("ID");
                query.Append(":");
                query.Append(this.ID);
            }

            foreach (var facetQuery in facetQueries)
            {
                query.Append("&fq=");
                query.Append(facetQuery.Field);
                query.Append(":");
                query.Append(facetQuery.Value);
            }

            query.AppendFormat("&rows={0}", this.PageSize);

            query.Append("&start=");
            query.Append(this.Page * this.PageSize);

            return query.ToString();
        }

    }
}
