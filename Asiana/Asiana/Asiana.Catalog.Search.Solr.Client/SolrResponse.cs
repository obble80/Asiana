using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Xml.XPath;

namespace Asiana.Catalog.Search.Solr.Client
{
    public class SolrResponse
    {
        private XElement response;
        private XElement configuration;
        private Dictionary<String, String> queriesToFriendlyNames = new Dictionary<string, string>();
        private Dictionary<String, String> friendlyNamesToQueries = new Dictionary<string, string>();

        public SolrResponse(XElement response, XElement configuration)
        {
            this.response = response;
            this.configuration = configuration;

            Initialize();
        }

        private void Initialize()
        {
            if (configuration != null)
            {
                var facetQueries = configuration.XPathSelectElements("//facetQuery");

                foreach (var facetQuery in facetQueries)
                {
                    queriesToFriendlyNames.Add(facetQuery.Attribute("query").Value, facetQuery.Attribute("en-GB").Value);
                    friendlyNamesToQueries.Add(facetQuery.Attribute("en-GB").Value, facetQuery.Attribute("query").Value);
                }
            }
        }

        private IEnumerable<String> GetParamValues(XElement param)
        {
            if (param.HasElements)
            {
                foreach (var childParam in param.Elements())
                {
                    yield return childParam.Value;
                }
            } else {
                yield return param.Value;
            }
        }
        private IEnumerable<FieldValuePair> GetParamKeyValues(XElement param)
        {
            if (param.HasElements)
            {
                foreach (var childParam in param.Elements())
                {
                    var elements = childParam.Value.Split(':');
                    yield return new FieldValuePair() { Field = elements[0], Value = elements[1]};
                }
            }
            else
            {
                var elements = param.Value.Split(':');
                yield return new FieldValuePair() { Field = elements[0], Value = elements[1]};
            }
        }

        private SolrResponseHeader GetHeader() {
            List<FieldValuePair> facetQueries = new List<FieldValuePair>();
            List<string> queries = new List<string>();
            List<string> facets = new List<string>();
            List<FieldValuePair> selectedFacets = new List<FieldValuePair>();
            Int32 results = 0;
            Int32 pageSize = 10;
            Int32 offSet = 0;

            var numberFound = response.Element("result").Attribute("numFound").Value;
            results = Int32.Parse(numberFound);

           

            var header = response
                .Elements("lst")
                .Where(x => x.Attribute("name").Value == "responseHeader")
                .First();

            // Get Params
            var responseParams = header.Elements("lst")
                .Where(x => x.Attribute("name").Value == "params")
                .First()
                .Elements();

            // Iterate params
            foreach (var param in responseParams)
            {
                // get the name
                var name = param.Attribute("name").Value;
                switch (name)
                {
                    case "facet.query":
                        facetQueries.AddRange(GetParamKeyValues(param));
                        break;
                    case "facet.field":
                        facets.AddRange(GetParamValues(param));
                        break;
                    case "q":
                        queries.AddRange(GetParamValues(param));
                        break;
                    case "fq":
                        selectedFacets.AddRange(GetParamKeyValues(param));
                        break;
                    case "start":
                        offSet = int.Parse(param.Value);
                        break; //TODO: This is page number
                    case "rows":
                        pageSize = int.Parse(param.Value);
                        break; //TODO: This is page size
                    default:
                        throw new ArgumentOutOfRangeException("Not expecting " + name + " for response parameter");
                } 

            }

            var pages = (results / pageSize) + 1;
            var page = (offSet / pageSize) + 1;

            var solrHeader = new SolrResponseHeader()
            {
                FacetQueries =facetQueries,
                Facets = facets,
                Queries = queries,
                SelectedFacets = selectedFacets,
                Results = results,
                Pages = pages,
                PageSize = pageSize,
                Page = page
            };

            return solrHeader;
        }
        private IEnumerable<SolrFacet> GetAllFacetTypes()
        {
            List<SolrFacet> facets = new List<SolrFacet>();

            var allFacetTypes = response.Elements("lst")
                .Where(x => x.Attribute("name").Value == "facet_counts")
                .Elements();

            foreach (var facetType in allFacetTypes)
            {
                var name = facetType.Attribute("name").Value;

                switch (name)
                {
                    case "facet_fields": // Simple facets
                        facets.AddRange(GetFacets(facetType));
                        break;
                    case "facet_queries": // Query facets
                        facets.AddRange(GetQueryFacets(facetType));
                        break;
                }
            }

            return facets;
        }

        public IEnumerable<SolrFacet> GetFacets(XElement facetType)
        {
            var facets = facetType.Elements("lst");

            foreach (var facet in facets)
            {
                var solrFacet = new SolrFacet() { Response = this };
                var name = facet.Attribute("name").Value;
                var values = facet
                    .Elements()
                    .Select(x => new SolrFacetValue() {
                        Name = x.Attribute("name").Value, Count=x.Value, Facet = solrFacet 
                    });

                solrFacet.Name = name;
                solrFacet.Values = values;

                yield return solrFacet;
            }
        }

        public IEnumerable<SolrFacet> GetQueryFacets(XElement facetType)
        {
            Dictionary<string, List<KeyValuePair<String, String>>> facets = new Dictionary<string, List<KeyValuePair<string,string>>>();

            var elements = facetType.Elements();

            foreach (var element in elements)
            {
                var nameValue = element
                    .Attribute("name")
                    .Value
                    .Split(':');

                var name = nameValue[0];
                var value = nameValue[1];
                var xpath = string.Format("//facet[@name='{0}']/facetQueries/facetQuery[@query='{1}']", name, value);
                value = configuration.XPathSelectElement(xpath).Attribute("en-GB").Value;

                if (facets.ContainsKey(name))
                {
                    facets[name].Add(new KeyValuePair<String,String>(value, element.Value));
                }
                else
                {
                    facets.Add(name, new List<KeyValuePair<String,String>>());
                    facets[name].Add(new KeyValuePair<String,String>(value, element.Value));
                }
            }

            foreach (var key in facets.Keys)
            {
                var facet = new SolrFacet() { Response = this };
                var values = facets[key].Select(x => new SolrFacetValue() { Name = x.Key, Count = x.Value, Facet = facet });
                facet.Name = key;
                facet.Values = values;

                yield return facet;
            }

        }
            

        public SolrResponseHeader Header
        {
            get
            {
                return GetHeader();
            }
        }

        public IEnumerable<SolrDocument> Documents
        {
            get
            {
                return response.Element("result")
                    .Elements("doc")
                    .Select(x => new SolrDocument(x));
            }
        }

        public IEnumerable<SolrFacet> Facets
        {
            get
            {
                return GetAllFacetTypes();
            }
        }
        public IEnumerable<FieldValuePair> BreadCrumb
        {
            get
            {
                foreach (var query in this.Header.Queries)
                {
                    if (query != "*:*")
                    {
                        yield return new FieldValuePair() { Field = "Search", Value = query };
                    }
                }

                foreach (var item in this.Header.SelectedFacets)
                {
                    if (queriesToFriendlyNames.ContainsKey(item.Value))
                    {
                        item.Value = queriesToFriendlyNames[item.Value];
                    }
                    yield return item;
                }
            }
        }
    }
}
