using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web;
using System.Text.RegularExpressions;
using System.Web.Routing;

namespace Asiana.Mvc
{
    public class RegexRoute : Route
    {

        private readonly Regex _urlRegex;



        public RegexRoute(string urlPattern, IRouteHandler routeHandler)

            : this(urlPattern, null, routeHandler)

        { }



        public RegexRoute(string urlPattern, RouteValueDictionary defaults, IRouteHandler routeHandler)

            : base(null, defaults, routeHandler)
        {

            _urlRegex = new Regex(urlPattern, RegexOptions.Compiled);

        }



        public override RouteData GetRouteData(HttpContextBase httpContext)
        {

            string requestUrl = httpContext.Request.AppRelativeCurrentExecutionFilePath.Substring(2) + httpContext.Request.PathInfo;



            Match match = _urlRegex.Match(requestUrl);



            RouteData data = null;



            if (match.Success)
            {

                data = new RouteData(this, this.RouteHandler);



                // add defaults first

                if (null != this.Defaults)
                {

                    foreach (KeyValuePair<string, object> def in this.Defaults)
                    {

                        data.Values[def.Key] = def.Value;

                    }

                }



                // iterate matching groups

                for (int i = 1; i < match.Groups.Count; i++)
                {

                    Group group = match.Groups[i];



                    if (group.Success)
                    {

                        string key = _urlRegex.GroupNameFromNumber(i);



                        if (!String.IsNullOrEmpty(key) && !Char.IsNumber(key, 0)) // only consider named groups
                        {

                            data.Values[key] = group.Value;

                        }

                    }

                }

            }



            return data;

        }

    }


}
