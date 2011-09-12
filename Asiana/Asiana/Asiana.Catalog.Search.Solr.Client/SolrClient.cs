using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Xml.Linq;
using System.Xml.XPath;
using Asiana.Catalog.Search.Solr.Client.Query;
using System.IO;
using Asiana.Catalog.Schema.Types;

namespace Asiana.Catalog.Search.Solr.Client
{
    public class SolrClient : ISolrSearchService
    {
        XElement configuration;
        ISchemaService schemaService;

        public SolrClient(string facetConfiguration,ISchemaService schemaService)
        {
            if (File.Exists(facetConfiguration))
            {
                this.configuration = XElement.Load(facetConfiguration);
            }

            this.schemaService = schemaService;
        }

        public SolrClient()
        {
        }

        private SolrQuery UpdateQuery(SolrQuery query)
        {
            foreach (var facetQuery in query.FacetQueries)
            {
                // Try and find the facet in the configuration
                var facetConfiguration = configuration.XPathSelectElement(String.Format("//facet[@name='{0}']", facetQuery.Field));
                if (facetConfiguration != null)
                {
                    // Try and find the value represented in the query
                    var targetFacetQuery = facetConfiguration.XPathSelectElement(String.Format(".//facetQuery[@en-GB='{0}']", facetQuery.Value));
                    if (targetFacetQuery != null)
                    {
                        // Get the actual query to be used against solr
                        var targetQuery = targetFacetQuery.Attribute("query").Value;
                        facetQuery.Value = targetQuery;
                    }
                }
            }

            return query;
        }

        public IEnumerable<SolrDocument> Execute(DocumentQuery query)
        {
            using (var client = new WebClient())
            {
                var response = client.DownloadString("http://localhost:8983/solr/browse?" + query.ToSolrQuery());
                var responseElement = XElement.Parse(response);


                return responseElement.Element("result")
                    .Elements("doc")
                    .Select(x => new SolrDocument(x));
            }
        }

        public SolrResponse Execute(SolrQuery query)
        {
            String queryExtensions = null;
            SolrResponse solrResponse = null;

            query = UpdateQuery(query);

            if (configuration != null)
            {
                // Get facets
                var facets = configuration.Element("facets").Elements();

                foreach (var facet in facets)
                {
                    // Get queries if any
                    var type = facet.Attribute("type").Value;
                    var name = facet.Attribute("name").Value;

                    switch (type)
                    {
                        case "Query":
                            var queries = facet.Elements("facetQueries").Elements("facetQuery").Attributes("query");
                            foreach (var facetQuery in queries)
                            {
                                queryExtensions = queryExtensions + "&facet.query=" + facet.Attribute("name").Value + ":" + facetQuery.Value;
                            }
                            break;
                        case "Field":
                            queryExtensions = queryExtensions + "&facet.field=" + name;
                            break;
                    }

                }
            }

            using (var client = new WebClient())
            {
                var response = client.DownloadString("http://localhost:8983/solr/browse?" + query.ToSolrQuery() + queryExtensions);
                var responseElement = XElement.Parse(response);

                solrResponse = new SolrResponse(responseElement, configuration);
            }

            var header = solrResponse.Header;
            return solrResponse;
        }
    }
}
