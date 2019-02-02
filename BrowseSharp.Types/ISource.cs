using System;

namespace BrowseSharp.Types
{
    /// <summary>
    /// Interface for objects that have a uri and content
    /// </summary>
    public interface ISource
    {
        /// <summary>
        /// Uri of object source
        /// </summary>
        Uri SourceUri { get; set; }
        
        /// <summary>
        /// Content of object
        /// </summary>
        string Content { get; set; }
    }
}