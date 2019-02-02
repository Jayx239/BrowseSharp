using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BrowseSharp.BOM.Navigator;
using BrowseSharp.BOM.Window;

namespace BrowseSharp.BOM.Test
{
    [TestClass]
    public class UnitTest1
    {
        public static string htmlContent = @"
<html>
	<head>
		
	</head>
	<body>
		<div id='content'>
		
		</div>
		<script>
			document.getElementById('content').textContent = 'this is the content';
		</script>
	</body>
</html>";

        [TestMethod]
        public void TestDocument()
        {
            IDocument doc = new Document();
            doc.HtmlDocument = new AngleSharp.Parser.Html.HtmlParser().Parse(htmlContent);
            Jint.Engine engine = new Jint.Engine();
            engine.SetValue("document", doc.HtmlDocument);
            engine.SetValue("window", new BrowseSharp.BOM.Window.Window(doc));
            var scripts = "document.getElementById('content').textContent = 'this is the content';";
            
            Assert.AreEqual(doc.HtmlDocument.Body.QuerySelector("#content").TextContent.Trim(), "");
            var result = engine.Execute(scripts);
            Assert.AreEqual(doc.HtmlDocument.Body.QuerySelector("#content").TextContent.Trim(), "this is the content");
        }

        [TestMethod]
        public void TestDocumentNotSet()
        {
            IDocument doc = new Document();
            doc.HtmlDocument = new AngleSharp.Parser.Html.HtmlParser().Parse(htmlContent);
            Jint.Engine engine = new Jint.Engine();
            engine.SetValue("window", new BrowseSharp.BOM.Window.Window(doc));
            var scripts = "document.getElementById('content').textContent = 'this is the content';";

            try
            { 
                var result = engine.Execute(scripts);
            }
            catch (Exception ex)
            {
                Assert.AreEqual(ex.Message, "document is not defined");
            }
        }
    }
}
