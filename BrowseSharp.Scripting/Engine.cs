using BrowseSharp.BOM;
using BrowseSharp.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrowseSharp.Scripting
{
    public class Engine
    {
        protected Jint.Engine JinEngine { get; set; }
        //public BrowserObjectModel BrowserObjectModel { get; }
        public IDocument Document { get; set; }
        public Engine()
        {
            JinEngine = new Jint.Engine();
          //  BrowserObjectModel = new BrowserObjectModel();
        }
        public Engine(IDocument document) : base()
        {
            Document = document;
        }
    }
}
