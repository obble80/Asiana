using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Asiana.Catalog.Search.Solr.Client;
using Asiana.Catalog.Search.Solr.Client.Query;
using Asiana.Shopping.Services.Data;

namespace Asiana.Shopping.Services.Products
{
    public class ProductService : IProductService
    {
        private Fashinon dbContext;

        public ProductService(Fashinon dbContext)
        {
            this.dbContext = dbContext;
        }

        public IEnumerable<Product> GetProducts(IEnumerable<String> ids)
        {
            SolrClient client = new SolrClient();
            DocumentQuery query = new DocumentQuery();
            query.Ids.AddRange(ids);
            var documents = client.Execute(query);

            foreach (dynamic document in documents)
            {
                yield return new Product() { ProductID = document.ID, Image = document.Image, Name = document.Name, Price = decimal.Parse(document.Price) };
            }
        }
    }
}