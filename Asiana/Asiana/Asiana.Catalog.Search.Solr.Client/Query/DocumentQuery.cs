using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Asiana.Catalog.Search.Solr.Client.Query
{
    public class DocumentQuery : ISolrQuery
    {
        private List<String> ids = new List<String>();

        public List<String> Ids
        {
            get { return ids; }
            set { ids = value; }
        }

        #region ISolrQuery Members

        public string ToSolrQuery()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("&q=");
            List<string> delimitedList = new List<string>();

            foreach(var item in ids) {
                delimitedList.Add(String.Format("ID:{0}", item));
            }
            if (ids.Count > 1)
            {
                builder.Append(String.Join(" OR ", delimitedList));
            }
            else
            {
                builder.Append(String.Format("ID:{0}", ids[0]));
            }

            return builder.ToString();
        }

        #endregion
    }
}
