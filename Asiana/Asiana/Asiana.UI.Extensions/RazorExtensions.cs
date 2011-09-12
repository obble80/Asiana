using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;
using System.Web.Mvc.Ajax;
using System.Web.Routing;
using System.Linq.Expressions;
//using Endeca.Data;

namespace Asiana.UI.Extensions
{
    public class HtmlAttribute : IHtmlString
    {
        private string _InternalValue = String.Empty;
        private string _Seperator;

        public string Name { get; set; }
        public string Value { get; set; }
        public bool Condition { get; set; }

        public HtmlAttribute(string name)
            : this(name, null)
        {
        }

        public HtmlAttribute(string name, string seperator)
        {
            Name = name;
            _Seperator = seperator ?? " ";
        }

        public HtmlAttribute Add(string value)
        {
            return Add(value, true);
        }

        public HtmlAttribute Add(string value, bool condition)
        {
            if (!String.IsNullOrWhiteSpace(value) && condition)
                _InternalValue += value + _Seperator;

            return this;
        }

        #region IHtmlString Members

        public string ToHtmlString()
        {
            
            return Name + "=\"" + _InternalValue + "\"";
        }

        #endregion
    }

    public static class RazorExtensions
    {
        public static HelperResult List<T>(this IEnumerable<T> items,
          Func<T, HelperResult> template)
        {
            return new HelperResult(writer =>
            {
                foreach (var item in items)
                {
                    template(item).WriteTo(writer);
                }
            });
        }


        public static MvcHtmlString ImageActionLink(this AjaxHelper helper, string imageUrl, string altText, string imageClass, string controllerName, string actionName, object routeValues, AjaxOptions ajaxOptions)
        {
            var builder = new TagBuilder("img");
            builder.MergeAttribute("src", imageUrl);
            builder.MergeAttribute("alt", altText);
            builder.AddCssClass(imageClass);
            var link = helper.ActionLink("[replaceme]", actionName, controllerName, routeValues, ajaxOptions);
            var html = link.ToHtmlString().Replace("[replaceme]", builder.ToString(TagRenderMode.SelfClosing));
            return new MvcHtmlString(html);

        }

        public static MvcHtmlString HtmlActionLink(this HtmlHelper helper, Func<dynamic, HelperResult> html, string actionName, string controllerName, object routeValues, object htmlAttributes)
        {
            var requestContext = helper.ViewContext.RequestContext;
            var routes = helper.RouteCollection;

            string value = UrlHelper.GenerateUrl(null, actionName, controllerName, new RouteValueDictionary(routeValues), routes, requestContext, true);
            TagBuilder tagBuilder = new TagBuilder("a");
            tagBuilder.InnerHtml = html(null).ToHtmlString();
            TagBuilder tagBuilder2 = tagBuilder;
            tagBuilder2.MergeAttributes<string, object>(HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
            tagBuilder2.MergeAttribute("href", value);
            var tag = tagBuilder2.ToString(TagRenderMode.Normal);

            return MvcHtmlString.Create(tag);
        }

        public static MvcHtmlString HtmlActionLink(this AjaxHelper ajaxHelper, Func<dynamic, HelperResult> html, string actionName, string controllerName, object routeValues, AjaxOptions ajaxOptions, IDictionary<string, object> htmlAttributes)
        {
            var requestContext = ajaxHelper.ViewContext.RequestContext;
            var routes = ajaxHelper.RouteCollection;
            
            string targetUrl = UrlHelper.GenerateUrl(null, actionName, controllerName, new RouteValueDictionary(routeValues), routes, requestContext, true);

            TagBuilder tagBuilder = new TagBuilder("a");
            tagBuilder.InnerHtml = html(null).ToHtmlString();
            TagBuilder tagBuilder2 = tagBuilder;
            tagBuilder2.MergeAttributes<string, object>(HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
            tagBuilder2.MergeAttribute("href", targetUrl);
            tagBuilder2.MergeAttributes<string, object>(ajaxOptions.ToUnobtrusiveHtmlAttributes());
                      
            return MvcHtmlString.Create(tagBuilder2.ToString(TagRenderMode.Normal));
        }


        public static MvcHtmlString DisplayNameFor<TModel, TProperty>(
       this HtmlHelper<TModel> htmlHelper,
       Expression<Func<TModel, TProperty>> expression)
        {
            var metaData = ModelMetadata.FromLambdaExpression<TModel, TProperty>(expression, htmlHelper.ViewData);
            string value = metaData.DisplayName ?? (metaData.PropertyName ?? ExpressionHelper.GetExpressionText(expression));
            return MvcHtmlString.Create(value);
        }

        public static MvcHtmlString IsSelected<T>(this HtmlHelper helper, T item, T otherItem)
        {
            if (item.Equals(otherItem))
            {
                return MvcHtmlString.Create("checked");
            }

            return MvcHtmlString.Empty;
        }

        public static HtmlAttribute Css(this HtmlHelper html, string value)
        {
            return Css(html, value, true);
        }

        public static HtmlAttribute Css(this HtmlHelper html, string value, bool condition)
        {
            return Css(html, null, value, condition);
        }

        public static HtmlAttribute Css(this HtmlHelper html, string seperator, string value, bool condition)
        {
            return new HtmlAttribute("class", seperator).Add(value, condition);
        }

        public static HtmlAttribute Attr(this HtmlHelper html, string name, string value)
        {
            return Attr(html, name, value, true);
        }

        public static HtmlAttribute Attr(this HtmlHelper html, string name, string value, bool condition)
        {
            return Attr(html, name, null, value, condition);
        }

        public static HtmlAttribute Attr(this HtmlHelper html, string name, string seperator, string value, bool condition)
        {
            return new HtmlAttribute(name, seperator).Add(value, condition);
        }

    }
}