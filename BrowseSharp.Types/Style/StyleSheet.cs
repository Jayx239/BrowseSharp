using System;

namespace BrowseSharp.Style
{
    /// <summary>
    /// Class containing stylesheet content
    /// </summary>
    public class StyleSheet : ISource
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="sourceUri"></param>
        public StyleSheet(Uri sourceUri)
        {
            SourceUri = sourceUri;
        }
        
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="styleSheetString"></param>
        public StyleSheet(string styleSheetString)
        {
            Content = styleSheetString;
        }
        
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="styleSheetString"></param>
        /// <param name="sourceUri"></param>
        public StyleSheet(string styleSheetString, Uri sourceUri)
        {
            SourceUri = sourceUri;
            Content = styleSheetString;
        }
        
        /// <summary>
        /// Source uri of stylesheet
        /// </summary>
        public Uri SourceUri { get; set; }
        
        /// <summary>
        /// Content of stylesheet
        /// </summary>
        public string Content { get; set; }
    }
}