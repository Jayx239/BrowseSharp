using Jint;
using Jint.Parser;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace BrowseSharp.Javascript
{
    public class JavascriptEvalEngine
    {
        public JavascriptEvalEngine():base()
        {

        }
        int index = 0;
        public  Object EvaluateScript(IDocument document)
        {
            index = 0;
            Engine engine = new Engine();
            engine.SetValue("document", document.HtmlDocument);
            var scripts = "";
            //var variables = new Dictionary<string, string>();
            foreach (var script in document.Scripts)
            {
                var scriptContent = script.Content;
                if (script.Content.Contains("<")) {
                    var variables = ReplaceDomStrings(script.Content,out scriptContent);
                    foreach (var variable in variables)
                    {
                        engine.SetValue(variable.Key, variable.Value);
                    }
                }
                scripts += scriptContent + "\n";
            }
            

            ParserOptions options = new ParserOptions();
            options.Tolerant = true;
            return engine.Execute(scripts, options).GetCompletionValue().ToObject();
        }
        private Dictionary<string,string> ReplaceDomStrings(string script, out string outScript)
        {
            outScript = script;
            var matches = GetDomStrings(script);
            
            Dictionary<string, string> variables = new Dictionary<string, string>();
            foreach(var match in matches)
            { var variableKey = "_scriptVar" + index++;
                variables.Add(variableKey, match.ToString());
                outScript = outScript.Replace(match.ToString(), variableKey);
            }
            return variables;
        }

        private MatchCollection GetDomStrings(string script)
        {
            Regex elementsRegex = new Regex("(\"<.*/>\")|(\"<.*>.*</.*>\")");
            return elementsRegex.Matches(script);
        }
    }
}
