using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using BrowseSharp.Toolbox;
using Jint;
using RestSharp;

namespace BrowseSharp.Style
{
    public class StyleEngine : IScraper
    {

        public StyleEngine()
        {
            _scrapeStyleRegex = new Regex("(?<=<style.*>).*(?=</style>)");
            _scrapeStyleSrcRegex = new Regex("((?<=<link.*(rel=\"stylesheet\".*href=\")).*(?=\".*>)|(?<=<link.*href=\").*(?=\".*rel=\"stylesheet\".*>))");
        }
        
        private Regex _scrapeStyleRegex;
        private Regex _scrapeStyleSrcRegex;

        public int Add(Document document)
        {
            string documentString = document.Response.Content;
            List<Style> styles = ScrapeStyles(documentString);
            
            foreach (Style style in styles)
            {
                style.SourceUri = document.Response.ResponseUri;
            }

            List<Style> externalScripts = ScrapeStylesSrc(document);
            styles.AddRange(externalScripts);
            document.Styles = styles;
            
            return styles.Count;
        }

        public Task<int> AddAsync(Document document)
        {
            throw new NotImplementedException();
        }
        /* Returns number of script script found added */
        public int AddStyles(Document document)
        {
            return Add(document);
        }

        private List<Style> ScrapeStyles(string documentString)
        {
            MatchCollection styleMatches = _scrapeStyleRegex.Matches(documentString);
            List<Style> styles = new List<Style>();
            
            foreach (var styleMatch in styleMatches)
            {
                var styleString = styleMatch.ToString();
                Style style = new Style(styleString);
                styles.Add(style);
            }

            return styles;
        }
        
        private List<Style> ScrapeStylesSrc(Document document)
        {
            string documentString = document.Response.Content;
            MatchCollection styleMatches = _scrapeStyleSrcRegex.Matches(documentString);
            List<Style> styles = new List<Style>();
            
            foreach (var styleMatch in styleMatches)
            {
                
                var scriptString = styleMatch.ToString();
                var styleUrl = scriptString;
                /*if (!styleUrl.ToLower().StartsWith("http") && !styleUrl.ToLower().StartsWith("www."))
                {
                    if (!styleUrl.StartsWith("/"))
                        styleUrl = "/" + styleUrl;
                    styleUrl = document.Response.ResponseUri.Scheme + "://" + document.Response.ResponseUri.Host + styleUrl;
                }
                */
                Uri styleUri = UriHelper.GetUri(document.Response.ResponseUri,styleUrl);// = new Uri(styleUrl);
                RestClient restClient = new RestClient(document.Response.ResponseUri.Scheme + "://" + styleUri.Host);
                IRestRequest request = new RestRequest(styleUri.PathAndQuery, Method.GET);
                IRestResponse response = restClient.Execute(request);
                
                Style style = new Style(response.Content, styleUri);
                styles.Add(style);
            }

            return styles;
        }
        
        private async Task<List<Style>> ScrapeStylesSrcAsync(string documentString)
        {
            MatchCollection styleMatches = _scrapeStyleSrcRegex.Matches(documentString);
            var requestAsyncHandles = new List<Task<IRestResponse>>();
            List<Style> scripts = new List<Style>();
            
            foreach (var styleMatch in styleMatches)
            {
                RestClient restClient = new RestClient(styleMatch.ToString());
                var styleString = styleMatch.ToString();
                Uri styleUri = new Uri(styleString); 
                
                IRestRequest request = new RestRequest(styleUri, Method.GET);
                var requestAsyncHandle = restClient.ExecuteTaskAsync(request);
                requestAsyncHandles.Add(requestAsyncHandle);
            }

            foreach (var requestAsyncHandle in requestAsyncHandles)
            {
                await requestAsyncHandle;
                string styleString = requestAsyncHandle.Result.Content;
                Style style = new Style(styleString,requestAsyncHandle.Result.ResponseUri);
                scripts.Add(style);
            }

            return scripts;
        }
    }
}