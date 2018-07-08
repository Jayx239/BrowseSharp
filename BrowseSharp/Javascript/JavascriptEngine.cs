using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Jint;
using Jint.Native;
using RestSharp;

namespace BrowseSharp.Javascript
{
    public class JavascriptEngine
    {

        public JavascriptEngine()
        {
            GlobalVariables = InitializeGlobals();
            _scrapeScriptRegex = new Regex("(?<=<script.*[^/]>)([^<].*)(?=</script>)");
            _scrapeScriptSrcRegex = new Regex("(?<=<script.*src=\")[^\"]*(?=\")");
            
        }
           
        public string Document { get; set; }
        public List<string> GlobalVariables { get; set; }
        private Regex _scrapeScriptRegex;
        private Regex _scrapeScriptSrcRegex;

        public Object Execute(string command)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine(getGlobalString());
            stringBuilder.AppendLine(Document);
            stringBuilder.AppendLine(command);
            
            return new Engine().Execute(stringBuilder.ToString()).GetCompletionValue().ToObject();
        }
        
        /* Returns number of script script found added */
        public int AddScripts(Document document)
        {
            string documentString = document.Response.Content;
            List<Javascript> scripts = ScrapeScripts(documentString);
            
            foreach (Javascript script in scripts)
            {
                script.SourceUri = document.Response.ResponseUri;
            }

            List<Javascript> externalScripts = ScrapeScriptSrc(document);
            scripts.AddRange(externalScripts);
            document.Scripts = scripts;
            
            return scripts.Count;

        }

        public List<Javascript> ScrapeScripts(string documentString)
        {
            MatchCollection scriptMatches = _scrapeScriptRegex.Matches(documentString);
            List<Javascript> scripts = new List<Javascript>();
            
            foreach (var scriptMatch in scriptMatches)
            {
                var scriptString = scriptMatch.ToString();
                if(!scriptString.Contains("<script") && !scriptString.Contains("</script")) 
                {
                    Javascript script = new Javascript(scriptString);
                    scripts.Add(script);
                }
            }

            return scripts;
        }
        
        public List<Javascript> ScrapeScriptSrc(Document document)
        {
            string documentString = document.Response.Content;
            MatchCollection scriptMatches = _scrapeScriptSrcRegex.Matches(documentString);
            List<Javascript> scripts = new List<Javascript>();
            
            foreach (var scriptMatch in scriptMatches)
            {
                
                var scriptString = scriptMatch.ToString();
                var scriptUrl = scriptString;
                if (scriptUrl.StartsWith("/"))
                    scriptUrl = document.Response.ResponseUri.Scheme + "://" + document.Response.ResponseUri.Host + scriptUrl;
                
                Uri scriptUri = new Uri(scriptUrl);
                RestClient restClient = new RestClient(document.Response.ResponseUri.Scheme + "://" + scriptUri.Host);
                IRestRequest request = new RestRequest(scriptUri.PathAndQuery, Method.GET);
                IRestResponse response = restClient.Execute(request);
                
                Javascript script = new Javascript(response.Content, scriptUri);
                scripts.Add(script);
            }

            return scripts;
        }
        
        public async Task<List<Javascript>> ScrapeScriptSrcAsync(string documentString)
        {
            MatchCollection scriptMatches = _scrapeScriptSrcRegex.Matches(documentString);
            var requestAsyncHandles = new List<Task<IRestResponse>>();
            List<Javascript> scripts = new List<Javascript>();
            
            foreach (var scriptMatch in scriptMatches)
            {
                RestClient restClient = new RestClient(scriptMatch.ToString());
                var scriptString = scriptMatch.ToString();
                Uri scriptUri = new Uri(scriptString); 
                
                IRestRequest request = new RestRequest(scriptUri, Method.GET);
                var requestAsyncHandle = restClient.ExecuteTaskAsync(request);
                requestAsyncHandles.Add(requestAsyncHandle);
            }

            foreach (var requestAsyncHandle in requestAsyncHandles)
            {
                await requestAsyncHandle;
                string scriptString = requestAsyncHandle.Result.Content;
                Javascript script = new Javascript(scriptString,requestAsyncHandle.Result.ResponseUri);
                scripts.Add(script);
            }

            return scripts;
        }

        private List<string> InitializeGlobals()
        {
            var globals = new List<string>()
            {
                "window = {};",
                "document = {}; "
            };
            
            return globals;
        }

        private string getGlobalString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (var globalVariable in GlobalVariables)
            {
                stringBuilder.AppendLine(globalVariable);
            }

            return stringBuilder.ToString();
        }
        
    }
}