using BrowseSharp.Common;
using Jint;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
		</script>
	</body>
</html>";

        [TestMethod]
        public void TestMethod1()
        {
            BOM.Window.Window window = new Window.Window();
        }

        [TestMethod]
        public void Test()
        {
            Engine jintEngine = new Engine();
            jintEngine.SetValue("context", new { time = "10:2:01" });
            var val = jintEngine.GetValue("context");
            Assert.AreEqual("10:2:01", val.AsObject().GetOwnProperty("time").Value);
        }

        public void Test2()
        {
            Window.Window window = new Window.Window();
            
        }

        [TestMethod]
        public void Test3()
        {
            Jint.Engine engine = new Jint.Engine();
            IDocument htmlDocument = new Document();
            htmlDocument.HtmlDocument = new AngleSharp.Html.Parser.HtmlParser().ParseDocument(htmlContent);

            Window.Window window1 = new Window.Window(engine);
            window1.document = htmlDocument.HtmlDocument;

            window1.InitializeEngine();
            var jquery = System.IO.File.ReadAllText(@"../../../../BrowseSharpPlayground/jquery.js");
            engine.Execute(jquery);

            engine.Execute("$(document).ready(function(){$('#content').text('hello there it worked');});");
            Assert.AreEqual("hello there it worked", window1.document.GetElementById("content").TextContent);
        }
    }
}
