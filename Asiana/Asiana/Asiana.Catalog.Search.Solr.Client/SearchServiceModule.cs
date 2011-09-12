using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ninject.Modules;
using System.Configuration;
namespace Asiana.Catalog.Search.Solr.Client
{
    public class SearchServiceModule:NinjectModule
    {
        public override void Load()
        {
            Bind<ISolrSearchService>()
                .To<SolrClient>()
                .WithConstructorArgument("facetConfiguration", 
                    ConfigurationManager.AppSettings["searchMetadata"]);
        }
    }
}
