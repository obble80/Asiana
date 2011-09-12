using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Asiana.Catalog.Search.Solr.Client;
using Asiana.Catalog.Search.Solr.Client.Query;

namespace Asiana.UI.Extensions
{
    public class SolrNavigation
    {
        private static string SafeParameter(String parameter)
        {
       
            //return HttpUtility.UrlEncode(parameter);
            return parameter;
        }

        private static string UnsafeParameter(String parameter)
        {
            //return HttpUtility.UrlDecode(parameter);
            return parameter;
        }

        public static string BuildNavigationLink(SolrResponse response)
        {
            String url = String.Empty;

         
                foreach (var item in response.BreadCrumb)
                {
                    url = url + "/" + SafeParameter(item.Field) + "/" + SafeParameter(item.Value);
                }

                return url;
        }

        public static string BuildNavigationLink(SolrResponse response, SolrFacetValue value)
        {
            String url = String.Empty;
            

            if (response == null)
            {
                return url + value.Facet.Name + "/" + value.Name;
            }
            else
            {
                foreach (var item in response.BreadCrumb)
                {
                    url = url + "/" + SafeParameter(item.Field) + "/" + SafeParameter(item.Value);
                }

                return url + "/" + SafeParameter(value.Facet.Name) + "/" + SafeParameter(value.Name);
            }

          
        }

        public static string BuildBreadCrumbLink(SolrResponse response, FieldValuePair value)
        {
            String url = String.Empty;

            foreach (var item in response.BreadCrumb)
            {
                if (item.Equals(value))
                {
                    url = url + "/" + SafeParameter(item.Field) + "/" + SafeParameter(item.Value);
                    break;
                }
                else
                {
                    url = url + "/" + SafeParameter(item.Field) + "/" + SafeParameter(item.Value);
                } 
                
            }

            return url;
        }



        public static SolrQuery GetQueryFromSeoPath(string path)
        {
            var navigationQuery = new SolrQuery();

            
            if (path != null)
            {
                path = path.TrimEnd('/');

                var pathElements = path.Split('/');
                if (pathElements.Length % 2 == 0)
                {
                    for (int i = 1; i < pathElements.Length; i = i + 2)
                    {
                        var name = pathElements[i - 1];
                        var value = pathElements[i];

                        switch (name)
                        {
                            case "Search":
                                navigationQuery.SearchTerm = UnsafeParameter(value);
                                break;
                            //case "Page":
                            //    navigationQuery.Page = int.Parse(value) - 1;
                            //    break;
                            //case "PageSize":
                            //    navigationQuery.PageSize = int.Parse(value);
                            //    break;
                            default:
                                navigationQuery.FacetQueries.Add(new FieldValuePair() { Field = UnsafeParameter(name), Value = UnsafeParameter(value) });
                                break;

                        }
                    }
                }
                else
                {
                    throw new Exception("Partial Query");
                }
            }

            return navigationQuery;
        }
    }
}