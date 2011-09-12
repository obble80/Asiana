using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Asiana.Shopping.Services.Orders;
using Asiana.Shopping.Services.Data;

namespace Asiana.UI.Controllers
{ 
    public class TestController : Controller
    {
        private Fashinon db = new Fashinon();

        //
        // GET: /Test/

        public ViewResult Index()
        {
            return View(db.Orders.ToList());
        }

        //
        // GET: /Test/Details/5

        public ViewResult Details(string id)
        {
            Order order = db.Orders.Find(id);
            return View(order);
        }

        //
        // GET: /Test/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Test/Create

        [HttpPost]
        public ActionResult Create(Order order)
        {
            if (ModelState.IsValid)
            {
                db.Orders.Add(order);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(order);
        }
        
        //
        // GET: /Test/Edit/5
 
        public ActionResult Edit(string id)
        {
            Order order = db.Orders.Find(id);
            return View(order);
        }

        //
        // POST: /Test/Edit/5

        [HttpPost]
        public ActionResult Edit(Order order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(order);
        }

        //
        // GET: /Test/Delete/5
 
        public ActionResult Delete(string id)
        {
            Order order = db.Orders.Find(id);
            return View(order);
        }

        //
        // POST: /Test/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(string id)
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