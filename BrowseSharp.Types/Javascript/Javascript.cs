using System;
using AngleSharp.Dom.Html;

namespace BrowseSharp.Types.Javascript
{
    /// <summary>
    /// Class containing javascript details
    /// </summary>
    public class Javascript : ISource
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="scriptElement"></param>
        public Javascript()
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="scriptElement"></param>
        public Javascript(IHtmlScriptElement scriptElement)
        {
            ScriptElement = scriptElement;
        }
        
        /// <summary>
        /// AngleSharp html element for script
        /// </summary>
        public IHtmlScriptElement ScriptElement { get; set; }
        
        /// <summary>
        /// Uri of script source
        /// </summary>
        public Uri SourceUri
        {
            get { return !string.IsNullOrEmpty(ScriptElement.Source) ? new Uri(ScriptElement.Source) : null; }
            set { ScriptElement.Source = value.AbsoluteUri; }
        }

        /// <summary>
        /// Script content
        /// </summary>
        public String Content
        {
            get { return ScriptElement != null ? ScriptElement.Text : ""; }
            set { ScriptElement.Text = value; }
        }

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