using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Asiana.Catalog.Search.Solr.Client;
using Asiana.Catalog.Search.Solr.Client.Query;
using Norm;

namespace Asiana.UI.Controllers
{
    public class SlotController : Controller
    {
        private ISolrSearchService searchService;
        private IMongo mongoService;

        public SlotController(ISolrSearchService searchService, IMongo mongoService)
        {
            this.searchService = searchService;
            this.mongoService = mongoService;
        }
        //
        // GET: /Slot/

        public ActionResult Slot(string page, string slot)
        {
            // TODO: Get a slot by the name
            // TODO: Get a slot template
            // TODO: Get slot content
            var query = new DocumentQuery();

            query.Ids.Add("J129528262");
            query.Ids.Add("J129528261");

            var model = searchService.Execute(query);
            
            return PartialView("Vertical3ProductSlot", model);
        }

    }
}
