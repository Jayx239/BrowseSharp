using BrowseSharp.Browsers.Core;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrowseSharpTest
{
    [TestFixture]
    public class Core
    {
        [Test]
        public void DefaultUriProtocolTest()
        {
            BrowserCore core = new BrowserCore();
            Assert.AreEqual("http", core.DefaultUriProtocol);
            core.DefaultUriProtocol = "https";
            Assert.AreEqual("https", core.DefaultUriProtocol);
        }
    }
}
