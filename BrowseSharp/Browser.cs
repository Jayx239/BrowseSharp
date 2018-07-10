using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AngleSharp.Dom.Html;
using AngleSharp.Parser.Html;
using BrowseSharp.Javascript;
using BrowseSharp.Style;
using Jint.Parser;
using RestSharp;
using RestSharp.Authenticators;
using RestSharp.Deserializers;

namespace BrowseSharp
{
    public class Browser: RestClient
    {
        public Browser() : base()
        {
            Documents = new List<Document>();
            JavascriptEngine = new JavascriptEngine();
            StyleEngine = new StyleEngine();
        }
        
        public List<Document> Documents { get; }
        public JavascriptEngine JavascriptEngine { get; }
        public StyleEngine StyleEngine { get; }

        public override RestRequestAsyncHandle ExecuteAsync(IRestRequest request, Action<IRestResponse, RestRequestAsyncHandle> callback)
        {
            return ExecuteAsync(request, callback);
        }

        public override RestRequestAsyncHandle ExecuteAsync<T>(IRestRequest request, Action<IRestResponse<T>, RestRequestAsyncHandle> callback)
        {
            return ExecuteAsync(request, callback);
        }

        public override IRestResponse Execute(IRestRequest request)
        {
            IRestResponse response = base.Execute(request);
            Documents.Add(PackageDocument(request,response));
            return response;
        }
        
        public override IRestResponse<T> Execute<T>(IRestRequest request)
        {
            IRestResponse<T> response = base.Execute<T>(request);
            Documents.Add(PackageDocument(request,response));
            return response;
        }

        public override RestRequestAsyncHandle ExecuteAsyncGet<T>(IRestRequest request, Action<IRestResponse<T>, RestRequestAsyncHandle> callback, string httpMethod)
        {
            return base.ExecuteAsyncGet(request, callback, httpMethod);
        }

        public override RestRequestAsyncHandle ExecuteAsyncPost<T>(IRestRequest request, Action<IRestResponse<T>, RestRequestAsyncHandle> callback, string httpMethod)
        {
            return base.ExecuteAsyncPost(request, callback, httpMethod);
        }

        public override Task<IRestResponse<T>> ExecuteTaskAsync<T>(IRestRequest request)
        {
            return base.ExecuteTaskAsync<T>(request);
        }

        public override Task<IRestResponse<T>> ExecuteTaskAsync<T>(IRestRequest request, CancellationToken token)
        {
            return base.ExecuteTaskAsync<T>(request, token);
        }

        public override Task<IRestResponse> ExecuteTaskAsync(IRestRequest request, CancellationToken token)
        {
            return base.ExecuteTaskAsync(request, token);
        }

        public override Task<IRestResponse> ExecuteTaskAsync(IRestRequest request)
        {
            return base.ExecuteTaskAsync(request);
        }

        public override Task<IRestResponse> ExecuteGetTaskAsync(IRestRequest request)
        {
            return base.ExecuteGetTaskAsync(request);
        }

        public override Task<IRestResponse> ExecuteGetTaskAsync(IRestRequest request, CancellationToken token)
        {
            return base.ExecuteGetTaskAsync(request, token);
        }

        public override Task<IRestResponse> ExecutePostTaskAsync(IRestRequest request)
        {
            return base.ExecutePostTaskAsync(request);
        }

        public override Task<IRestResponse> ExecutePostTaskAsync(IRestRequest request, CancellationToken token)
        {
            return base.ExecutePostTaskAsync(request, token);
        }

        public override Task<IRestResponse<T>> ExecuteGetTaskAsync<T>(IRestRequest request)
        {
            return base.ExecuteGetTaskAsync<T>(request);
        }

        public override Task<IRestResponse<T>> ExecuteGetTaskAsync<T>(IRestRequest request, CancellationToken token)
        {
            return base.ExecuteGetTaskAsync<T>(request, token);
        }

        public override Task<IRestResponse<T>> ExecutePostTaskAsync<T>(IRestRequest request)
        {
            return base.ExecutePostTaskAsync<T>(request);
        }

        public override Task<IRestResponse<T>> ExecutePostTaskAsync<T>(IRestRequest request, CancellationToken token)
        {
            return base.ExecutePostTaskAsync<T>(request, token);
        }

        public override RestRequestAsyncHandle ExecuteAsyncGet(IRestRequest request, Action<IRestResponse, RestRequestAsyncHandle> callback, string httpMethod)
        {
            return base.ExecuteAsyncGet(request, callback, httpMethod);
        }

        public override RestRequestAsyncHandle ExecuteAsyncPost(IRestRequest request, Action<IRestResponse, RestRequestAsyncHandle> callback, string httpMethod)
        {
            return base.ExecuteAsyncPost(request, callback, httpMethod);
        }
        
        private Document PackageDocument(IRestRequest request, IRestResponse response)
        {
            HtmlParser parser = new HtmlParser();
            IHtmlDocument htmlDocument = parser.Parse(response.Content);
            Document document = new Document(request,response, htmlDocument);
            //JavascriptEngine.UpdateExternalScripts(document);
            JavascriptEngine.AddScripts(document);
            StyleEngine.AddStyles(document);
            //StyleEngine.AddStyles(document);
            return document;
        }
        
        private Document PackageDocumentAsync(IRestRequest request, IRestResponse response)
        {
            HtmlParser parser = new HtmlParser();
            IHtmlDocument htmlDocument = parser.Parse(response.Content);
            Document document = new Document(request,response, htmlDocument);
            //JavascriptEngine.UpdateExternalScripts(document);
            JavascriptEngine.AddScripts(document);
            StyleEngine.AddStyles(document);
            //StyleEngine.AddStyles(document);
            return document;
        }
    }
}
