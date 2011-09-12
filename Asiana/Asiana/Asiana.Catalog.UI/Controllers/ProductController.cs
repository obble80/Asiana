using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Asiana.Catalog.Search.Solr.Client.Query;
using Asiana.Catalog.Search.Solr.Client;
using Asiana.Catalog.Schema.Types;
using System.Globalization;

namespace Asiana.Catalog.UI.Controllers
{
    public class ProductController : Controller
    {
        private ISolrSearchService searchService;
        private ISchemaService schemaService;


        public ProductController(
            ISolrSearchService searchService,
            ISchemaService schemaService)
        {
            this.searchService = searchService;
            this.schemaService = schemaService;
        }
        //
        // GET: /Product/

        public ActionResult Index()
        {
            var query = new SolrQuery();

            var response = searchService
                .Execute(query);

            return View(response);
        }

        // Returns a view for creating a new product
        public ActionResult New()
        {
            var types = schemaService.GetProductTypes();
            return PartialView(types);
        }

        public ActionResult Create(FormCollection form)
        {
            // Get the product type
            var productTypeName = form["productType"];
            var productType = schemaService.GetProductType(productTypeName);

            var productSku = form["sku"];
            //var product = productService.GetProduct(productSku);

            var product = new Product();

            CultureInfo culture = Session["Locale"] as CultureInfo;
            if(culture == null) {
                culture = System.Threading.Thread.CurrentThread.CurrentUICulture;
            }

            foreach (var attribute in productType.Attributes)
            {
                var attributeName = attribute.GetLocalisedName(culture);
                // Get the value from the form
                product[attributeName] = form[attributeName];
            }

            schemaService.SaveProduct(product);
            return new EmptyResult();
        }

        public ActionResult Edit(string productType, string id)
        {
            Product product = null;


            if (!String.IsNullOrWhiteSpace(productType))
            {
                product = new Product();
                product.Schema = schemaService.GetProductType(productType);


                product.EditorCulture = Session["Locale"] as CultureInfo;

                if (product.EditorCulture == null)
                {
                    product.EditorCulture = System.Threading.Thread.CurrentThread.CurrentUICulture;
                }

                return View(product);
            }
            
            if(!String.IsNullOrWhiteSpace(id))
            {
                product = schemaService.GetProduct(id);

                product.EditorCulture = Session["Locale"] as CultureInfo;

                if (product.EditorCulture == null)
                {
                    product.EditorCulture = System.Threading.Thread.CurrentThread.CurrentUICulture;
                }

                return View(product);
            }


            return RedirectToAction("Index");
        }

        public ActionResult Search()
        {
            var query = new SolrQuery();

            var response = searchService
                .Execute(query);

            return View(response);
        }
    }
}
