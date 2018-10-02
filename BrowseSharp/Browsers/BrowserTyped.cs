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
using BrowseSharp.Browsers.Core;

namespace BrowseSharp.Browsers
{
    /// <summary>
    /// Headless browser implementation that creates documents for each web request.
    /// </summary>
    public class BrowserTyped : TypedCore, IBrowserTyped, IBrowserHistorySync
    {
        /// <summary>
        /// Default Constructor
        /// </summary>
        public BrowserTyped(): base()
        {
            _browserTyped = new TypedCore(base.JavascriptEngine, base.StyleEngine, _restClient, _restClient.CookieContainer, _history, base.StyleScrapingEnabled, base.JavascriptScrapingEnabled, DefaultUriProtocol);
        }

        /// <summary>
        /// Browser for making typed requests, deserializes response body to typed objects
        /// </summary>
        private TypedCore _browserTyped;
        
        /// <summary>
        /// Javascript engine used by browser
        /// </summary>
        public virtual JavascriptEngine JavascriptEngine { get { return _javascriptEngine; } }

        protected JavascriptEngine _javascriptEngine;

        /// <summary>
        /// Style engine for parsing css styles 
        /// </summary>
        public virtual StyleEngine StyleEngine { get { return _styleEngine; } }

        protected StyleEngine _styleEngine;
        
        /// <summary>
        /// Enables or disables javascript scraping on each request
        /// </summary>
        public virtual bool JavascriptScrapingEnabled { get { return _javascriptScrapingEnabled; }
            set
            {
                _javascriptScrapingEnabled = value;
                _browserTyped.JavascriptScrapingEnabled = value;
            } 
        }

        private bool _javascriptScrapingEnabled;

        /// <summary>
        /// Enables or disables style scraping on each request
        /// </summary>
        public virtual bool StyleScrapingEnabled { get { return _styleScrapingEnabled; }
            set
            {
                _styleScrapingEnabled = value;
                _browserTyped.StyleScrapingEnabled = value;
            } 
        }

        private bool _styleScrapingEnabled;
        
        /// <summary>
        /// Contains all previous documents stored for each previous request
        /// </summary>
        public List<IDocument> History => _history.History;

        /// <summary>
        /// Stores the forward history when the back method is called 
        /// </summary>
        public List<IDocument> ForwardHistory
        {
            get { return _history.ForwardHistory; }
        }

        /// <summary>
        /// Clears browse history by re-initializing Documents
        /// </summary>
        public void ClearHistory()
        {
            _history.ClearHistory();
        }

        /// <summary>
        /// Clears forward history
        /// </summary>
        public void ClearForwardHistory()
        {
            _history.ClearForwardHistory();
        }

        /// <summary>
        /// Method for navigating to last browser state by re-issuing the previous request
        /// </summary>
        public IDocument Back()
        {
            return Back(false);
        }

        /// <summary>
        /// Method for navigating to last browser state
        /// </summary>
        /// <param name="useCache">Determines whether to re-issue request or reload last document</param>
        public IDocument Back(bool useCache)
        {
            IDocument oldDocument = _history.Back(useCache);
            if (useCache)
                return oldDocument;

            _restClient.BaseUrl = oldDocument.RequestUri;
            return Execute<Object>(oldDocument.Request);
        }


        /// <summary>
        /// Method for navigating to last browser state by re-issuing the previous request asynchronously
        /// </summary>
        public async Task<IDocument> BackAsync()
        {
            return await BackAsync(false);
        }

        /// <summary>
        /// Method for navigating to last browser state asynchronously
        /// </summary>
        /// <param name="useCache">Determines whether to re-issue request or reload last document</param>
        public async Task<IDocument> BackAsync(bool useCache)
        {
            IDocument oldDocument = _history.Back(useCache);
            if (useCache)
                return oldDocument;

            _restClient.BaseUrl = oldDocument.RequestUri;
            return await ExecuteTaskAsync<Object>(oldDocument.Request);
        }

        /// <summary>
        /// Navigate to next document in forward history
        /// </summary>
        /// <returns></returns>
        public IDocument Forward()
        {
            return Forward(false);
        }

        /// <summary>
        /// Navigate to next document in forward history
        /// </summary>
        /// <param name="useCache"></param>
        /// <returns></returns>
        public IDocument Forward(bool useCache)
        {
            IDocument forwardDocument = _history.Forward(useCache);
            if (useCache)
                return forwardDocument;

            _restClient.BaseUrl = forwardDocument.RequestUri;
            return Execute<Object>(forwardDocument.Request);

        }

        /// <summary>
        /// Navigate to next document in forward history asynchronously
        /// </summary>
        /// <returns></returns>
        public async Task<IDocument> ForwardAsync()
        {
            return await ForwardAsync(false);
        }

        /// <summary>
        /// Navigate to next document in forward history asynchronously
        /// </summary>
        /// <param name="useCache"></param>
        /// <returns></returns>
        public async Task<IDocument> ForwardAsync(bool useCache)
        {

            IDocument forwardDocument = _history.Forward(useCache);
            if (useCache)
                return forwardDocument;

            _restClient.BaseUrl = forwardDocument.RequestUri;
            return await ExecuteTaskAsync<Object>(forwardDocument.Request);

        }

        /// <summary>
        /// Max amount of history/Documents to be stored by the browser (-1 for no limit)
        /// </summary>
        public int MaxHistorySize { get { return _history.MaxHistorySize; } set { _history.MaxHistorySize = value; } }

        /// <summary>
        /// Refresh page, re-submits last request
        /// </summary>
        /// <returns></returns>
        public IDocument Refresh()
        {
            IDocument oldDocument = _history.Refresh();
            _restClient.BaseUrl = oldDocument.RequestUri;
            return Execute<Object>(oldDocument.Request);
        }

        /// <summary>
        /// Refresh page, re-submits last request asynchronously
        /// </summary>
        /// <returns></returns>
        public async Task<IDocument> RefreshAsync()
        {
            IDocument oldDocument = _history.Refresh();
            _restClient.BaseUrl = oldDocument.RequestUri;
            return await ExecuteTaskAsync<Object>(oldDocument.Request);
        }

        /// <summary>
        /// Gets current document from the current request
        /// </summary>
        /// <returns>Current Document</returns>
        public IDocument Document => _history.Document;

        /// <summary>
        /// Documents generated for each request
        /// </summary>
        private List<IDocument> _documents;

        /// <summary>
        /// Stores the forward history when the back method is called 
        /// </summary>
        private List<IDocument> _forwardHistory;
        
        /// <summary>
        /// Creates a document from a request and response
        /// </summary>
        /// <param name="request"></param>
        /// <param name="response"></param>
        /// <returns></returns>
        protected IDocument PackageAndAddDocument(IRestRequest request, IRestResponse response)
        {
            Uri requestUri = _restClient.BaseUrl;
            HtmlParser parser = new HtmlParser();
            IHtmlDocument htmlDocument = parser.Parse(response.Content);
            IDocument document = new Document(request, response, htmlDocument);
            if (JavascriptScrapingEnabled)
                JavascriptEngine.Add(document);
            if (StyleScrapingEnabled)
                StyleEngine.Add(document);
            document.RequestUri = requestUri;
            Documents.Add(document);
            return document;
        }

        /// <summary>
        /// Creates a document from a request and response asynchronously
        /// </summary>
        /// <param name="request"></param>
        /// <param name="responseTask"></param>
        /// <returns></returns>
        protected async Task<IDocument> PackageAndAddDocumentAsync(IRestRequest request, Task<IRestResponse> responseTask)
        {
            Uri requestUri = _restClient.BaseUrl;
            IRestResponse response = await responseTask;
            HtmlParser parser = new HtmlParser();
            IHtmlDocument htmlDocument = parser.Parse(response.Content);
            IDocument document = new Document(request, response, htmlDocument);

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

        /// <inheritdoc />
        public IDocument<T> Execute<T>(IRestRequest request)
        {
            return _browserTyped.Execute<T>(request);
        }
        
        /// <inheritdoc />
        public IDocument<T> ExecuteAsGet<T>(IRestRequest request, string httpMethod)
        {
            return _browserTyped.ExecuteAsGet<T>(request, httpMethod);
        }

        /// <inheritdoc />
        public IDocument<T> ExecuteAsPost<T>(IRestRequest request, string httpMethod)
        {
            return _browserTyped.ExecuteAsPost<T>(request, httpMethod);
        }

        /// <inheritdoc />
        public async Task<IDocument<T>> ExecuteTaskAsync<T>(IRestRequest request, CancellationToken token)
        {
            return await _browserTyped.ExecuteTaskAsync<T>(request, token);
        }

        /// <inheritdoc />
        public async Task<IDocument<T>> ExecuteTaskAsync<T>(IRestRequest request)
        {
            return await _browserTyped.ExecuteTaskAsync<T>(request);
        }

        /// <inheritdoc />
        public async Task<IDocument<T>> ExecuteGetTaskAsync<T>(IRestRequest request)
        {
            return await _browserTyped.ExecuteGetTaskAsync<T>(request);
        }

        /// <inheritdoc />
        public async Task<IDocument<T>> ExecuteGetTaskAsync<T>(IRestRequest request, CancellationToken token)
        {
            return await _browserTyped.ExecuteGetTaskAsync<T>(request, token);
        }

        /// <inheritdoc />
        public async Task<IDocument<T>> ExecutePostTaskAsync<T>(IRestRequest request)
        {
            return await _browserTyped.ExecutePostTaskAsync<T>(request);
        }

        /// <inheritdoc />
        public async Task<IDocument<T>> ExecutePostTaskAsync<T>(IRestRequest request, CancellationToken token)
        {
            return await _browserTyped.ExecutePostTaskAsync<T>(request, token);
        }

        /// <inheritdoc />
        public IDocument<T> Navigate<T>(string uri)
        {
            return _browserTyped.Navigate<T>(uri);
        }

        /// <inheritdoc />
        public IDocument<T> Navigate<T>(Uri uri)
        {
            return _browserTyped.Navigate<T>(uri);
        }

        /// <inheritdoc />
        public IDocument<T> Navigate<T>(string uri, Dictionary<string, string> headers)
        {
            return _browserTyped.Navigate<T>(uri, headers);
        }

        /// <inheritdoc />
        public IDocument<T> Navigate<T>(Uri uri, Dictionary<string, string> headers)
        {
            return _browserTyped.Navigate<T>(uri, headers);
        }

        /// <inheritdoc />
        public IDocument<T> Navigate<T>(string uri, Dictionary<string, string> headers, Dictionary<string, string> formData)
        {
            return _browserTyped.Navigate<T>(uri, headers, formData);
        }

        /// <inheritdoc />
        public IDocument<T> Navigate<T>(Uri uri, Dictionary<string, string> headers, Dictionary<string, string> formData)
        {
            return _browserTyped.Navigate<T>(uri, headers, formData);
        }

        /// <inheritdoc />
        public async Task<IDocument<T>> NavigateAsync<T>(string uri)
        {
            return await _browserTyped.NavigateAsync<T>(uri);
        }

        /// <inheritdoc />
        public async Task<IDocument<T>> NavigateAsync<T>(Uri uri)
        {
            return await _browserTyped.NavigateAsync<T>(uri);
        }

        /// <inheritdoc />
        public async Task<IDocument<T>> NavigateAsync<T>(string uri, Dictionary<string, string> headers)
        {
            return await _browserTyped.NavigateAsync<T>(uri, headers);
        }

        /// <inheritdoc />
        public async Task<IDocument<T>> NavigateAsync<T>(Uri uri, Dictionary<string, string> headers)
        {
            return await _browserTyped.NavigateAsync<T>(uri, headers);
        }
        
        /// <inheritdoc />
        public async Task<IDocument<T>> NavigateAsync<T>(string uri, Dictionary<string, string> headers, Dictionary<string, string> formData)
        {
            return await _browserTyped.NavigateAsync<T>(uri, headers, formData);
        }

        /// <inheritdoc />
        public async Task<IDocument<T>> NavigateAsync<T>(Uri uri, Dictionary<string, string> headers, Dictionary<string, string> formData)
        {
            return await _browserTyped.NavigateAsync<T>(uri, headers, formData);
        }

        /// <inheritdoc />
        public IDocument<T> Submit<T>(string uri)
        {
            return _browserTyped.Submit<T>(uri);
        }

        /// <inheritdoc />
        public IDocument<T> Submit<T>(Uri uri)
        {
            return _browserTyped.Submit<T>(uri);
        }

        /// <inheritdoc />
        public IDocument<T> Submit<T>(string uri, Dictionary<string, string> formData)
        {
            return _browserTyped.Submit<T>(uri, formData);
        }

        /// <inheritdoc />
        public IDocument<T> Submit<T>(Uri uri, Dictionary<string, string> formData)
        {
            return _browserTyped.Submit<T>(uri, formData);
        }

        /// <inheritdoc />
        public IDocument<T> Submit<T>(string uri, Dictionary<string, string> formData, Dictionary<string, string> headers)
        {
            return _browserTyped.Submit<T>(uri, formData, headers);
        }

        /// <inheritdoc />
        public IDocument<T> Submit<T>(Uri uri, Dictionary<string, string> formData, Dictionary<string, string> headers)
        {
            return _browserTyped.Submit<T>(uri, formData, headers);
        }

        /// <inheritdoc />
        public IDocument<T> SubmitForm<T>(Form form)
        {
            return _browserTyped.SubmitForm<T>(form);
        }

        /// <inheritdoc />
        public IDocument<T> SubmitForm<T>(Form form, Dictionary<string, string> headers)
        {
            return _browserTyped.SubmitForm<T>(form, headers);
        }

        /// <inheritdoc />
        public async Task<IDocument<T>> SubmitAsync<T>(string uri)
        {
            return await _browserTyped.SubmitAsync<T>(uri);
        }

        /// <inheritdoc />
        public async Task<IDocument<T>> SubmitAsync<T>(Uri uri)
        {
            return await _browserTyped.SubmitAsync<T>(uri);
        }

        /// <inheritdoc />
        public async Task<IDocument<T>> SubmitAsync<T>(string uri, Dictionary<string, string> formData)
        {
            return await _browserTyped.SubmitAsync<T>(uri, formData);
        }

        /// <inheritdoc />
        public async Task<IDocument<T>> SubmitAsync<T>(Uri uri, Dictionary<string, string> formData)
        {
            return await _browserTyped.SubmitAsync<T>(uri, formData);
        }

        /// <inheritdoc />
        public async Task<IDocument<T>> SubmitAsync<T>(string uri, Dictionary<string, string> formData, Dictionary<string, string> headers)
        {
            return await _browserTyped.SubmitAsync<T>(uri, formData, headers);
        }

        /// <inheritdoc />
        public async Task<IDocument<T>> SubmitAsync<T>(Uri uri, Dictionary<string, string> formData, Dictionary<string, string> headers)
        {
            return await _browserTyped.SubmitAsync<T>(uri, formData, headers);
        }

        /// <inheritdoc />
        public async Task<IDocument<T>> SubmitFormAsync<T>(Form form)
        {
            return await _browserTyped.SubmitFormAsync<T>(form);
        }

        /// <inheritdoc />
        public async Task<IDocument<T>> SubmitFormAsync<T>(Form form, Dictionary<string, string> headers)
        {
            return await _browserTyped.SubmitFormAsync<T>(form, headers);
        }

        /// <inheritdoc/>
        public IDocument<T> DocumentTyped<T>()
        {
            return _browserTyped.DocumentTyped<T>();
        }
        
        /// <summary>
        /// Adds form data to the request input
        /// </summary>
        /// <param name="request"></param>
        /// <param name="formData"></param>
        protected void AddFormData(IRestRequest request, Dictionary<string, string> formData)
        {
            if (formData == null)
                return;

            foreach (var formInput in formData)
            {
                request.AddParameter(formInput.Key, formInput.Value);
            }
        }

        /// <summary>
        /// Adds headers to the request input
        /// </summary>
        /// <param name="request"></param>
        /// <param name="headers"></param>
        protected void AddHeaders(IRestRequest request, Dictionary<string, string> headers)
        {
            if (headers == null)
                return;

            foreach (var formInput in headers)
            {
                request.AddHeader(formInput.Key, formInput.Value);
            }
        }
    }
}