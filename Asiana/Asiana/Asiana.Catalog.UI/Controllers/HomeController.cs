using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Drawing;

namespace Asiana.Catalog.UI.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Welcome to ASP.NET MVC!";
            ViewBag.Colours = new List<KnownColor>() { KnownColor.Red, KnownColor.Blue, KnownColor.Green, KnownColor.Pink, KnownColor.Orange, KnownColor.Orchid, KnownColor.White, KnownColor.Black };
            return View();
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
