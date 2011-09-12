using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Asiana.Catalog.Schema.Types;

namespace Asiana.Catalog.UI.Models
{
    public class NewAttributeModel
    {
        public AttributeDefinition Attribute {get;set;}
        public IEnumerable<Type> BaseTypes { get; set; }
        public IEnumerable<String> Renderers { get; set; }
        public IEnumerable<String> Editors { get; set; }
    }

    public class NewProductTypeModel
    {
        public IEnumerable<AttributeDefinition> Attributes { get; set; }
        public ProductType ProductType { get; set; }
    }
}