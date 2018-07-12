using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AngleSharp.Dom;
using AngleSharp.Dom.Html;
using AngleSharp.Parser.Html;
using BrowseSharp.Toolbox;
using Jint;
using Jint.Native;
using RestSharp;

namespace BrowseSharp.Javascript
{
    public class JavascriptEngine : IScraper
    {

        public JavascriptEngine()
        {
            GlobalVariables = InitializeGlobals();
        }
           
        public string Document { get; set; }
        public List<string> GlobalVariables { get; set; }

        public Object Execute(string command)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine(GetGlobalString());
            stringBuilder.AppendLine(Document);
            stringBuilder.AppendLine(command);
            
            return new Engine().Execute(stringBuilder.ToString()).GetCompletionValue().ToObject();
        }

        public int Add(IDocument document)
        {
            IHtmlCollection<IHtmlScriptElement> scripts;
            if (document.HtmlDocument != null)
                scripts = ScrapeScripts(document.HtmlDocument);
            else
                scripts = ScrapeScripts(document.Response.Content);
            //ScrapeScriptSrc(document,scripts);
            document.Scripts = ConvertToJavascripts(scripts);
            ScrapeScriptSrc(document);
            
            return scripts.Length;
        }
        
        public async Task<int> AddAsync(IDocument document)
        {
            IHtmlCollection<IHtmlScriptElement> scripts;
            if (document.HtmlDocument != null)
                scripts = ScrapeScripts(document.HtmlDocument);
            else
                scripts = ScrapeScripts(document.Response.Content);
            //ScrapeScriptSrc(document,scripts);
            document.Scripts = ConvertToJavascripts(scripts);
            await ScrapeScriptSrcAsync(document);
            
            return scripts.Length;
        }
        /* Returns number of script added */
        public int AddScripts(Document document)
        {
            return Add(document);
        }

        private IHtmlCollection<IHtmlScriptElement> ScrapeScripts(string documentString)
        {
            HtmlParser parser = new HtmlParser();
            var document = parser.Parse(documentString);
            IHtmlCollection<IHtmlScriptElement> scripts = document.Scripts;
            return scripts;
        }
        
        private IHtmlCollection<IHtmlScriptElement> ScrapeScripts(IHtmlDocument document)
        {
            IHtmlCollection<IHtmlScriptElement> scripts = document.Scripts;
            return scripts;
        }
        
        private int ScrapeScriptSrc(IDocument document)
        {
            List<Javascript> scripts = document.Scripts;
            int numExternalScripts = 0;
            Uri responseUri = document.Response.ResponseUri;
            foreach (var script in scripts.Where(s => s.ScriptElement.Source != null && string.IsNullOrEmpty(s.ScriptElement.Text)))
            {
                Uri scriptUri = UriHelper.GetUri(document.Response.ResponseUri, script.ScriptElement.Source);
                script.SourceUri = scriptUri;
                RestClient restClient = new RestClient(scriptUri.Scheme + "://" + scriptUri.Host);
                IRestRequest request = new RestRequest(scriptUri.PathAndQuery, Method.GET);
                IRestResponse response = restClient.Execute(request);
                script.Content = response.Content;
                numExternalScripts++;
            }

            foreach (var script in scripts.Where(s => s.SourceUri == null))
            {
                script.SourceUri = responseUri;
            }

            return numExternalScripts;
        }
        
        private async Task<int> ScrapeScriptSrcAsync(IDocument document)
        {
            List<Javascript> scripts = document.Scripts;
            int numExternalScripts = 0;
            Uri responseUri = document.Response.ResponseUri;
            var requestAsyncTasks = new List<JavascriptRequestAsyncHandle>();
            foreach (var script in scripts.Where(s => s.ScriptElement.Source != null && string.IsNullOrEmpty(s.ScriptElement.Text)))
            {
                
                Uri scriptUri = UriHelper.GetUri(document.Response.ResponseUri, script.ScriptElement.Source);
                script.SourceUri = scriptUri;
                HttpClient client = new HttpClient();
                Task<HttpResponseMessage> responseMessage = client.GetAsync(scriptUri);
                //RestClient restClient = new RestClient(scriptUri.Scheme + "://" + scriptUri.Host);
                //IRestRequest request = new RestRequest(scriptUri.PathAndQuery, Method.GET);
                //var requestAsyncHandle = restClient.ExecuteTaskAsync(request);
                JavascriptRequestAsyncHandle requestAsynceHanle = new JavascriptRequestAsyncHandle(responseMessage,script);
                requestAsyncTasks.Add(requestAsynceHanle);
            }
            
            foreach (var requestAsyncTask in requestAsyncTasks)
            {
                await requestAsyncTask.ResponseAsyncTask;
                requestAsyncTask.Script.Content = requestAsyncTask.ResponseAsyncTask.Result.Content.ToString();
            }
            
            foreach (var script in scripts.Where(s => s.SourceUri == null))
            {
                script.SourceUri = responseUri;
            }

            return numExternalScripts;
            // end

            /*MatchCollection scriptMatches = _scrapeScriptSrcRegex.Matches(documentString);
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

            return scripts;*/
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

        private string GetGlobalString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (var globalVariable in GlobalVariables)
            {
                stringBuilder.AppendLine(globalVariable);
            }

            return stringBuilder.ToString();
        }
        
        

        private List<Javascript> ConvertToJavascripts(IHtmlCollection<IHtmlScriptElement> scriptElements)
        {
            List<Javascript> scripts = new List<Javascript>();
            foreach (IHtmlScriptElement scriptElement in scriptElements)
            {
                scripts.Add(ConvertToJavascript(scriptElement));
            }

            return scripts;
        }
        
        private Javascript ConvertToJavascript(IHtmlScriptElement scriptElement)
        {
            Javascript javascript = new Javascript(scriptElement);
            return javascript;
        }
        
    }
}