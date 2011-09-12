using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Asiana.Catalog.UI.Extensions
{
    public static class RazorExtensions
    {
        public static IEnumerable<SelectListItem> ToSelectList<T>(this IEnumerable<T> list, 
            Func<T, string> idSelector,
            Func<T, string> textSelector)
        {
            foreach (var item in list)
            {
                yield return new SelectListItem() { Value = idSelector(item), Text = textSelector(item) };
            }
        }
    }
}