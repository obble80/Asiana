using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Dynamic;
using System.Threading;

using Asiana.Common.Localisation;

namespace Asiana.Catalog.Search.Solr.Client
{
    public class SolrDocument : DynamicObject
    {
        private XElement document;
        private Dictionary<String, object> dynamicMembers = new Dictionary<string, object>();
        

        public SolrDocument(XElement document)
        {
            this.document = document;
        }


        public XElement GetElement(GetMemberBinder binder)
        {
            var standardPropertyName = "{name}_{culture}_{usageType}_{systemType}_{solrType}".FormatWith(
                new
                {
                    name = binder.Name,
                    culture = Thread.CurrentThread.CurrentUICulture.Name,
                    usageType = "display",
                    systemType = "string",
                    solrType = "string"
                });

            return null;

        }

        public override bool TryGetIndex(GetIndexBinder binder, object[] indexes, out object result)
        {
            string name = indexes[0] as string;
            if (dynamicMembers.ContainsKey(name))
            {
                result = dynamicMembers[name];
                return true;
            }
            else
            {

                var element = document.Elements().Where(x => x.Attribute("name").Value == name).FirstOrDefault();
                if (element != null)
                {
                    if (element.Name == "arr")
                    {
                        List<String> values = new List<string>();
                        foreach (var child in element.Elements())
                        {
                            values.Add(child.Value);
                        }

                        result = values;

                    }
                    else
                    {
                        result = element.Value;
                    }

                    return true;
                }
            }

            result = new List<String>();
            return true;
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            if (dynamicMembers.ContainsKey(binder.Name))
            {
                result = dynamicMembers[binder.Name];
                return true;
            }
            else
            {
                
                var element = document.Elements().Where(x => x.Attribute("name").Value == binder.Name).FirstOrDefault();
                if (element != null)
                {
                    if (element.Name == "arr")
                    {
                        List<String> values = new List<string>();
                        foreach (var child in element.Elements())
                        {
                            values.Add(child.Value);
                        }

                        result = values;

                    }
                    else
                    {
                        result = element.Value;
                    }

                    return true;
                }
            }

            result = new List<String>();
            return true;
        }

        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            if (dynamicMembers.ContainsKey(binder.Name))
            {
                dynamicMembers[binder.Name] = value;
            }
            else
            {
                dynamicMembers.Add(binder.Name, value);
            }

            return true;
        }

        public override IEnumerable<string> GetDynamicMemberNames()
        {
            return document.Elements().Select(x => x.Attribute("name").Value);
        }
    }
}
