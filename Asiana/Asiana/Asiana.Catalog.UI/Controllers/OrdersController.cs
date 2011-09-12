using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Asiana.Shopping.Services.Orders;
using Asiana.Shopping.Services.Data;
using Asiana.Shopping.Payments.Paypal;

namespace Asiana.Catalog.UI.Controllers
{ 
    public class OrdersController : Controller
    {
        private Fashinon db = new Fashinon();
        private IPayPalService payPalService;

        public OrdersController(IPayPalService payPalService)
        {
            this.payPalService = payPalService;
        }
        //
        // GET: /Orders/

        public ActionResult Index()
        {
            var queryDate = (DateTime.Now - new TimeSpan(7,0,0,0));
            var orders = db.Orders
                .Include(o => o.Customer)
                .Include(o => o.ShippingAddress)
                .Include(o=> o.ShippingMethod)
                .Include(o=> o.Payment)
                .Where(x => x.Created >= queryDate)
                .GroupBy(x => x.Status);
            if (Request.IsAjaxRequest())
            {
                return PartialView(orders.ToList());
            }
            else
            {
                return View(orders.ToList());
            }
        }

        //
        // GET: /Orders/Details/5

        public ViewResult Details(long id)
        {
            Order order = db.Orders.Find(id);
            return View(order);
        }

        //
        // GET: /Orders/Create

        public ActionResult Create()
        {
            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "Title");
            return View();
        } 

        //
        // POST: /Orders/Create

        [HttpPost]
        public ActionResult Create(Order order)
        {
            if (ModelState.IsValid)
            {
                db.Orders.Add(order);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "Title", order.CustomerID);
            return View(order);
        }

        //
        // GET: /Orders/Edit/5

        public ActionResult Refund(long id)
        {
            Order order = db.Orders.Find(id);

            if (order.Payment is PayPalPayment)
            {
                var payment = order.Payment as PayPalPayment;
                payPalService.RefundPayment(payment);
                order.Status = "Refunded";
                db.SaveChanges();
            }

            return View(order);
        }

        //
        // GET: /Orders/Edit/5
 
        public ActionResult Edit(long id)
        {
            Order order = db.Orders.Find(id);
            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "Title", order.CustomerID);
            return View(order);
        }

        //
        // POST: /Orders/Edit/5

        [HttpPost]
        public ActionResult Edit(Order order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "Title", order.CustomerID);
            return View(order);
        }

        //
        // GET: /Orders/Delete/5
 
        public ActionResult Delete(long id)
        {
            Order order = db.Orders.Find(id);
            return View(order);
        }

        //
        // POST: /Orders/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(long id)
        {            
            Order order = db.Orders.Find(id);
            db.Orders.Remove(order);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}