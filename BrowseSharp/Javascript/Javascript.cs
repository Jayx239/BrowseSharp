using System;
using AngleSharp.Dom.Html;

namespace BrowseSharp.Javascript
{
    public class Javascript : ISource
    {
        public Javascript(IHtmlScriptElement scriptElement)
        {
            ScriptElement = scriptElement;
        }
        
        public IHtmlScriptElement ScriptElement { get; set; }
        
        public Uri SourceUri
        {
            get { return !string.IsNullOrEmpty(ScriptElement.Source) ? new Uri(ScriptElement.Source) : null; }
            set { ScriptElement.Source = value.AbsoluteUri; }
        }

        public String Content
        {
            get { return ScriptElement != null ? ScriptElement.Text : ""; }
            set { ScriptElement.Text = value; }
        }
        
        public string JavascriptString
        {
            get { return Content; }
            set { Content = value; }
        }


    }
}