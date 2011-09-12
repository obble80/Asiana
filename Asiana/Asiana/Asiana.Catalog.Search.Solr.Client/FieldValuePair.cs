using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Asiana.Catalog.Search.Solr.Client
{
    public class FieldValuePair
    {
        public string Field { get; set; }
        public string Value { get; set; }

        public override int GetHashCode()
        {
            return Field.GetHashCode() ^ Value.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            var fieldValuePair = obj as FieldValuePair;

            if (fieldValuePair != null 
                && fieldValuePair.Field == this.Field 
                && fieldValuePair.Value == this.Value)
            {
                return true;
            }

            return false;
        }
    }
}
