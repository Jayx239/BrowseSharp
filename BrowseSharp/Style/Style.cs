using System;

namespace BrowseSharp.Style
{
    /// <summary>
    /// Class containing a css style
    /// </summary>
    public class Style : ISource
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public Style() { }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="styleString"></param>
        public Style(string styleString)
        {
            StyleString = styleString;
        }
        
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="styleString"></param>
        /// <param name="sourceUri"></param>
        public Style(string styleString, Uri sourceUri)
        {
            StyleString = styleString;
            SourceUri = sourceUri;
        }

        /// <summary>
        /// CSS style content
        /// </summary>
        public string Content { get; set; }
        
        /// <summary>
        /// CSS style source uri
        /// </summary>
        public Uri SourceUri { get; set; }
        
        /// <summary>
        /// Same ass Content, use Content instead
        /// </summary>
        public string StyleString
        {
            get { return Content; }
            set { Content = value; }
        }
    }
}