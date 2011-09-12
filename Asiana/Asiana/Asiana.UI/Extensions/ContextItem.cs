using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Asiana.UI.Extensions
{
    public class ContextItem
    {
        private Dictionary<String, ContextItem> values = new Dictionary<String, ContextItem>();

        public Dictionary<String, ContextItem> Values
        {
            get { return values; }
            set { values = value; }
        }
        public string ID { get; set; }
        public string Name { get; set; }
        
    }
}