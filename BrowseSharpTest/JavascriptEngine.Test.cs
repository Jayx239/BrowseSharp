using BrowseSharp;
using BrowseSharp.Javascript;
using Jint;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrowseSharpTest
{
    [TestFixture]
    public class JavascriptEngine
    {
        [Test]
        public void Test()
        {
            Browser browser = new Browser();
            IDocument document = browser.Navigate("www.browsesharp.com");
            browser.JavascriptEngine.Document = document.Scripts[0].Content;
            var value = browser.JavascriptEngine.Execute("$(document).html()");
        }
        [Test]
        public void Test2()
        {
            Browser browser = new Browser();
            IDocument document = browser.Navigate("http://browsesharp.org/testsitesjqueryrender.html");
            browser.JavascriptEngine.Document = document.Scripts[0].Content;
            JavascriptEvalEngine jsEngine = new JavascriptEvalEngine();
            var result = jsEngine.EvaluateScript(document);
        }
    }
}
