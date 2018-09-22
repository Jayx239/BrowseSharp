using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AngleSharp.Dom.Html;
using AngleSharp.Parser.Html;
using BrowseSharp.Html;
using RestSharp;

namespace BrowseSharp.Browsers
{
    public class BrowserTyped : Browser, IBrowserTyped
    {
        public new IDocument<T> Back<T>()
        {
            throw new NotImplementedException();
        }

        public new IDocument<T> Back<T>(bool useCache)
        {
            IDocument<T> document = new Document<T>(Back(useCache));
            return document;
            throw new NotImplementedException();
        }

        public new Task<IDocument<T>> BackAsync<T>()
        {
            throw new NotImplementedException();
        }

        public new Task<IDocument<T>> BackAsync<T>(bool useCache)
        {
            throw new NotImplementedException();
        }

        public new IDocument<T> Execute<T>(IRestRequest request)
        {
            IRestResponse<T> response = Deserialize<T>(_restClient.Execute(request));
            IDocument<T> document = PackageAndAddDocument<T>(request, response);
            return document;
            //throw new NotImplementedException();
        }

        public new IDocument<T> ExecuteAsGet<T>(IRestRequest request, string httpMethod)
        {
            throw new NotImplementedException();
        }

        public new IDocument<T> ExecuteAsPost<T>(IRestRequest request, string httpMethod)
        {
            throw new NotImplementedException();
        }

        public new Task<IDocument<T>> ExecuteGetTaskAsync<T>(IRestRequest request)
        {
            throw new NotImplementedException();
        }

        public new Task<IDocument<T>> ExecuteGetTaskAsync<T>(IRestRequest request, CancellationToken token)
        {
            throw new NotImplementedException();
        }

        public new Task<IDocument<T>> ExecutePostTaskAsync<T>(IRestRequest request)
        {
            throw new NotImplementedException();
        }

        public new Task<IDocument<T>> ExecutePostTaskAsync<T>(IRestRequest request, CancellationToken token)
        {
            throw new NotImplementedException();
        }

        public new Task<IDocument<T>> ExecuteTaskAsync<T>(IRestRequest request, CancellationToken token)
        {
            throw new NotImplementedException();
        }

        public new Task<IDocument<T>> ExecuteTaskAsync<T>(IRestRequest request)
        {
            throw new NotImplementedException();
        }

        public new IDocument<T> Forward<T>()
        {
            throw new NotImplementedException();
        }

        public new IDocument<T> Forward<T>(bool useCache)
        {
            throw new NotImplementedException();
        }

        public new Task<IDocument<T>> ForwardAsync<T>()
        {
            throw new NotImplementedException();
        }

        public new Task<IDocument<T>> ForwardAsync<T>(bool useCache)
        {
            throw new NotImplementedException();
        }

        public IDocument<T> GetDocumentTyped<T>()
        {
            throw new NotImplementedException();
        }

        public new IDocument<T> Navigate<T>(string uri)
        {
            throw new NotImplementedException();
        }

        public new IDocument<T> Navigate<T>(Uri uri)
        {
            throw new NotImplementedException();
        }

        public new IDocument<T> Navigate<T>(string uri, Dictionary<string, string> headers)
        {
            throw new NotImplementedException();
        }

        public new IDocument<T> Navigate<T>(Uri uri, Dictionary<string, string> headers)
        {
            throw new NotImplementedException();
        }

        public new IDocument<T> Navigate<T>(string uri, Dictionary<string, string> headers, Dictionary<string, string> formData)
        {
            throw new NotImplementedException();
        }

        public new IDocument<T> Navigate<T>(Uri uri, Dictionary<string, string> headers, Dictionary<string, string> formData)
        {
            throw new NotImplementedException();
        }

        public new Task<IDocument<T>> NavigateAsync<T>(string uri)
        {
            throw new NotImplementedException();
        }

        public new Task<IDocument<T>> NavigateAsync<T>(Uri uri)
        {
            throw new NotImplementedException();
        }

        public new Task<IDocument<T>> NavigateAsync<T>(string uri, Dictionary<string, string> headers)
        {
            throw new NotImplementedException();
        }

        public new Task<IDocument<T>> NavigateAsync<T>(Uri uri, Dictionary<string, string> headers)
        {
            throw new NotImplementedException();
        }

        public new Task<IDocument<T>> NavigateAsync<T>(string uri, Dictionary<string, string> headers, Dictionary<string, string> formData)
        {
            throw new NotImplementedException();
        }

        public new Task<IDocument<T>> NavigateAsync<T>(Uri uri, Dictionary<string, string> headers, Dictionary<string, string> formData)
        {
            throw new NotImplementedException();
        }

        public new IDocument<T> Refresh<T>()
        {
            throw new NotImplementedException();
        }

        public new Task<IDocument<T>> RefreshAsync<T>()
        {
            throw new NotImplementedException();
        }

        public new IDocument<T> Submit<T>(string uri)
        {
            throw new NotImplementedException();
        }

        public new IDocument<T> Submit<T>(Uri uri)
        {
            throw new NotImplementedException();
        }

        public new IDocument<T> Submit<T>(string uri, Dictionary<string, string> formData)
        {
            throw new NotImplementedException();
        }

        public new IDocument<T> Submit<T>(Uri uri, Dictionary<string, string> formData)
        {
            throw new NotImplementedException();
        }

        public new IDocument<T> Submit<T>(string uri, Dictionary<string, string> formData, Dictionary<string, string> headers)
        {
            throw new NotImplementedException();
        }

        public new IDocument<T> Submit<T>(Uri uri, Dictionary<string, string> formData, Dictionary<string, string> headers)
        {
            throw new NotImplementedException();
        }

        public new Task<IDocument<T>> SubmitAsync<T>(string uri)
        {
            throw new NotImplementedException();
        }

        public new Task<IDocument<T>> SubmitAsync<T>(Uri uri)
        {
            throw new NotImplementedException();
        }

        public new Task<IDocument<T>> SubmitAsync<T>(string uri, Dictionary<string, string> formData)
        {
            throw new NotImplementedException();
        }

        public new Task<IDocument<T>> SubmitAsync<T>(Uri uri, Dictionary<string, string> formData)
        {
            throw new NotImplementedException();
        }

        public new Task<IDocument<T>> SubmitAsync<T>(string uri, Dictionary<string, string> formData, Dictionary<string, string> headers)
        {
            throw new NotImplementedException();
        }

        public new Task<IDocument<T>> SubmitAsync<T>(Uri uri, Dictionary<string, string> formData, Dictionary<string, string> headers)
        {
            throw new NotImplementedException();
        }

        public new IDocument<T> SubmitForm<T>(Form form)
        {
            throw new NotImplementedException();
        }

        public new IDocument<T> SubmitForm<T>(Form form, Dictionary<string, string> Headers)
        {
            throw new NotImplementedException();
        }

        public new Task<IDocument<T>> SubmitFormAsync<T>(Form form)
        {
            throw new NotImplementedException();
        }

        public new Task<IDocument<T>> SubmitFormAsync<T>(Form form, Dictionary<string, string> headers)
        {
            throw new NotImplementedException();
        }

        protected IDocument<T> PackageAndAddDocument<T>(IRestRequest request, IRestResponse<T> response)
        {
            Uri requestUri = _restClient.BaseUrl;
            HtmlParser parser = new HtmlParser();
            IHtmlDocument htmlDocument = parser.Parse(response.Content);
            IDocument<T> document = new Document<T>(request, response, htmlDocument);
            if (JavascriptScrapingEnabled)
                JavascriptEngine.Add(document);
            if (StyleScrapingEnabled)
                StyleEngine.Add(document);
            document.RequestUri = requestUri;
            Documents.Add(document);
            return document;
        }

        protected async Task<IDocument<T>> PackageAndAddDocumentAsync<T>(IRestRequest request, Task<IRestResponse<T>> responseTask)
        {
            Uri requestUri = _restClient.BaseUrl;
            IRestResponse<T> response = await responseTask;
            HtmlParser parser = new HtmlParser();
            IHtmlDocument htmlDocument = parser.Parse(response.Content);
            IDocument<T> document = new Document<T>(request, response, htmlDocument);

            Task<int> result = null;
            Task<int> styleResult = null;
            if (JavascriptScrapingEnabled)
                result = JavascriptEngine.AddAsync(document);
            if (StyleScrapingEnabled)
                styleResult = StyleEngine.AddAsync(document);

            if (JavascriptScrapingEnabled && result != null)
                await result;
            if (StyleScrapingEnabled && styleResult != null)
                await styleResult;
            document.RequestUri = requestUri;
            Documents.Add(document);
            return document;
        }
    }
}
