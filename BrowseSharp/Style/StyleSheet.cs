using System;

namespace BrowseSharp.Style
{
    public class StyleSheet : ISource
    {
        public StyleSheet(Uri sourceUri)
        {
            SourceUri = sourceUri;
        }
        public StyleSheet(string styleSheetString)
        {
            Content = styleSheetString;
        }
        public StyleSheet(string styleSheetString, Uri sourceUri)
        {
            SourceUri = sourceUri;
            Content = styleSheetString;
        }
        
        public Uri SourceUri { get; set; }
        public string Content { get; set; }
    }
}