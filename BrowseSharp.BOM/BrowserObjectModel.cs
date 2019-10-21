using BrowseSharp.BOM.Navigator;
using BrowseSharp.BOM.Window;
using BrowseSharp.Common;
using Jint;
using System;
using System.Collections.Generic;
using System.Text;

namespace BrowseSharp.BOM
{
    public class BrowserObjectModel : IBrowserObjectModel
    {
        public BrowserObjectModel(IDocument document)
        {
            Engine = new Engine();
            Window = new Window.Window(Engine, document);
            //Navigator = new Navigator();

        }
        public Engine Engine { get; protected set; }
        public IWindow Window { get; }
        public INavigator Navigator { get; }
    }
}
