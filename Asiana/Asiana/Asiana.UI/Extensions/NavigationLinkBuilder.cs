using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Endeca.Data;
using System.Text;
using System.Xml.Linq;
using System.Xml.XPath;

namespace Asiana.UI.Extensions
{
    public class NavigationLinkBuilder
    {
        XElement dimensions;
        Dictionary<String, ContextItem> contextItems = new Dictionary<String, ContextItem>();
        Dictionary<String, String> idToDimensions = new Dictionary<string, string>();

        public NavigationLinkBuilder(string dimensionsPath)
        {
            dimensions = XElement.Load(dimensionsPath);
            //Create context items for dimension name/value pairs
            foreach(var node in dimensions.XPathSelectElements("//DIMENSION")) {
                string name = node.Attribute("NAME").Value;
                string id = node.Descendants("DIMENSION_ID").First().Attribute("ID").Value;
               
                var item = new ContextItem() { Name = name, ID = id };
                var count = 0;
                foreach (var descendant in node.Descendants("DIMENSION_NODE"))
                {
                    // Skip first
                    if (count != 0)
                    {
                        // get id
                        var a = descendant.Descendants("DVAL_ID").First().Attribute("ID").Value;
                        // get name from syn
                        var b = descendant.Descendants("SYN").First().Value;

                        item.Values.Add(b, new ContextItem() { Name = b, ID = a });

                        idToDimensions.Add(a, id);
                    }

                    count++;
                }
     
                contextItems.Add(name, item);
            }
        }

        public IEnumerable<String> Parse(String path)
        {
            String[] parts = path.Trim('/').Split('/');
           
            // Get the last item
            var ids = parts[parts.Length - 1].Split('Z');
            foreach (var id in ids)
            {
                long valueId = Base36Decode(id);
                long dimensionId = long.Parse(idToDimensions[valueId.ToString()]);

                yield return String.Format("{0}:{1}||1", dimensionId, valueId);
            }
        }

        public static string BuildBreadCrumbLink(NavigationResult navigation, DimensionValue refinement)
        {
            string link = null;
            List<String> navStates = new List<string>();

            foreach (var item in navigation.AppliedFiltersResult.DimensionValues)
            {

                if (item.Id == refinement.Id)
                {
                    link = link + "/" + item.Dimension.DisplayName + "/" + item.DisplayName;
                    navStates.Add(Base36Encode(long.Parse(item.Id)));
                    break;
                }
                else
                {
                    link = link + "/" + item.Dimension.DisplayName + "/" + item.DisplayName;
                    navStates.Add(Base36Encode(long.Parse(item.Id)));
                }
            }
            
            link = link + "/" + String.Join("Z", navStates);
            return link.Replace(' ', '-');
        }

        public static string BuildNavigationLink(NavigationResult navigation, DimensionValue refinement)
        {
            string link = null;
            List<String> navStates = new List<string>();

            foreach (var item in navigation.AppliedFiltersResult.DimensionValues)
            {
                if (!(item.Dimension.Id == refinement.Dimension.Id
                    && refinement.Dimension.MultiSelect == MultiSelect.None
                    && refinement.Parent != null))
                {
                    link = link + "/" + item.Dimension.DisplayName + "/" + item.DisplayName;
                    navStates.Add(Base36Encode(long.Parse(item.Id)));
                }

             

            }
            navStates.Add(Base36Encode(long.Parse(refinement.Id)));
            link = link + "/" + refinement.Dimension.DisplayName + "/" + refinement.DisplayName + "/" + String.Join("Z", navStates);
            return link.Replace(' ', '-');
        }

        private static long Base36Decode(string inputString)
        {
            string clist = "0123456789abcdefghijklmnopqrstuvwxyz";
  
            inputString = Reverse(inputString.ToLower());
            long result = 0;
            int pos = 0;
            foreach (char c in inputString)
            {
                result += clist.IndexOf(c) * (long)Math.Pow(36, pos);
                pos++;
            }
            return result;
        }

        private static string Base36Encode(long inputNumber)
        {
            char[] clist = new char[] { '0', '1', '2', '3', '4',
                                '5', '6', '7', '8', '9',
                                'a', 'b', 'c', 'd', 'e',
                                'f', 'g', 'h', 'i', 'j',
                                'k', 'l', 'm', 'n', 'o',
                                'p', 'q', 'r', 's', 't',
                                'u', 'v', 'w', 'x', 'y',
                                'z' };

            StringBuilder sb = new StringBuilder();
            while (inputNumber != 0)
            {
                sb.Append(clist[inputNumber % 36]);
                inputNumber /= 36;
            }
            return Reverse(sb.ToString());
        }

        private static string Reverse(string input)
        {
            Stack<char> resultStack = new Stack<char>();
            foreach (char c in input)
            {
                resultStack.Push(c);
            }

            StringBuilder sb = new StringBuilder();
            while (resultStack.Count > 0)
            {
                sb.Append(resultStack.Pop());
            }
            return sb.ToString();
        }
    }
}