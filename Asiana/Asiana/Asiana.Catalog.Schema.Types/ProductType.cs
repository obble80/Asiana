using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Norm;

namespace Asiana.Catalog.Schema.Types
{
    public class ProductType
    {
        private List<AttributeDefinition> attributes = new List<AttributeDefinition>();
        public ObjectId Id { get; set; }
        public String Version { get; set; }

        public List<AttributeDefinition> Attributes
        {
            get { return attributes; }
            set { attributes = value; }
        }

        public string Name { get; set; }
    }
}
