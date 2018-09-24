using BrowseSharp.History;
using System;
using System.Collections.Generic;
using System.Text;

namespace BrowseSharp.Browsers
{
    public interface IBrowserHistorySync : IHistory
    {
        /// <summary>
        /// Refresh page, re-submits last request
        /// </summary>
        /// <returns></returns>
        IDocument Refresh();
    }
}
