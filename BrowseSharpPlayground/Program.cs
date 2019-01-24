using BrowseSharp;
using BrowseSharp.Scripting.Navigator;
using BrowseSharp.Scripting.Window;
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
            IDocument doc = browser.Navigate("http://www.espn.com/");
            //doc.HtmlDocument = new AngleSharp.Parser.Html.HtmlParser().Parse(htmlContent);
            Jint.Engine engine = new Jint.Engine();
            engine.SetValue("document", doc.HtmlDocument);
            //engine.SetValue("d", doc.HtmlDocument);
            Navigator navigator = new Navigator();
            engine.SetValue("navigator", navigator);
            engine.SetValue("window", new Window(doc));
            var scripts = "";// "var require = function(asd){};\nvar window = {};\nvar module = new Object();\nvar exports = new Object()\n;";
            int skipFirst = 0;
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
        }
    }
}
