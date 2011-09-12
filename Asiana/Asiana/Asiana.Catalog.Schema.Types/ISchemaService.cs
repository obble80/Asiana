using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Asiana.Catalog.Schema.Types
{
    public interface ISchemaService
    {
        IEnumerable<String> GetRenderers();
        IEnumerable<String> GetEditors();
        IEnumerable<Type> GetBaseTypes();
        
        AttributeDefinition GetAttribute(string name);
        IEnumerable<AttributeDefinition> GetAttributes();
        IEnumerable<AttributeDefinition> GetAttributes(IEnumerable<String> names);
        void SaveAttribute(AttributeDefinition attribute);
        
        
        ProductType GetProductType(string name);
        IEnumerable<ProductType> GetProductTypes();
        void SaveProductType(ProductType productType);

        Product GetProduct(string name);
        void SaveProduct(Product product);
       
    }
}
