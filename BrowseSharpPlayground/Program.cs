using BrowseSharp;
using BrowseSharp.BOM.Navigator;
using BrowseSharp.BOM.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            Browser browser = new Browser();
            IDocument doc = browser.Navigate("https://browsesharp.org/testsitesjqueryrender.html");
            //doc.HtmlDocument = new AngleSharp.Parser.Html.HtmlParser().Parse(htmlContent);
            Jint.Engine engine = new Jint.Engine();
            engine.SetValue("document", doc.HtmlDocument);
            //engine.SetValue("d", doc.HtmlDocument);
            Navigator navigator = new Navigator(engine);
            Window window1 = new Window(engine);
            window1.document = doc.HtmlDocument;
            engine.SetValue("window.document", doc);
            navigator.InitializeEngine();
            window1.InitializeEngine();
            //engine.SetValue("navigator", navigator);
            
            //engine.SetValue("window", );
            var scripts = "";// "var require = function(asd){};\nvar window = {};\nvar module = new Object();\nvar exports = new Object()\n;";
            int skipFirst = -1;
            //scripts += doc.Scripts[2].JavascriptString + "\n" + doc.Scripts[6].JavascriptString;
            foreach (var script in doc.Scripts)
            {
                if (skipFirst < 0)
                {
                    skipFirst++;
                    continue;
                }

                scripts += script.JavascriptString + "\n";
            }
            var result = engine.Execute(scripts);
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
            window.onappinstalled = (e)=> Console.Write(e);
            Console.WriteLine(window.DevicePixelRatio);
            //engine2.Execute("window.devicePixelRatio = 19;");
            var pixRatio = engine2.Execute("window.devicePixelRatio");
            //engine2.Execute("window.OnAppInstalled = function() {write('hello world');}");
            engine2.Execute("window.onappinstalled('asd');");
            engine2.Execute("window.onappinstalled = function(a) {write('a');};");
            //engine2.Execute("window.onappinstalled('asd');");
            Console.WriteLine(pixRatio);
            Console.WriteLine(window.DevicePixelRatio);
            var windowVal = engine2.Execute("window.myValue = 35;");
            var w = (Window) windowVal.GetValue("window").ToObject();
            engine2.Execute("window.myValue = 'MyValue';");

        }
    }
}
