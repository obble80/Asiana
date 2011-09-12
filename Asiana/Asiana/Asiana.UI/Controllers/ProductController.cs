using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//using Endeca.Web.UI;
using Asiana.Catalog.Search.Solr.Client;
using Asiana.Catalog.Search.Solr.Client.Query;
using Asiana.UI.Services;
using System.IO;
using System.Xml.Linq;
using Asiana.Shopping.Services.Basket;
using Asiana.UI.Models;
using Asiana.Profile.Services.Reviews;

namespace Asiana.UI.Controllers
{
    public class ProductController : Controller
    {
        IDeepZoomService deepZoomService;
        ISolrSearchService searchService;
        IBasketService basketService;
        IReviewService reviewService;

        public ProductController(
            IDeepZoomService deepZoomService, 
            ISolrSearchService searchService,
            IBasketService basketService,
            IReviewService reviewService)
        {
            this.deepZoomService = deepZoomService;
            this.searchService = searchService;
            this.basketService = basketService;
            this.reviewService = reviewService;
        }

        //
        // GET: /Product/

        public ActionResult Index(string id)
        {
            
            Server.MapPath(@"~\Metadata\Solr\Asiana.xml");

            var response = searchService
                .Execute(new SolrQuery() { ID = id });

            dynamic document = response.Documents.FirstOrDefault();
            document.DeepZoomControlImages = Url.Content("~/Content/themes/base/seadragon");

            Basket basket = basketService.GetBasket();
            ProductReview productReview = reviewService.GetProductReview(id);

            XElement navigation = XElement.Load(Server.MapPath(@"~\MetaData\Navigation.xml"));

            var model = new PageModel() { Basket = basket, Product = document, TopNavigation = navigation, Review = productReview };

            if (Request.IsAjaxRequest())
            {
                return PartialView("Ajax/Index", model);
            }
            else
            {
                return View("Index", model);
            }
        }

    }
}
