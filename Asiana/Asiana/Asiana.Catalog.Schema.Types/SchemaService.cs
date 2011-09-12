using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.IO;
using Norm;
using Norm.BSON;
using System.Diagnostics;

namespace Asiana.Catalog.Schema.Types
{
    public class SchemaService : ISchemaService
    {

        IMongo mongoService;
        public SchemaService(IMongo mongoService)
        {
            this.mongoService = mongoService;
        }

        /// <summary>
        /// This method should belong to UI services
        /// </summary>
        /// <returns></returns>
        public IEnumerable<String> GetRenderers()
        {
            List<String> rendererTypes = new List<string>();
            rendererTypes.Add("Label");
            rendererTypes.Add("Colour Palette");
            rendererTypes.Add("Price");
            rendererTypes.Add("Slider");

            return rendererTypes;
        }

        /// <summary>
        /// This method should belong to editor services
        /// </summary>
        /// <returns></returns>
        public IEnumerable<String> GetEditors()
        {
            List<String> editorTypes = new List<string>();
            editorTypes.Add("Price Editor");
            editorTypes.Add("Colour Picker");
            editorTypes.Add("Html Editor");
            editorTypes.Add("Image Editor");
            editorTypes.Add("Calendar");

            return editorTypes;
        }

        /// <summary>
        /// This one is fine
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Type> GetBaseTypes()
        {
            List<Type> baseTypes = new List<Type>();
            baseTypes.Add(typeof(int));
            baseTypes.Add(typeof(long));
            baseTypes.Add(typeof(string));
            // baseTypes.Add(typeof(Colour));
            baseTypes.Add(typeof(Decimal));
            baseTypes.Add(typeof(Boolean));
            return baseTypes;
        }

        public IEnumerable<ProductType> GetProductTypes()
        {
           
            return mongoService
                    .GetCollection<ProductType>("types")
                    .AsQueryable();
           
        }

        public AttributeDefinition GetAttribute(string name)
        {
            return mongoService
                .GetCollection<AttributeDefinition>("attributes")
                .AsQueryable()
                .Where(x => x.Name == name)
                .FirstOrDefault();
        
        }

        public void SaveProduct(Product product)
        {

            mongoService.GetCollection<Product>("products").Save(product);
        }

        public Product GetProduct(string name)
        {
            return mongoService
                .GetCollection<Product>("products")
                .AsQueryable()
                .Where(x => x["Title"] as string == name)
                .FirstOrDefault();
        }

        public void SaveAttribute(AttributeDefinition attribute)
        {
            mongoService
                .GetCollection<AttributeDefinition>("attributes")
                .Save(attribute);
           
        }

        public IEnumerable<AttributeDefinition> GetAttributes()
        {
            return mongoService
                .GetCollection<AttributeDefinition>("attributes")
                .AsQueryable();  
        }

        public IEnumerable<AttributeDefinition> GetAttributes(IEnumerable<String> names)
        {
            return mongoService
                .GetCollection<AttributeDefinition>("attributes")
                .AsQueryable()
                .ToList()
                .Where(x => names.Contains(x.Name));
        }

        #region ISchemaService Members


        public ProductType GetProductType(string name)
        {
            return mongoService
                    .GetCollection<ProductType>("types")
                    .AsQueryable()
                    .Where(x => x.Name == name)
                    .FirstOrDefault();   
        }

        public void SaveProductType(ProductType productType)
        {
            mongoService
                .GetCollection<ProductType>("types")
                .Save(productType);
        }

        #endregion
    }
}
