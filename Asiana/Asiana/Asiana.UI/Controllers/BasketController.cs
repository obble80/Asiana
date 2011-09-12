using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Asiana.Shopping.Services.Products;
using Asiana.Shopping.Services.Basket;
using Asiana.UI.Models;
using System.Xml.Linq;

namespace Asiana.UI.Controllers
{
    public class BasketController : Controller
    {
        private IBasketService basketService;
        private IProductService productService;

        public BasketController(IBasketService basketService, IProductService productService)
        {
            this.basketService = basketService;
            this.productService = productService;
        }
        //
        // GET: /Basket/

        public ActionResult Index()
        {
            Basket basket = basketService.GetBasket();

            XElement navigation = XElement.Load(Server.MapPath(@"~\MetaData\Navigation.xml"));

            var model = new PageModel() { Basket = basket, Response = null, TopNavigation = navigation };

            return View(model);
        }

        [HttpPost]
        public ActionResult Add(string productId, int quantity)
        {
            
            Basket basket = basketService.GetBasket();
            List<String> ids = new List<string>();
            ids.Add(productId);

            var products = productService.GetProducts(ids);

            foreach (var product in products)
            {
                basket.AddToBasket(product, quantity);
            }
           
                return PartialView("Basket/MiniBasket", basket);
           
        }

        [HttpPost]
        public ActionResult Remove(string productId)
        {
            
            Basket basket = basketService.GetBasket();

            basket.Items.RemoveAll(x => x.ProductID == productId);
            return PartialView("Basket/MiniBasket", basket);
        }

        [HttpGet]
        [ActionName("Remove")]
        public ActionResult GetRemove(string productId)
        {

            Basket basket = basketService.GetBasket();
            basket.Items.RemoveAll(x => x.ProductID == productId);

            return RedirectToAction("Index");
        }

        public ActionResult Update(IEnumerable<Product> products)
        {
            Basket basket = basketService.GetBasket();

            foreach (var product in products)
            {
                basket.Items.ForEach(x =>
                {
                    if (x.ProductID == product.ProductID)
                    {
                        x.Quantity = product.Quantity;
                    }
                });
            }

            return RedirectToAction("Index");
        }

    }
}
