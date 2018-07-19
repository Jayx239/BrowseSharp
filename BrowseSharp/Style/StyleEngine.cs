using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using BrowseSharp.Toolbox;
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

        /* Returns number of script script found added */
        public int Add(IDocument document)
        {
            string documentString = document.Response.Content;
            List<StyleSheet> styles = ScrapeStyles(documentString);
            foreach (StyleSheet style in styles)
            {
                style.SourceUri = document.Response.ResponseUri;
            }
            List<StyleSheet> externalStyles = ScrapeStylesSrc(document);
            styles.AddRange(externalStyles);
            document.Styles = styles;
            
            return styles.Count;
        }

        public async Task<int> AddAsync(IDocument document)
        {
            var styles = await ScrapeStylesSrcAsync(document);
            document.Styles.AddRange(styles);
            return document.Styles.Count;
        }
        

        private List<StyleSheet> ScrapeStyles(string documentString)
        {
            MatchCollection styleMatches = _scrapeStyleRegex.Matches(documentString);
            List<StyleSheet> styles = new List<StyleSheet>();
            
            foreach (var styleMatch in styleMatches)
            {
                var styleString = styleMatch.ToString();
                StyleSheet style = new StyleSheet(styleString);
                styles.Add(style);
            }

            return styles;
        }
        
        private List<StyleSheet> ScrapeStylesSrc(IDocument document)
        {
            string documentString = document.Response.Content;
            MatchCollection styleMatches = _scrapeStyleSrcRegex.Matches(documentString);
            List<StyleSheet> styles = new List<StyleSheet>();
            
            foreach (var styleMatch in styleMatches)
            {
                string styleUrl = styleMatch.ToString();
                Uri styleUri = UriHelper.GetUri(document.Response.ResponseUri,styleUrl);
                RestClient restClient = new RestClient(document.Response.ResponseUri.Scheme + "://" + styleUri.Host);
                IRestRequest request = new RestRequest(styleUri.PathAndQuery, Method.GET);
                IRestResponse response = restClient.Execute(request);
                StyleSheet style = new StyleSheet(response.Content, styleUri);
                styles.Add(style);
            }

            return styles;
        }
        
        private async Task<List<StyleSheet>> ScrapeStylesSrcAsync(IDocument document)
        {
            string documentString = document.Response.Content;
            MatchCollection styleMatches = _scrapeStyleSrcRegex.Matches(documentString);
            var requestAsyncHandles = new List<StyleSheetRequestAsyncHandle>();
            List<StyleSheet> scripts = new List<StyleSheet>();
            
            foreach (var styleMatch in styleMatches)
            {
                string styleUrl = styleMatch.ToString();
                Uri styleUri = UriHelper.GetUri(document.Response.ResponseUri,styleUrl);
                HttpClient client = new HttpClient();
                StyleSheetRequestAsyncHandle requestAsyncHandle = new StyleSheetRequestAsyncHandle(client.GetAsync(styleUri),new StyleSheet(styleUri));
                requestAsyncHandles.Add(requestAsyncHandle);
            }

            foreach (var requestAsyncTask in requestAsyncHandles)
            {
                await requestAsyncTask.ResponseAsyncTask;
                requestAsyncTask.StyleSheet.Content = requestAsyncTask.ResponseAsyncTask.Result.Content.ToString();
                scripts.Add(requestAsyncTask.StyleSheet);
            }

            return scripts;
        }
    }
}