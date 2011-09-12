using System;
using System.Collections.Generic;
using System.Text;

namespace Asiana.Merchandising
{
    public class Slot
    {
        /// <summary>
        /// The slot name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// The page name this slot belongs to
        /// </summary>
        public string Page { get; set; }
        /// <summary>
        /// The title to be displayed in the slot
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// The description of the slot
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// The template to render the slot
        /// </summary>
        public string Template { get; set; }
    }
}
