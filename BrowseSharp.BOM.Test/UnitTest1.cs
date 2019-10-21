
using AngleSharp;
using AngleSharp.Html.Dom;
using BrowseSharp.Common;
using Jint;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

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

            Window.Window window1 = new Window.Window(engine, htmlDocument);
            //window1.Document = htmlDocument;

            window1.InitializeEngine();
            var jquery = System.IO.File.ReadAllText(@"../../../../BrowseSharpPlayground/jquery.js");
            engine.Execute(jquery);
            engine.Execute("var $ = window.jQuery;");
            engine.Execute("$('#content').text('hello there it worked');");
            //engine.Execute("$(document).trigger('ready')");
            Assert.AreEqual("hello there it worked", window1.Document.GetElementById("content").TextContent);
        }
        [TestMethod]
        public void Test4()
        {
            Jint.Engine engine = new Jint.Engine();
            IDocument htmlDocument = new Document();
            htmlDocument.HtmlDocument = new AngleSharp.Html.Parser.HtmlParser().ParseDocument(htmlContent);

            Window.Window window1 = new Window.Window(engine, htmlDocument);
            //window1.Document = htmlDocument;

            window1.InitializeEngine();
            var jquery = System.IO.File.ReadAllText(@"../../../../BrowseSharpPlayground/jquery.js");
            //engine.Execute("window.document.readyState = \"Loading\";");
            engine.Execute(jquery);

            engine.Execute("var $ = window.jQuery;");
            var script = "$(document).ready(function() {$('#content').text('hello there it worked');})";
            //var cleanedScript = new Regex();
            Regex regex = new Regex(@"(\$\([^document]*document[^)]*\)[^.]*.ready[^(]*\([^function]*function[^(]*\([^)]*\)[^{]*{)([^a|a]*)(}\))");
            engine.Execute(regex.Match(script).Groups[2].Value);
            //engine.Execute("window.document.readyState = 'complete'");
            //engine.Execute("$(document).trigger('ready',window.document)");
            //engine.Execute("$(document).trigger('ready')");
            Assert.AreEqual("hello there it worked", window1.Document.GetElementById("content").TextContent);
        }

        [TestMethod]
        public void TestJqueryRegexScraper()
        {
            string firstDocReady = 
@"$(document).ready(function() {
    $('#content').text('hello there it worked');
    $('#content').click(function() {

    });
})";
            string secondDocReady = 
@"$(document).ready(function() {
    console.log('({});');
});";
            string sampleJquery = firstDocReady + "\n" + secondDocReady;

            Regex regex = new Regex(@"(\$\([^document]*document[^)]*\)[^.]*.ready[^(]*\([^function]*function[^(]*\([^)]*\)[^{]*{)([^a|a]*)(}\))");

            var matches = regex.Matches(sampleJquery);
            Assert.AreEqual("\r\n    $('#content').text('hello there it worked');\r\n    $('#content').click(function() {\r\n\r\n    });\r\n", matches[0].Groups[2].Value);
            Assert.AreEqual("\r\n    console.log('({});');\r\n", matches[1].Groups[2].Value);
        }

        [TestMethod]
        public async Task jQueryClickTest()
        {
            string script = "var numClicks = 0;\n" +
                "$('#btn').click(function() {\n" +
                 "$('#message').text(\"hello there you clicked the button \" + numClicks + \" times\");\n" +
                "});";
            string html = @"<!DOCTYPE html>\n<html>
	<head>
		
	</head>
	<body>
		<div id='message'>empty</div>
        <input type='button' id='btn'>Click me please</input>
	</body>
</html>";

            Jint.Engine engine = new Jint.Engine();
            IDocument document = new Document();
            var parser = new AngleSharp.Html.Parser.HtmlParser(new AngleSharp.Html.Parser.HtmlParserOptions() { IsScripting = true });
            
            bool waitForScripts = true;
            //parser.AddEventListener(AngleSharp.Dom.EventNames.Parsing, (target,ev) => { document.HtmlDocument = (IHtmlDocument)target; while (waitForScripts) { Thread.Sleep(250); } });
            //CancellationToken parserCanellationToken = new CancellationToken();
            /*Task<IHtmlDocument> parseTask = Task.Run(() => { return parser.ParseDocumentAsync(html, parserCanellationToken); });
                
                Thread.Sleep(500);
                while(document.HtmlDocument == null)
            {

            }*/
            //IDocument document = new Document();
            document.HtmlDocument = parser.ParseDocument(html);
            Window.Window window1 = new Window.Window(engine, document);
            //window1.Document = parser.ParseDocument(html);

                window1.InitializeEngine();
                var jquery = System.IO.File.ReadAllText(@"../../../../BrowseSharpPlayground/jquery.js");
                //engine.Execute("window.document.readyState = \"Loading\";");
                engine.Execute(jquery);
                engine.Execute("var $ = window.jQuery;");

                engine.Execute(script);
            waitForScripts = false; ;
                CheckMessage(engine, document, "empty");

                engine.Execute("$('#btn').trigger('click');");
                CheckMessage(engine, document, "hello there you clicked the button 1 times");

            

        }

        [TestMethod]
        public async Task vanillaJsClickTest()
        {
            string script = "var numClicks = 0; function onClickBtn(){numClicks += 1; document.getElementById('message').textContent = \"hello there you clicked the button \" + numClicks + \" times\";}; ";
            string html = @"
<!DOCTYPE html>
<html>
	<head>
		
	</head>
	<body>
		<div id='message'>empty</div>
        <button id='btn' onclick='onClickBtn();'>Click me please</button>
	</body>
</html>";

            //var context = BrowsingContext.New(new Configuration.Default.WithJavaScript());

            Jint.Engine engine = new Jint.Engine();
            IDocument htmlDocument = new Document();

            Window.Window window1 = new Window.Window(engine, htmlDocument);
            //window1.Document = htmlDocument.HtmlDocument;

            window1.InitializeEngine();
            var jquery = System.IO.File.ReadAllText(@"../../../../BrowseSharpPlayground/jquery.js");
            //engine.Execute("window.document.readyState = \"Loading\";");
            engine.Execute(jquery);

            engine.Execute("var $ = window.jQuery;");
            engine.Execute(script);

            CheckMessage(engine, htmlDocument, "empty");

            ((AngleSharp.Html.Dom.IHtmlElement)window1.Document.GetElementById("btn")).DoClick();
            CheckMessage(engine, htmlDocument, "hello there you clicked the button 1 times");


        }

        private void CheckMessage(Jint.Engine engine, IDocument document, string message)
        {
            string messageContentJintJqueryInitial = engine.Execute("$('#message').text();").GetCompletionValue().AsString();
            Assert.AreEqual(message, messageContentJintJqueryInitial);

            string messageContentJintVanillaInitial = engine.Execute("document.getElementById('message').textContent;").GetCompletionValue().AsString();
            Assert.AreEqual(message, messageContentJintVanillaInitial);

            string messageContentAngleSharpInitial = engine.Execute("document.getElementById('message').textContent;").GetCompletionValue().AsString();
            Assert.AreEqual(message, document.HtmlDocument.GetElementById("message").TextContent);

        }

    }
}
