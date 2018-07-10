using System;

namespace BrowseSharp
{
    public interface ISource
    {
        Uri SourceUri { get; set; }
        string Content { get; set; }
    }
}