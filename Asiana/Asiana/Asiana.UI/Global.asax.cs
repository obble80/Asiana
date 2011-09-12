using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Ninject.Web.Mvc;
using Ninject;
using System.Reflection;
using NLog;
using System.Diagnostics;
using System.Data.Entity;
using System.Data.EntityClient;
using Asiana.Shopping.Services.Data;
namespace Asiana.UI
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : NinjectHttpApplication
    {
        Logger logger = LogManager.GetLogger("MyClassName");
   
        protected void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        protected override IKernel CreateKernel()
        {
            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
            var kernel = new StandardKernel();
            kernel.Load(assemblies);
            return kernel;
        }


        protected void Application_Error(object sender, EventArgs e)
        {
            var exception = HttpContext.Current.Server.GetLastError().GetBaseException();
            Debug.WriteLine(exception.ToString());
            logger.Log(LogLevel.Fatal, HttpContext.Current.Server.GetLastError().GetBaseException());
          
            //var errorService = NinjectKernel.Get<IErrorLogService>();
            //errorService.LogError(HttpContext.Current.Server.GetLastError().GetBaseException(), "AppSite");
        }


        protected void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            // Image Resize
            routes.MapRoute("ImageResize",
                "Image/Size/{name}/{width}/{height}",
                new { controller = "Image", action = "Size" });

            // Image Resize
            routes.MapRoute("ImageZoom",
                "Image/Zoom/{name}/{level}/{image}",
                new { controller = "Image", action = "Zoom", level=UrlParameter.Optional, image=UrlParameter.Optional});

            // Basket Add
            routes.MapRoute("Basket",
                "Basket/{action}/{productId}/{quantity}/",
                new
                {
                    controller = "Basket",
                    action = "Add",
                    quantity = UrlParameter.Optional
                });

            routes.MapRoute(
              "Product", // Route name
              "Product/{id}", // URL with parameters
              new { controller = "Product", action = "Index", id = UrlParameter.Optional } // Parameter defaults
          );
            routes.MapRoute(
               "Browse", // Route name
               "Browse/{action}/{pageSize}/{page}/{*path}", // URL with parameters
               new { controller = "Browse", action = "Index", pageSize=10, page=1,path = UrlParameter.Optional } // Parameter defaults
           );
            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

        }

        protected override void OnApplicationStarted()
        {
            base.OnApplicationStarted();

            logger.Info("started");
            AreaRegistration.RegisterAllAreas();

            //Database.SetInitializer<Fashinon>(new DropCreateDatabaseIfModelChanges<Fashinon>());

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
        }
    }
}