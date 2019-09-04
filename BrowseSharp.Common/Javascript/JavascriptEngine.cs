using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using AngleSharp.Html.Parser;
using BrowseSharp.Common.Toolbox;
using Jint;
using RestSharp;

namespace BrowseSharp.Common.Javascript
{
    /// <summary>
    /// Javascript scraper and executor
    /// </summary>
    public class JavascriptEngine : IScraper
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public JavascriptEngine()
        {
            GlobalVariables = InitializeGlobals();
        }
        
        /// <summary>
        /// String containing scripts to be executed
        /// </summary>
        public string Document { get; set; }
        
        /// <summary>
        /// Global variables that may be used in script execution 
        /// </summary>
        public List<string> GlobalVariables { get; set; }

        /// <summary>
        /// Method for executing javascript command, uses document
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public Object Execute(string command)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine(GetGlobalString());
            stringBuilder.AppendLine(Document);
            stringBuilder.AppendLine(command);
            
            return new Engine().Execute(stringBuilder.ToString()).GetCompletionValue().ToObject();
        }

        /// <summary>
        /// Adds scripts to document
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        public int Add(IDocument document)
        {
            IHtmlCollection<IHtmlScriptElement> scripts;
            if (document.HtmlDocument != null)
                scripts = ScrapeScripts(document.HtmlDocument);
            else
                scripts = ScrapeScripts(document.Response.Content);
            
            document.Scripts = ConvertToJavascripts(scripts);
            ScrapeScriptSrc(document);
            
            return scripts.Length;
        }
        
        /// <summary>
        /// Adds scripts to document asynchronously
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        public async Task<int> AddAsync(IDocument document)
        {
            IHtmlCollection<IHtmlScriptElement> scripts;
            if (document.HtmlDocument != null)
                scripts = ScrapeScripts(document.HtmlDocument);
            else
                scripts = ScrapeScripts(document.Response.Content);
            
            document.Scripts = ConvertToJavascripts(scripts);
            await ScrapeScriptSrcAsync(document);
            
            return scripts.Length;
        }
        
        /// <summary>
        /// Same as Add method
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        public int AddScripts(IDocument document)
        {
            return Add(document);
        }

        /// <summary>
        /// Internal method for parsing scripts
        /// </summary>
        /// <param name="documentString"></param>
        /// <returns></returns>
        private IHtmlCollection<IHtmlScriptElement> ScrapeScripts(string documentString)
        {
            HtmlParser parser = new HtmlParser();
            var document = parser.ParseDocument(documentString);
            IHtmlCollection<IHtmlScriptElement> scripts = document.Scripts;
            return scripts;
        }
        
        /// <summary>
        /// Internal method for parsing scripts
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        private IHtmlCollection<IHtmlScriptElement> ScrapeScripts(IHtmlDocument document)
        {
            IHtmlCollection<IHtmlScriptElement> scripts = document.Scripts;
            return scripts;
        }
        
        /// <summary>
        /// Internal method for scraping external scripts
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
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
        
        /// <summary>
        /// Internal method for scraping external scripts asynchronously
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
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
        }

        /// <summary>
        /// Method for initializing globals with default js variables
        /// </summary>
        /// <returns></returns>
        private List<string> InitializeGlobals()
        {
            var globals = new List<string>()
            {
                "var window = {};",
                "var document = {}; "
            };
            
            return globals;
        }

        /// <summary>
        /// Gets global variable collection as a string
        /// </summary>
        /// <returns></returns>
        private string GetGlobalString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (var globalVariable in GlobalVariables)
            {
                stringBuilder.AppendLine(globalVariable);
            }

            return stringBuilder.ToString();
        }

        /// <summary>
        /// Converts AngleSharp Scripts to BrowseSharp Javascripts
        /// </summary>
        /// <param name="scriptElements"></param>
        /// <returns></returns>
        private List<Javascript> ConvertToJavascripts(IHtmlCollection<IHtmlScriptElement> scriptElements)
        {
            List<Javascript> scripts = new List<Javascript>();
            foreach (IHtmlScriptElement scriptElement in scriptElements)
            {
                scripts.Add(ConvertToJavascript(scriptElement));
            }

            return scripts;
        }
        
        /// <summary>
        /// Converts AngleSharp Script to BrowseSharp Javascript
        /// </summary>
        /// <param name="scriptElement"></param>
        /// <returns></returns>
        private Javascript ConvertToJavascript(IHtmlScriptElement scriptElement)
        {
            Javascript javascript = new Javascript(scriptElement);
            return javascript;
        }
    }
}