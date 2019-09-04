using System;
using AngleSharp.Html.Dom;
using BrowseSharp.Common;

namespace BrowseSharp.Common.Javascript
{
    /// <summary>
    /// Class containing javascript details
    /// </summary>
    public class Javascript : ISource
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="scriptElement"></param>
        public Javascript(IHtmlScriptElement scriptElement)
        {
            ScriptElement = scriptElement;
            Content = scriptElement.Text;
        }

        /// <summary>
        /// AngleSharp html element for script
        /// </summary>
        public IHtmlScriptElement ScriptElement { get; set; }

        /// <summary>
        /// Uri of script source
        /// </summary>
        public Uri SourceUri { get; set; }

        /// <summary>
        /// Script content
        /// </summary>
        public String Content { get; set; }

        /// <summary>
        /// Same as script content, may be deprecated in the future, use Content instead
        /// </summary>
        public string JavascriptString
        {
            get { return Content; }
            set { Content = value; }
        }


    }
}