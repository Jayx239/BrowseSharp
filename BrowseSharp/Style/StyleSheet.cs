using System;
using System.Collections.Generic;

namespace BrowseSharp.Style
{
    public class StyleSheet : ISource
    {
        public StyleSheet(Uri sourceUri)
        {
            SourceUri = sourceUri;
        }
        public StyleSheet(string StyleString)
        {
            Content = StyleString;
        }
        public StyleSheet(string StyleString, Uri sourceUri)
        {
            SourceUri = sourceUri;
            Content = StyleString;
        }
        
        public Uri SourceUri { get; set; }
        public string Content { get; set; }
        private List<Style> Styles { get; set; }

        private void ParseStyles(string styleString)
        {
            
        }
    }
}