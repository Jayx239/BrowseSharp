using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using AngleSharp.Dom.Html;
using AngleSharp.Parser.Html;
using BrowseSharp.History;
using BrowseSharp.Html;
using BrowseSharp.Javascript;
using BrowseSharp.Style;
using BrowseSharp.Toolbox;
using RestSharp;

namespace BrowseSharp.Browsers.Core
{
    public class TypedCore : BrowserCore, IBrowserTyped
    {
        /// <summary>
        /// Default Constructor
        /// </summary>
        public TypedCore() : base()
        {

        }
        public TypedCore(JavascriptEngine javascriptEngine, StyleEngine styleEngine,
                RestClient restClient, CookieContainer cookieContainer,
                HistoryManager historyManager, bool styleScrapingEnabled,
                bool javascriptScrapingEnabled, string defaultUriProtocol)
        {
            _javascriptEngine = javascriptEngine;
            _styleEngine = styleEngine;
            _restClient = restClient;
            _restClient.CookieContainer = cookieContainer;
            _history = historyManager;
            StyleScrapingEnabled = styleScrapingEnabled;
            JavascriptScrapingEnabled = javascriptScrapingEnabled;
            DefaultUriProtocol = defaultUriProtocol;
        }

        public IDocument<T> Execute<T>(IRestRequest request)
        {
            IRestResponse<T> response = Deserialize<T>(_restClient.Execute(request));
            IDocument<T> document = PackageAndAddDocument<T>(request, response);
            return document;
        }

        public IDocument<T> ExecuteAsGet<T>(IRestRequest request, string httpMethod)
        {
            IRestResponse<T> response = Deserialize<T>(_restClient.ExecuteAsGet(request, httpMethod));
            IDocument<T> document = PackageAndAddDocument<T>(request, response);
            return document;
        }

        public IDocument<T> ExecuteAsPost<T>(IRestRequest request, string httpMethod)
        {
            IRestResponse<T> response = Deserialize<T>(_restClient.ExecuteAsPost(request, httpMethod));
            IDocument<T> document = PackageAndAddDocument<T>(request, response);
            return document;
        }

        public async Task<IDocument<T>> ExecuteGetTaskAsync<T>(IRestRequest request)
        {
            Task<IRestResponse<T>> responseTask = _restClient.ExecuteGetTaskAsync<T>(request);
            Task<IDocument<T>> documentTask = PackageAndAddDocumentAsync(request, responseTask);
            return await documentTask;
        }

        public async Task<IDocument<T>> ExecuteGetTaskAsync<T>(IRestRequest request, CancellationToken token)
        {
            Task<IRestResponse<T>> responseTask = _restClient.ExecuteGetTaskAsync<T>(request,token);
            Task<IDocument<T>> documentTask = PackageAndAddDocumentAsync(request, responseTask);
            return await documentTask;
        }

        public async Task<IDocument<T>> ExecutePostTaskAsync<T>(IRestRequest request)
        {
            Task<IRestResponse<T>> responseTask = _restClient.ExecutePostTaskAsync<T>(request);
            Task<IDocument<T>> documentTask = PackageAndAddDocumentAsync(request, responseTask);
            return await documentTask;
        }

        public async Task<IDocument<T>> ExecutePostTaskAsync<T>(IRestRequest request, CancellationToken token)
        {
            Task<IRestResponse<T>> responseTask = _restClient.ExecutePostTaskAsync<T>(request, token);
            Task<IDocument<T>> documentTask = PackageAndAddDocumentAsync(request, responseTask);
            return await documentTask;
        }

        public async Task<IDocument<T>> ExecuteTaskAsync<T>(IRestRequest request, CancellationToken token)
        {
            Task<IRestResponse<T>> responseTask = _restClient.ExecuteTaskAsync<T>(request, token);
            Task<IDocument<T>> documentTask = PackageAndAddDocumentAsync(request, responseTask);
            return await documentTask;
        }

        public async Task<IDocument<T>> ExecuteTaskAsync<T>(IRestRequest request)
        {

            Task<IRestResponse<T>> responseTask = _restClient.ExecuteTaskAsync<T>(request);
            Task<IDocument<T>> documentTask = PackageAndAddDocumentAsync(request, responseTask);
            return await documentTask;
        }

        public IDocument<T> Navigate<T>(string uri)
        {
            Uri requestUri = UriHelper.Uri(uri, DefaultUriProtocol);
            return Navigate<T>(requestUri);
        }

        public IDocument<T> Navigate<T>(Uri uri)
        {
            _history.Navigate();
            _restClient.BaseUrl = uri;
            RestRequest request = new RestRequest();
            return Execute<T>(request);
        }

        public IDocument<T> Navigate<T>(string uri, Dictionary<string, string> headers)
        {
            Uri requestUri = UriHelper.Uri(uri, DefaultUriProtocol);
            return Navigate<T>(requestUri, headers);
        }

        public IDocument<T> Navigate<T>(Uri uri, Dictionary<string, string> headers)
        {
            _history.Navigate();
            _restClient.BaseUrl = uri;
            RestRequest request = new RestRequest();
            AddHeaders(request, headers);
            return ExecuteAsGet<T>(request, "GET");
        }

        public IDocument<T> Navigate<T>(string uri, Dictionary<string, string> headers, Dictionary<string, string> formData)
        {
            Uri requestUri = UriHelper.Uri(uri, DefaultUriProtocol);
            return Navigate<T>(requestUri, headers, formData);
        }

        public IDocument<T> Navigate<T>(Uri uri, Dictionary<string, string> headers, Dictionary<string, string> formData)
        {
            _history.Navigate();
            _restClient.BaseUrl = uri;
            RestRequest request = new RestRequest();
            AddHeaders(request, headers);
            AddFormData(request, formData);
            return ExecuteAsGet<T>(request, "GET");
        }

        public async Task<IDocument<T>> NavigateAsync<T>(string uri)
        {
            Uri requestUri = UriHelper.Uri(uri, DefaultUriProtocol);
            return await NavigateAsync<T>(requestUri);
        }

        public async Task<IDocument<T>> NavigateAsync<T>(Uri uri)
        {
            _history.Navigate();
            _restClient.BaseUrl = uri;
            RestRequest request = new RestRequest();
            return await ExecuteTaskAsync<T>(request);
        }

        public async Task<IDocument<T>> NavigateAsync<T>(string uri, Dictionary<string, string> headers)
        {
            Uri requestUri = UriHelper.Uri(uri, DefaultUriProtocol);
            return await NavigateAsync<T>(requestUri,headers);
        }

        public async Task<IDocument<T>> NavigateAsync<T>(Uri uri, Dictionary<string, string> headers)
        {
            _history.Navigate();
            _restClient.BaseUrl = uri;
            RestRequest request = new RestRequest();
            AddHeaders(request, headers);
            return await ExecuteTaskAsync<T>(request);
        }

        public async Task<IDocument<T>> NavigateAsync<T>(string uri, Dictionary<string, string> headers, Dictionary<string, string> formData)
        {
            Uri requestUri = UriHelper.Uri(uri, DefaultUriProtocol);
            return await NavigateAsync<T>(requestUri, headers, formData);
        }

        public async Task<IDocument<T>> NavigateAsync<T>(Uri uri, Dictionary<string, string> headers, Dictionary<string, string> formData)
        {
            _history.Navigate();
            _restClient.BaseUrl = uri;
            RestRequest request = new RestRequest();
            AddHeaders(request, headers);
            AddFormData(request, formData);
            return await ExecuteTaskAsync<T>(request);
        }

        public IDocument<T> Submit<T>(string uri)
        {
            Uri requestUri = UriHelper.Uri(uri, DefaultUriProtocol);
            return Submit<T>(requestUri);
        }

        public IDocument<T> Submit<T>(Uri uri)
        {
            _history.Submit();
            _restClient.BaseUrl = uri;
            RestRequest request = new RestRequest();
            return ExecuteAsPost<T>(request, "POST");
        }

        public IDocument<T> Submit<T>(string uri, Dictionary<string, string> formData)
        {
            Uri requestUri = UriHelper.Uri(uri, DefaultUriProtocol);
            return Submit<T>(requestUri, formData);
        }

        public IDocument<T> Submit<T>(Uri uri, Dictionary<string, string> formData)
        {
            _history.Submit();
            _restClient.BaseUrl = uri;
            RestRequest request = new RestRequest();
            AddFormData(request, formData);
            return ExecuteAsPost<T>(request, "POST");
        }

        public IDocument<T> Submit<T>(string uri, Dictionary<string, string> formData, Dictionary<string, string> headers)
        {
            Uri requestUri = UriHelper.Uri(uri, DefaultUriProtocol);
            return Submit<T>(requestUri, formData, headers);
        }

        public IDocument<T> Submit<T>(Uri uri, Dictionary<string, string> formData, Dictionary<string, string> headers)
        {
            _history.Submit();
            _restClient.BaseUrl = uri;
            RestRequest request = new RestRequest();
            AddFormData(request, formData);
            AddHeaders(request, headers);
            return ExecuteAsPost<T>(request, "POST");
        }

        public async Task<IDocument<T>> SubmitAsync<T>(string uri)
        {
            Uri requestUri = UriHelper.Uri(uri, DefaultUriProtocol);
            return await SubmitAsync<T>(requestUri);
        }

        public async Task<IDocument<T>> SubmitAsync<T>(Uri uri)
        {
            _history.Submit();
            _restClient.BaseUrl = uri;
            RestRequest request = new RestRequest();
            return await ExecutePostTaskAsync<T>(request);
        }

        public async Task<IDocument<T>> SubmitAsync<T>(string uri, Dictionary<string, string> formData)
        {
            Uri requestUri = UriHelper.Uri(uri, DefaultUriProtocol);
            return await SubmitAsync<T>(requestUri, formData);
        }

        public async Task<IDocument<T>> SubmitAsync<T>(Uri uri, Dictionary<string, string> formData)
        {
            _history.Submit();
            _restClient.BaseUrl = uri;
            RestRequest request = new RestRequest();
            AddFormData(request, formData);
            return await ExecutePostTaskAsync<T>(request);
        }

        public async Task<IDocument<T>> SubmitAsync<T>(string uri, Dictionary<string, string> formData, Dictionary<string, string> headers)
        {
            Uri requestUri = UriHelper.Uri(uri, DefaultUriProtocol);
            return await SubmitAsync<T>(requestUri, formData, headers);
        }

        public async Task<IDocument<T>> SubmitAsync<T>(Uri uri, Dictionary<string, string> formData, Dictionary<string, string> headers)
        {
            _history.Submit();
            _restClient.BaseUrl = uri;
            RestRequest request = new RestRequest();
            AddFormData(request, formData);
            AddHeaders(request, headers);
            return await ExecutePostTaskAsync<T>(request);
        }

        public IDocument<T> SubmitForm<T>(Form form)
        {
            return SubmitForm<T>(form, null);
        }

        public IDocument<T> SubmitForm<T>(Form form, Dictionary<string, string> headers)
        {
            if (form.Method.ToLower() == "get")
                return Navigate<T>(form.Action, headers, form.FormValues);
            else if (form.Method.ToLower() == "post")
                return Submit<T>(form.Action, form.FormValues, headers);

            /* Default to post */
            return Submit<T>(form.Action, form.FormValues, headers);
        }

        public async Task<IDocument<T>> SubmitFormAsync<T>(Form form)
        {
            return await SubmitFormAsync<T>(form, null);
        }

        public async Task<IDocument<T>> SubmitFormAsync<T>(Form form, Dictionary<string, string> headers)
        {
            if (form.Method.ToLower() == "get")
                return await NavigateAsync<T>(form.Action, headers, form.FormValues);
            else if (form.Method.ToLower() == "post")
                return await SubmitAsync<T>(form.Action, form.FormValues, headers);

            /* Default to post */
            return await SubmitAsync<T>(form.Action, form.FormValues, headers);
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
