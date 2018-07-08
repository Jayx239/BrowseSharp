using System;

namespace BrowseSharp.Javascript
{
    public class Javascript : ISource
    {
        public Javascript() { }

        public Javascript(string javascriptString)
        {
            JavascriptString = javascriptString;
        }
        
        public Javascript(string javascriptString, Uri sourceUri)
        {
            JavascriptString = javascriptString;
            SourceUri = sourceUri;
        }
        
        public string JavascriptString { get; set; }
        public Uri SourceUri { get; set; }
    }
}