using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Dynamic;
using Norm.BSON;
using Norm.BSON.DbTypes;
using System.Globalization;
using System.ComponentModel;

namespace Asiana.Catalog.Schema.Types
{
    public class Product : DynamicObject, IExpando
    {
        public ProductType Schema { get; set; }
        public CultureInfo EditorCulture { get; set; }

        [DisplayName("sku")]
        public String ID {
            get { return this["_id"] as string; }
            set{ this["_id"] = value;}
        }

        public String ExternalReference { get; set; }
        public String Title { get; set; }

        private Dictionary<String, Object> properties = new Dictionary<string, object>();

        #region IExpando Members

        public IEnumerable<ExpandoProperty> AllProperties()
        {
            return properties.Select(x => new ExpandoProperty(x.Key, x.Value));
        }

        public string GetName(string name)
        {
            // Get attribute type from schema
            var schemaAttribute = Schema.Attributes.First(x => x.Name == name);
            // name_locale_baseType_solrType
            var specificName = String.Format("{0}_{1}_{2}_{3}", name, EditorCulture.Name, schemaAttribute.BaseType, schemaAttribute.SolrType);

            return specificName;
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result){

            var name = GetName(binder.Name);
           
            if (properties.ContainsKey(name))
            {
                result = properties[name];
                return true;
            }

            return base.TryGetMember(binder, out result);
        }

        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            var name = GetName(binder.Name);
            properties[name] = value;
            return true;
        }

        public void Delete(string propertyName)
        {
            var name = GetName(propertyName);
            properties.Remove(name);
        }

        public object this[string propertyName]
        {
            get
            {
                object value = null;
                properties.TryGetValue(propertyName, out value);

                return value;
            }
            set
            {
                properties[propertyName] = value;
            }
        }

        #endregion
    }
}
