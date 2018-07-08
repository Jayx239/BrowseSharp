using System;

namespace BrowseSharp.Style
{
    public class Style : ISource
    {
        public Style() { }

        public Style(string styleString)
        {
            StyleString = styleString;
        }
        
        public Style(string styleString, Uri sourceUri)
        {
            StyleString = styleString;
            SourceUri = sourceUri;
        }
        
        public string StyleString { get; set; }
        public Uri SourceUri { get; set; }
    }
}