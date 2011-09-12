using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ninject.Modules;
using Ninject;
using Norm;
namespace Asiana.Catalog.Schema.Types
{
    public class SchemaModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ISchemaService>().To<SchemaService>();
            Bind<IMongo>()
                .ToMethod(x => Mongo.Create("mongodb://localhost:27017/catalog"))
                .InRequestScope();
        }
    }
}
