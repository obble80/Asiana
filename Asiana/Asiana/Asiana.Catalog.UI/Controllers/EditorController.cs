
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Asiana.Catalog.UI.Models;
using Asiana.Catalog.Schema.Types;
using System.Globalization;

namespace Asiana.Catalog.UI.Controllers
{
    public class EditorController : Controller
    {
        private ISchemaService schemaService;

        public EditorController(ISchemaService schemaService)
        {
            this.schemaService = schemaService;
        }

        //
        // GET: /Editor/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult UpdateSchema()
        {
            return View("Index");
        }

        public ActionResult Locales()
        {
            var currentCulture = Session["Locale"];
            if (currentCulture == null)
            {
                currentCulture = System.Threading.Thread.CurrentThread.CurrentUICulture;
            }
            ViewBag.CurrentCulture = currentCulture;
            var cultures = CultureInfo.GetCultures(CultureTypes.SpecificCultures);
            return PartialView(cultures);

        }

        public ActionResult SetLocale(string locale)
        {
            var culture = CultureInfo.GetCultureInfo(locale);
            Session.Add("locale", culture);
            return new EmptyResult();
        }

        public ActionResult NewAttribute()
        {
            var attributeOptions = new NewAttributeModel();

            attributeOptions.BaseTypes = schemaService.GetBaseTypes();
            attributeOptions.Editors = schemaService.GetEditors();
            attributeOptions.Renderers = schemaService.GetRenderers();
            
            if (Request.IsAjaxRequest())
            {
                return PartialView(attributeOptions);
            }
            else
            {
                return View(attributeOptions);
            }
        }

        public ActionResult SaveAttribute(AttributeDefinition attribute)
        {
            return new EmptyResult();
        }
    }
}
