using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Asiana.Profile.Services.Reviews;
using Asiana.UI.Models;
using System.Dynamic;

namespace Asiana.UI.Controllers
{
    public class ReviewController : Controller
    {

        private IReviewService reviewService;

        public ReviewController(IReviewService reviewService)
        {
            this.reviewService = reviewService;
        }
        //
        // GET: /Review/

        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Review/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Review/Create


        public ActionResult Create(string productId)
        {
            ViewBag.ProductID = productId;

            return PartialView("Ajax/Create");
        } 

        //
        // POST: /Review/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
               var productReview = reviewService.GetProductReview(collection["productId"]);
               if(productReview == null) {
                   productReview = new ProductReview();
                   productReview.ProductID = collection["productId"];
               }

               var review = new Review();

               review.FullComment = collection["comment"];
               review.DesignRating = double.Parse(collection["designRating"]);
               review.ValueRating = double.Parse(collection["valueRating"]);
               review.QualityRating = double.Parse(collection["qualityRating"]);
               review.UserNickname = "Anonymous";

               productReview.Reviews.Add(review);
     
               dynamic product = new ExpandoObject();
                product.ID = collection["productId"];
                var model = new PageModel() { Review = productReview, Product = product };
               reviewService.Save(productReview);

               return PartialView("Ajax/Index", model);
            }            
            catch
            {
                return View();
            }
        }
        
        //
        // GET: /Review/Edit/5
 
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Review/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Review/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Review/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
