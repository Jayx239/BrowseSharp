using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using AngleSharp.Html.Dom;
using AngleSharp.Html.Parser;
using BrowseSharp.Common;
using BrowseSharp.Common.History;
using BrowseSharp.Common.Html;
using BrowseSharp.Common.Javascript;
using BrowseSharp.Common.Style;
using BrowseSharp.Common.Toolbox;
using RestSharp;

namespace BrowseSharp.Browsers.Core
{
    /// <summary>
    /// Browser core supporting type deserialization
    /// </summary>
    public class TypedCore : BrowserCore, IBrowserTyped
    {
        /// <summary>
        /// Default Constructor
        /// </summary>
        public TypedCore() : base()
        {

        }
        
        /// <summary>
        /// Secondary initializer
        /// </summary>
        /// <param name="javascriptEngine"></param>
        /// <param name="styleEngine"></param>
        /// <param name="restClient"></param>
        /// <param name="cookieContainer"></param>
        /// <param name="historyManager"></param>
        /// <param name="styleScrapingEnabled"></param>
        /// <param name="javascriptScrapingEnabled"></param>
        /// <param name="defaultUriProtocol"></param>
        public TypedCore(JavascriptEngine javascriptEngine, StyleEngine styleEngine,
                RestClient restClient, CookieContainer cookieContainer,
                HistoryManager historyManager, bool styleScrapingEnabled,
                bool javascriptScrapingEnabled, string defaultUriProtocol): 
            base(javascriptEngine, styleEngine,restClient,
                cookieContainer,historyManager,styleScrapingEnabled,
                javascriptScrapingEnabled,defaultUriProtocol)
        {
            
        }

        /// <inheritdoc />
        public IDocument<T> Execute<T>(IRestRequest request)
        {
            IRestResponse<T> response = Deserialize<T>(_restClient.Execute(request));
            IDocument<T> document = PackageAndAddDocument<T>(request, response);
            return document;
        }

        /// <inheritdoc />
        public IDocument<T> ExecuteAsGet<T>(IRestRequest request, string httpMethod)
        {
            IRestResponse<T> response = Deserialize<T>(_restClient.ExecuteAsGet(request, httpMethod));
            IDocument<T> document = PackageAndAddDocument<T>(request, response);
            return document;
        }

        /// <inheritdoc />
        public IDocument<T> ExecuteAsPost<T>(IRestRequest request, string httpMethod)
        {
            IRestResponse<T> response = Deserialize<T>(_restClient.ExecuteAsPost(request, httpMethod));
            IDocument<T> document = PackageAndAddDocument<T>(request, response);
            return document;
        }

        /// <inheritdoc />
        public async Task<IDocument<T>> ExecuteGetTaskAsync<T>(IRestRequest request)
        {
            Task<IRestResponse<T>> responseTask = _restClient.ExecuteGetTaskAsync<T>(request);
            Task<IDocument<T>> documentTask = PackageAndAddDocumentAsync(request, responseTask);
            return await documentTask;
        }

        /// <inheritdoc />
        public async Task<IDocument<T>> ExecuteGetTaskAsync<T>(IRestRequest request, CancellationToken token)
        {
            Task<IRestResponse<T>> responseTask = _restClient.ExecuteGetTaskAsync<T>(request,token);
            Task<IDocument<T>> documentTask = PackageAndAddDocumentAsync(request, responseTask);
            return await documentTask;
        }

        /// <inheritdoc />
        public async Task<IDocument<T>> ExecutePostTaskAsync<T>(IRestRequest request)
        {
            Task<IRestResponse<T>> responseTask = _restClient.ExecutePostTaskAsync<T>(request);
            Task<IDocument<T>> documentTask = PackageAndAddDocumentAsync(request, responseTask);
            return await documentTask;
        }

        /// <inheritdoc />
        public async Task<IDocument<T>> ExecutePostTaskAsync<T>(IRestRequest request, CancellationToken token)
        {
            Task<IRestResponse<T>> responseTask = _restClient.ExecutePostTaskAsync<T>(request, token);
            Task<IDocument<T>> documentTask = PackageAndAddDocumentAsync(request, responseTask);
            return await documentTask;
        }

        /// <inheritdoc />
        public async Task<IDocument<T>> ExecuteTaskAsync<T>(IRestRequest request, CancellationToken token)
        {
            Task<IRestResponse<T>> responseTask = _restClient.ExecuteTaskAsync<T>(request, token);
            Task<IDocument<T>> documentTask = PackageAndAddDocumentAsync(request, responseTask);
            return await documentTask;
        }

        /// <inheritdoc />
        public async Task<IDocument<T>> ExecuteTaskAsync<T>(IRestRequest request)
        {

            Task<IRestResponse<T>> responseTask = _restClient.ExecuteTaskAsync<T>(request);
            Task<IDocument<T>> documentTask = PackageAndAddDocumentAsync(request, responseTask);
            return await documentTask;
        }

        /// <inheritdoc />
        public IDocument<T> Navigate<T>(string uri)
        {
            Uri requestUri = UriHelper.Uri(uri, DefaultUriProtocol);
            return Navigate<T>(requestUri);
        }

        /// <inheritdoc />
        public IDocument<T> Navigate<T>(Uri uri)
        {
            _history.Navigate();
            _restClient.BaseUrl = uri;
            RestRequest request = new RestRequest();
            return Execute<T>(request);
        }

        /// <inheritdoc />
        public IDocument<T> Navigate<T>(string uri, Dictionary<string, string> headers)
        {
            Uri requestUri = UriHelper.Uri(uri, DefaultUriProtocol);
            return Navigate<T>(requestUri, headers);
        }

        /// <inheritdoc />
        public IDocument<T> Navigate<T>(Uri uri, Dictionary<string, string> headers)
        {
            _history.Navigate();
            _restClient.BaseUrl = uri;
            RestRequest request = new RestRequest();
            AddHeaders(request, headers);
            return ExecuteAsGet<T>(request, "GET");
        }

        /// <inheritdoc />
        public IDocument<T> Navigate<T>(string uri, Dictionary<string, string> headers, Dictionary<string, string> formData)
        {
            Uri requestUri = UriHelper.Uri(uri, DefaultUriProtocol);
            return Navigate<T>(requestUri, headers, formData);
        }

        /// <inheritdoc />
        public IDocument<T> Navigate<T>(Uri uri, Dictionary<string, string> headers, Dictionary<string, string> formData)
        {
            _history.Navigate();
            _restClient.BaseUrl = uri;
            RestRequest request = new RestRequest();
            AddHeaders(request, headers);
            AddFormData(request, formData);
            return ExecuteAsGet<T>(request, "GET");
        }

        /// <inheritdoc />
        public async Task<IDocument<T>> NavigateAsync<T>(string uri)
        {
            Uri requestUri = UriHelper.Uri(uri, DefaultUriProtocol);
            return await NavigateAsync<T>(requestUri);
        }

        /// <inheritdoc />
        public async Task<IDocument<T>> NavigateAsync<T>(Uri uri)
        {
            _history.Navigate();
            _restClient.BaseUrl = uri;
            RestRequest request = new RestRequest();
            return await ExecuteTaskAsync<T>(request);
        }

        /// <inheritdoc />
        public async Task<IDocument<T>> NavigateAsync<T>(string uri, Dictionary<string, string> headers)
        {
            Uri requestUri = UriHelper.Uri(uri, DefaultUriProtocol);
            return await NavigateAsync<T>(requestUri,headers);
        }

        /// <inheritdoc />
        public async Task<IDocument<T>> NavigateAsync<T>(Uri uri, Dictionary<string, string> headers)
        {
            _history.Navigate();
            _restClient.BaseUrl = uri;
            RestRequest request = new RestRequest();
            AddHeaders(request, headers);
            return await ExecuteTaskAsync<T>(request);
        }

        /// <inheritdoc />
        public async Task<IDocument<T>> NavigateAsync<T>(string uri, Dictionary<string, string> headers, Dictionary<string, string> formData)
        {
            Uri requestUri = UriHelper.Uri(uri, DefaultUriProtocol);
            return await NavigateAsync<T>(requestUri, headers, formData);
        }

        /// <inheritdoc />
        public async Task<IDocument<T>> NavigateAsync<T>(Uri uri, Dictionary<string, string> headers, Dictionary<string, string> formData)
        {
            _history.Navigate();
            _restClient.BaseUrl = uri;
            RestRequest request = new RestRequest();
            AddHeaders(request, headers);
            AddFormData(request, formData);
            return await ExecuteTaskAsync<T>(request);
        }

        /// <inheritdoc />
        public IDocument<T> Submit<T>(string uri)
        {
            Uri requestUri = UriHelper.Uri(uri, DefaultUriProtocol);
            return Submit<T>(requestUri);
        }

        /// <inheritdoc />
        public IDocument<T> Submit<T>(Uri uri)
        {
            _history.Submit();
            _restClient.BaseUrl = uri;
            RestRequest request = new RestRequest();
            return ExecuteAsPost<T>(request, "POST");
        }

        /// <inheritdoc />
        public IDocument<T> Submit<T>(string uri, Dictionary<string, string> formData)
        {
            Uri requestUri = UriHelper.Uri(uri, DefaultUriProtocol);
            return Submit<T>(requestUri, formData);
        }

        /// <inheritdoc />
        public IDocument<T> Submit<T>(Uri uri, Dictionary<string, string> formData)
        {
            _history.Submit();
            _restClient.BaseUrl = uri;
            RestRequest request = new RestRequest();
            AddFormData(request, formData);
            return ExecuteAsPost<T>(request, "POST");
        }

        /// <inheritdoc />
        public IDocument<T> Submit<T>(string uri, Dictionary<string, string> formData, Dictionary<string, string> headers)
        {
            Uri requestUri = UriHelper.Uri(uri, DefaultUriProtocol);
            return Submit<T>(requestUri, formData, headers);
        }

        /// <inheritdoc />
        public IDocument<T> Submit<T>(Uri uri, Dictionary<string, string> formData, Dictionary<string, string> headers)
        {
            _history.Submit();
            _restClient.BaseUrl = uri;
            RestRequest request = new RestRequest();
            AddFormData(request, formData);
            AddHeaders(request, headers);
            return ExecuteAsPost<T>(request, "POST");
        }

        /// <inheritdoc />
        public async Task<IDocument<T>> SubmitAsync<T>(string uri)
        {
            Uri requestUri = UriHelper.Uri(uri, DefaultUriProtocol);
            return await SubmitAsync<T>(requestUri);
        }

        /// <inheritdoc />
        public async Task<IDocument<T>> SubmitAsync<T>(Uri uri)
        {
            _history.Submit();
            _restClient.BaseUrl = uri;
            RestRequest request = new RestRequest();
            return await ExecutePostTaskAsync<T>(request);
        }

        /// <inheritdoc />
        public async Task<IDocument<T>> SubmitAsync<T>(string uri, Dictionary<string, string> formData)
        {
            Uri requestUri = UriHelper.Uri(uri, DefaultUriProtocol);
            return await SubmitAsync<T>(requestUri, formData);
        }

        /// <inheritdoc />
        public async Task<IDocument<T>> SubmitAsync<T>(Uri uri, Dictionary<string, string> formData)
        {
            _history.Submit();
            _restClient.BaseUrl = uri;
            RestRequest request = new RestRequest();
            AddFormData(request, formData);
            return await ExecutePostTaskAsync<T>(request);
        }
        
        /// <inheritdoc />
        public async Task<IDocument<T>> SubmitAsync<T>(string uri, Dictionary<string, string> formData, Dictionary<string, string> headers)
        {
            Uri requestUri = UriHelper.Uri(uri, DefaultUriProtocol);
            return await SubmitAsync<T>(requestUri, formData, headers);
        }

        /// <inheritdoc />
        public async Task<IDocument<T>> SubmitAsync<T>(Uri uri, Dictionary<string, string> formData, Dictionary<string, string> headers)
        {
            _history.Submit();
            _restClient.BaseUrl = uri;
            RestRequest request = new RestRequest();
            AddFormData(request, formData);
            AddHeaders(request, headers);
            return await ExecutePostTaskAsync<T>(request);
        }

        /// <inheritdoc />
        public IDocument<T> SubmitForm<T>(Form form)
        {
            return SubmitForm<T>(form, null);
        }

        /// <inheritdoc />
        public IDocument<T> SubmitForm<T>(Form form, Dictionary<string, string> headers)
        {
            if (form.Method.ToLower() == "get")
                return Navigate<T>(form.Action, headers, form.FormValues);
            else if (form.Method.ToLower() == "post")
                return Submit<T>(form.Action, form.FormValues, headers);

            /* Default to post */
            return Submit<T>(form.Action, form.FormValues, headers);
        }

        /// <inheritdoc />
        public async Task<IDocument<T>> SubmitFormAsync<T>(Form form)
        {
            return await SubmitFormAsync<T>(form, null);
        }

        /// <inheritdoc />
        public async Task<IDocument<T>> SubmitFormAsync<T>(Form form, Dictionary<string, string> headers)
        {
            if (form.Method.ToLower() == "get")
                return await NavigateAsync<T>(form.Action, headers, form.FormValues);
            else if (form.Method.ToLower() == "post")
                return await SubmitAsync<T>(form.Action, form.FormValues, headers);

            /* Default to post */
            return await SubmitAsync<T>(form.Action, form.FormValues, headers);
        }
        
        /// <summary>
        /// Gets current document from the current request
        /// </summary>
        /// <returns>Current Document</returns>
        public IDocument Document => _history.Document;
        
        /// <inheritdoc /> 
        public IDocument<T> DocumentTyped<T>()
        {
            return (IDocument<T>) _history.Document;
        }

        /// <summary>
        /// Package typed document
        /// </summary>
        /// <typeparam name="T">Type of document body</typeparam>
        /// <param name="request"></param>
        /// <param name="response"></param>
        /// <returns></returns>
        protected IDocument<T> PackageAndAddDocument<T>(IRestRequest request, IRestResponse<T> response)
        {
            Uri requestUri = _restClient.BaseUrl;
            HtmlParser parser = new HtmlParser();
            IHtmlDocument htmlDocument = parser.ParseDocument(response.Content);
            IDocument<T> document = new Document<T>(request, response, htmlDocument);
            if (JavascriptScrapingEnabled)
                JavascriptEngine.Add(document);
            if (StyleScrapingEnabled)
                StyleEngine.Add(document);
            document.RequestUri = requestUri;
            Documents.Add(document);
            return document;
        }

        /// <summary>
        /// Package typed document asynchronously
        /// </summary>
        /// <typeparam name="T">Type of document body</typeparam>
        /// <param name="request">Request to be packaged</param>
        /// <param name="responseTask">Reponse task to be packaged</param>
        /// <returns>Packaged document</returns>
        protected async Task<IDocument<T>> PackageAndAddDocumentAsync<T>(IRestRequest request, Task<IRestResponse<T>> responseTask)
        {
            Uri requestUri = _restClient.BaseUrl;
            IRestResponse<T> response = await responseTask;
            HtmlParser parser = new HtmlParser();
            IHtmlDocument htmlDocument = parser.ParseDocument(response.Content);
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
