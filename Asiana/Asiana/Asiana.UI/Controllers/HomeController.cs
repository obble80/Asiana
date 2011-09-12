using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Asiana.UI.Extensions;
using Asiana.Catalog.Search.Solr.Client;
using Asiana.Catalog.Search.Solr.Client.Query;
using Asiana.UI.Models;
using Asiana.Shopping.Services.Basket;
using System.Xml.Linq;
//using Endeca.Web.UI;

namespace Asiana.UI.Controllers
{
    public class HomeController : Controller
    {
        private IBasketService basketService;
        private ISolrSearchService searchService;

        public HomeController(IBasketService basketService, ISolrSearchService searchService)
        {
            this.basketService = basketService;
            this.searchService = searchService;
        }

        public ActionResult Index()
        {
            
            var response = searchService
                .Execute(new SolrQuery());

            Basket basket = basketService.GetBasket();

            ViewBag.Title = "Fashinon - Fair Trade Hand Made Fashion for Everyone";

            XElement navigation = XElement.Load(Server.MapPath(@"~\MetaData\Navigation.xml"));

            var model = new PageModel() { Basket = basket, Response = response, TopNavigation = navigation };

            return View(model);
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
