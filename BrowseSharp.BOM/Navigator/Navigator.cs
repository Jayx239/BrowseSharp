using Jint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrowseSharp.BOM.Navigator
{
    public class Navigator : INavigator
    {

        protected Engine _jintEngine;
        public Navigator(Engine jintEngine)
        {
            _jintEngine = jintEngine;
        }

        protected INavigator _navigator => _jintEngine.GetValue("Navigator").TryCast<INavigator>();

        public string appCodeName { get; set; }

        public bool cookieEnabled { get; set; }

        public int maxTouchPoints { get; set; }

        public bool onLine { get; set; }

        public string oscpu { get; set; }

        public string platform { get; set; }

        public string product { get; set; }

        public string userAgent { get; set; }

        public bool webdriver { get; set; }

        public int deviceMemory { get; set; }

        public string doNotTrack { get; set; }

        public string vendor { get; set; }

        public string vendorSub { get; set; }

        public void InitializeEngine()
        {
            _jintEngine.SetValue("Navigator", this);
            _jintEngine.SetValue("navigator", this);

        }
    }
}
