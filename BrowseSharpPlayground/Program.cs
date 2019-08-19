using BrowseSharp;
using BrowseSharp.Common;
using BrowseSharp.BOM.Navigator;
using BrowseSharp.BOM.Window;
using BrowseSharp.Common.Javascript;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AngleSharp.Html;
using AngleSharp.Html.Dom;
using BrowseSharp.Scripting;



namespace BrowseSharpPlayground
{
    class Program
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
        static void Main(string[] args)
        {
            //Console.WriteLine(Say.somethingToDo());
            //Say.doSomethingIn(() => { return 10; });
            //Console.WriteLine(Say.somethingToDo());

            IDocument htmlDocument = new Document();
            htmlDocument.HtmlDocument = new AngleSharp.Html.Parser.HtmlParser().ParseDocument(htmlContent);
            //var script1 = new Javascript();
            string script1 = "document.getElementById('content').textContent = 'this is the content';";
            //htmlDocument.Scripts.Add(script1);

            Jint.Engine engine = new Jint.Engine();
            engine.SetValue("document",htmlDocument.HtmlDocument);
            engine.Execute(script1);
            Browser browser = new Browser();
            IDocument doc = browser.Navigate("https://browsesharp.org/testsitesjqueryrender.html");
            //doc.HtmlDocument = new AngleSharp.Html.Parser.HtmlParser().ParseDocument(htmlContent);
            //Jint.Engine engine = new Jint.Engine();
            //engine.SetValue("document", doc.HtmlDocument);
            //engine.SetValue("d", doc.HtmlDocument);
            //Navigator navigator = new Navigator(engine);
            Window window1 = new Window(engine);
            window1.document = doc.HtmlDocument;
            //engine.Execute("Window = {};");
            //engine.SetValue("window.document", doc.HtmlDocument);
            //engine.SetValue("window.document", doc);

            //navigator.InitializeEngine();
            window1.InitializeEngine();
            Action<string> console = (val) => { Console.WriteLine(val); };
            engine.SetValue("write", console);
            //engine.SetValue("navigator", navigator);
            
            //engine.SetValue("window", );
            var scripts = "";// "var require = function(asd){};\nvar window = {};\nvar module = new Object();\nvar exports = new Object()\n;";
            int skipFirst = -3;
            //engine.SetValue("console.log", new Action<object>(Console.WriteLine));
            

           

            var jquery = System.IO.File.ReadAllText(@"../../jquery.js");
            //engine.Execute(windowJs);
            engine.Execute("window.test = 'test';");
            engine.Execute("var noGlobal = false; ");
            dynamic window1d = window1;
            try { 
            engine.Execute(jquery + "\nvar document = window.document;\nvar $ = window.jQuery;\n$(document).ready(function(){\n$('h3.mt-4').text('hello there');\n});\n var document = window.document;\n$(document).ready() ");
            }
            catch(Exception ex)
            {
                Console.Write(String.Format("Execption: {0}", ex.StackTrace));
            }

            engine.Execute("window.jQuery.ready();");

            //engine.Execute("document.ready()");
            engine.Execute("(function($, window, document){$('#Area1').text('hello there');})(window.jQuery, window, window.document);");

            string newValue = engine.Execute("$('#Area1').text()").GetCompletionValue().ToString(); // This is the value set in the previous line with jquery
            string newValueFromDOM = window1.document.GetElementById("Area1").TextContent; // Get new value from DOM

            //engine.Execute("window.jQuery = jQuery;");
            engine.Execute("var $ = window.jQuery()('");
            scripts += doc.Scripts[6].JavascriptString;
            foreach (var script in doc.Scripts)
            {
                if (script.Content.Contains("jQuery"))
                {
                    skipFirst++;
                    continue;
                }
                //var result = engine.Execute(script.Content);
                //scripts += script.JavascriptString + "\n";
            }

            try { 
            var result = engine.Execute(scripts);
            } catch(Exception ex)
            {
                Console.Write("");
            }
            string[] vals = { "div" };
            Console.Write("!");
            
            /*Jint.Engine engine = new Jint.Engine();
            engine.SetValue("console", new Action<object>(Console.WriteLine));
            var consoleLog = engine.GetValue("console");
            var res = engine.Execute("function executableFunction() {return otherFunction;}\n function otherFunction() { return true;};");
            
            Console.WriteLine(res.ToString());
            
            Jint.Native.JsValue executableFunction = res.GetValue("executableFunction");
            res.SetValue("executableFunction","wer");
            var newExecutableFunction = res.GetValue("executableFunction");
            var MyValue = "My value is nigh";
            engine.SetValue("MyValue", MyValue);
            Console.WriteLine(MyValue);
            engine.Execute("MyValue = 'Something different';");
            Console.WriteLine(MyValue);
            */

            //More stuff

            
            Jint.Engine engine2 = new Jint.Engine();
            Window window = new Window(engine2);
            window.InitializeEngine();
            //engine2.SetValue("write", new Action<string>(Console.Write));
            //engine2.SetValue("window", window);
            window.onappinstalled = Console.Write;
            window.onappinstalled("on app installed works");
            
            Console.WriteLine(window.DevicePixelRatio);
            //engine2.Execute("window.devicePixelRatio = 19;");
            var pixRatio = engine2.Execute("window.devicePixelRatio");
            //engine2.Execute("window.OnAppInstalled = function() {write('hello world');}");
            engine2.Execute("window.onappinstalled('asd');");
            engine2.Execute("window.onappinstalled = window.LogArray");

            //engine2.Execute("window.onappinstalled('asd');");
            Console.WriteLine(pixRatio);
            Console.WriteLine(window.DevicePixelRatio);
            var windowVal = engine2.Execute("window.myValue = 35;");
            var w = (Window) windowVal.GetValue("window").ToObject();
            engine2.Execute("window.myValue = 'MyValue';");

        }
    }

}
