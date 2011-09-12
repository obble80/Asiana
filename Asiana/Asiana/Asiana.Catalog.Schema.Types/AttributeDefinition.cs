using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Norm;
using System.Globalization;

namespace Asiana.Catalog.Schema.Types
{
    public class AttributeDefinition 
    {
        public AttributeDefinition()
        {
         
        }

        [MongoIdentifier]
        public String Name
        {
            get;
            set;
        }
        public String BaseType
        {
            get;
            set;
        }
        public String SolrType
        {
            get;
            set;
        }
        public String EditorType
        {
            get;
            set;
        }
        public String NavigationRenderer
        {
            get;
            set;
        }
        public String PageRenderer
        {
            get;
            set;
        }

        //public ILocalisationSerice LocalisationService { get; set; }

        public bool Wildcardable { get; set; }
        public bool Searchable { get; set; }
        public bool Navigable { get; set; }

        public string Category { get; set; }

        //public string DisplayName { get; set; }
        //public string SystemName { get; set; }
        //public string LocalisedName
        //{
        //    get
        //    {

        //    }
        //}

        public string GetLocalisedName(CultureInfo culture)
        {
            // name_locale_baseType_solrType
            return String.Format("{0}_{1}_{2}_{3}", this.Name, culture.Name, this.BaseType, this.SolrType);
        }
    }
}
