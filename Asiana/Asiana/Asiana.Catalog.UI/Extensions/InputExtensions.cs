using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Text;
using System.Linq.Expressions;
using Asiana.Catalog.Schema.Types;

namespace Asiana.Catalog.UI.Extensions
{
    public static class InputExtensions
    {
        /// <summary>
        /// Renders a table with a checkbox for each item in the list using the specified name or id selector.
        /// All other members of the object are rendered as hidden inputs so that the objects are rebuilt when posting
        /// back the form.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="helper"></param>
        /// <param name="function"></param>
        /// <returns></returns>
        //public static MvcHtmlString SelectionGridFor<TModel,TProperty>(this HtmlHelper<TModel> helper, Expression<Func<TModel,TProperty>> expression, IEnumerable<TModel> items, Func<TModel,String> idSelector)
        //{
        //    //ModelMetadata modelMetadata = ModelMetadata.FromLambdaExpression<TModel, TProperty>(expression, helper.ViewData);
        //    //StringBuilder builder = new StringBuilder();
           
        //    //var listItems = items.ToList();
        //    //foreach(var item in listItems) {
        //    //    TagBuilder tagBuilder = new TagBuilder("input");
        //    //    tagBuilder.MergeAttribute("type", "checkbox");
        //    //    tagBuilder.MergeAttribute("name", listItems.IndexOf(item)
        //    //    tagBuilder.Append("<input type='checkbox' name='" + idSelector(item) + "'/>");
        //    //}

        //    //return MvcHtmlString.Create(builder.ToString());


        //public static MvcHtmlString EditorFor(Product product)
        //{
        //    StringBuilder builder = new StringBuilder();

        //    foreach (var attribute in product.Schema.Attributes)
        //    {
        //        var editor = attribute.EditorType;
        //        var value = product

        //    }
        //}
    }
}