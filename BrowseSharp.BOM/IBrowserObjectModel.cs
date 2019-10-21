using BrowseSharp.BOM.Navigator;
using BrowseSharp.BOM.Window;
using System;
using System.Collections.Generic;
using System.Text;

namespace BrowseSharp.BOM
{
    public interface IBrowserObjectModel
    {

        IWindow Window { get; }
        INavigator Navigator { get; }
    }
}
