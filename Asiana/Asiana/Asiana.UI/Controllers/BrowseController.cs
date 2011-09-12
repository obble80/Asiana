using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//using Endeca.Web.UI;
using Asiana.UI.Extensions;
using Asiana.Catalog.Search.Solr.Client;
using Asiana.Catalog.Search.Solr.Client.Query;
using Asiana.Shopping.Services.Basket;
using Asiana.UI.Models;
using System.Xml.Linq;
using Asiana.UI.Filters;
using System.Text;

namespace Asiana.UI.Controllers
{
    public class BrowseController : Controller
    {
        //
        // GET: /Browse/
        private IBasketService basketService;
        private ISolrSearchService searchService;

        public BrowseController(IBasketService basketService, ISolrSearchService searchService) {
            this.basketService = basketService;
            this.searchService = searchService;
        }

        [AcceptVerbs(HttpVerbs.Get)]
        //[CompressFilter()]
        public ActionResult Index(int pageSize, int page, string path)
        {
            var query = SolrNavigation.GetQueryFromSeoPath(path);
            query.PageSize = pageSize;
            query.Page = page - 1;

            var response = searchService
                .Execute(query);

            StringBuilder title = new StringBuilder();
            foreach(var item in response.BreadCrumb) {
                title.AppendFormat("{0} ",item.Value);
            }

            title.Append("| ");
            title.Append("Products at Fashinon");
            ViewBag.Title = title.ToString();

            Basket basket = basketService.GetBasket();

            XElement navigation = XElement.Load(Server.MapPath(@"~\MetaData\Navigation.xml"));

            var model = new PageModel() { Basket = basket, Response = response, TopNavigation=navigation};

            return View(model);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [ActionName("Index")]
        //[CompressFilter()]
        public ActionResult IndexPost(string searchTerm)
        {
            var query = new SolrQuery() { SearchTerm = searchTerm };

            var response = searchService
                .Execute(query);

            Basket basket = basketService.GetBasket();

            XElement navigation = XElement.Load(Server.MapPath(@"~\MetaData\Navigation.xml"));

            var model = new PageModel() { Basket = basket, Response = response, TopNavigation = navigation };

            return View(model);
        }
        
        //[AcceptVerbs(HttpVerbs.Post)]
        //[CompressFilter]
        //public ActionResult NavigationList(string path)
        //{
        //    var response = new SolrClient(Server.MapPath(@"~\Metadata\Solr\Asiana.xml")).Execute(null);

        //    return PartialView("NavigationList", response);
        //}

        [AcceptVerbs(HttpVerbs.Post)]
        //[CompressFilter]
        public ActionResult TopNavigation(string path)
        {
            var query = SolrNavigation.GetQueryFromSeoPath(path);

            var response = searchService
                .Execute(query);

            return PartialView("Navigation/CategoryList", response.Facets.Where(x => x.Name == "Category").FirstOrDefault());
        }
    }
}
