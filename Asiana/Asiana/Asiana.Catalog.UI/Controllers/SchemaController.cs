using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Asiana.Catalog.Schema.Types;
using Asiana.Catalog.UI.Models;

namespace Asiana.Catalog.UI.Controllers
{
    public class SchemaController : Controller
    {
        private ISchemaService schemaService;

        public SchemaController(ISchemaService schemaService)
        {
            this.schemaService = schemaService;
        }

        [HttpGet]
        public ActionResult NewAttribute()
        {
            var attribute = new AttributeDefinition();
            var attributeModel = new NewAttributeModel() { 
                Attribute = attribute, 
                BaseTypes = schemaService.GetBaseTypes(), 
                Editors = schemaService.GetEditors(), 
                Renderers = schemaService.GetRenderers() };

            return PartialView("Attributes/Edit", attributeModel);
        }

        [HttpPost]
        public ActionResult NewAttribute(AttributeDefinition attribute)
        {
            schemaService.SaveAttribute(attribute);
            var attributes = schemaService.GetAttributes();
            return PartialView("Attributes/Index", attributes);
        }

        public ActionResult Attributes()
        {
            var attributes = schemaService.GetAttributes();
            return View("Attributes/Index", attributes);
        }

        public ActionResult EditAttribute(string name)
        {
            var attribute = schemaService.GetAttribute(name);
            var attributeModel = new NewAttributeModel()
            {
                Attribute = attribute,
                BaseTypes = schemaService.GetBaseTypes(),
                Editors = schemaService.GetEditors(),
                Renderers = schemaService.GetRenderers()
            };
            return PartialView("Attributes/Edit", attributeModel);
        }

        [HttpGet]
        public ActionResult NewType()
        {
            var type = new ProductType();
            var attributes = schemaService.GetAttributes();
            var model = new NewProductTypeModel() { ProductType = type, Attributes = attributes };
            return PartialView("Types/Edit", model);
        }

        [HttpPost]
        public ActionResult NewType(String[] attributes, ProductType productType)
        {
            var attributeDefs = schemaService.GetAttributes(attributes);
            productType.Attributes.AddRange(attributeDefs);
            schemaService.SaveProductType(productType);
            var productTypes = schemaService.GetProductTypes();

            return PartialView("Types/Index", productTypes);
        }

        public ActionResult Types()
        {
            var productTypes = schemaService.GetProductTypes();
            return PartialView("Types/Index", productTypes);
        }

        public ActionResult EditType(string name)
        {
            var productType = schemaService.GetProductType(name);
            var attributes = schemaService.GetAttributes();

            var model = new NewProductTypeModel() { Attributes = attributes, ProductType = productType };
            return PartialView("Types/Edit", model);
        }

        public ActionResult Index()
        {
            return View();
        }
    }
}
