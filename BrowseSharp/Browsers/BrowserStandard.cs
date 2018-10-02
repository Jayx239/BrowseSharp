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
    public class BrowserStandard : BrowserCore, IBrowser
    {
        /// <summary>
        /// Default Constructor
        /// </summary>
        public BrowserStandard(): base()
        {
            _browserStandard = new StandardCore(base.JavascriptEngine, base.StyleEngine, _restClient, _restClient.CookieContainer, _history, base.StyleScrapingEnabled, base.JavascriptScrapingEnabled, DefaultUriProtocol);
        }
        
        /// <summary>
        /// Standard browser client for making standard web requests
        /// </summary>
        private StandardCore _browserStandard;

        /// <summary>
        /// Enables or disables javascript scraping on each request, sets for all browsers
        /// </summary>
        public override bool JavascriptScrapingEnabled {
            get { return base.JavascriptScrapingEnabled; }
            set {
                base.JavascriptScrapingEnabled = value;
                _browserStandard.JavascriptScrapingEnabled = value;
            }
        }

        /// <summary>
        /// Enables or disables style scraping on each request, sets for all browsers
        /// </summary>
        public override bool StyleScrapingEnabled { 
            get { return base.StyleScrapingEnabled; }
            set {
                base.StyleScrapingEnabled = value;
                _browserStandard.StyleScrapingEnabled = value;
            }
        }
        
        /// <inheritdoc />
        public IDocument Execute(IRestRequest request)
        {
            return _browserStandard.Execute(request);
        }

        /// <inheritdoc />
        public IDocument ExecuteAsGet(IRestRequest request, string httpMethod)
        {
            return _browserStandard.ExecuteAsGet(request, httpMethod);
        }

        /// <inheritdoc />
        public IDocument ExecuteAsPost(IRestRequest request, string httpMethod)
        {
            return _browserStandard.ExecuteAsPost(request, httpMethod);
        }

        /// <inheritdoc />
        public async Task<IDocument> ExecuteTaskAsync(IRestRequest request, CancellationToken token)
        {
            return await _browserStandard.ExecuteTaskAsync(request, token);
        }

        /// <inheritdoc />
        public async Task<IDocument> ExecuteTaskAsync(IRestRequest request)
        {
            return await _browserStandard.ExecuteTaskAsync(request);
        }

        /// <inheritdoc />
        public async Task<IDocument> ExecuteGetTaskAsync(IRestRequest request)
        {
            return await _browserStandard.ExecuteGetTaskAsync(request);
        }

        /// <inheritdoc />
        public async Task<IDocument> ExecuteGetTaskAsync(IRestRequest request, CancellationToken token)
        {
            return await _browserStandard.ExecuteGetTaskAsync(request, token);
        }

        /// <inheritdoc />
        public async Task<IDocument> ExecutePostTaskAsync(IRestRequest request)
        {
            return await _browserStandard.ExecutePostTaskAsync(request);
        }

        /// <inheritdoc />
        public async Task<IDocument> ExecutePostTaskAsync(IRestRequest request, CancellationToken token)
        {
            return await _browserStandard.ExecutePostTaskAsync(request, token);
        }

        /// <inheritdoc />
        public IDocument Navigate(string uri)
        {
            return _browserStandard.Navigate(uri);
        }

        /// <inheritdoc />
        public IDocument Navigate(Uri uri)
        {
            return _browserStandard.Navigate(uri);
        }

        /// <inheritdoc />
        public IDocument Navigate(string uri, Dictionary<string, string> headers)
        {
            return _browserStandard.Navigate(uri, headers);
        }

        /// <inheritdoc />
        public IDocument Navigate(Uri uri, Dictionary<string, string> headers)
        {
            return _browserStandard.Navigate(uri, headers);
        }

        /// <inheritdoc />
        public IDocument Navigate(string uri, Dictionary<string, string> headers, Dictionary<string, string> formData)
        {
            return _browserStandard.Navigate(uri, headers, formData);
        }

        /// <inheritdoc />
        public IDocument Navigate(Uri uri, Dictionary<string, string> headers, Dictionary<string, string> formData)
        {
            return _browserStandard.Navigate(uri, headers, formData);
        }

        /// <inheritdoc />
        public async Task<IDocument> NavigateAsync(string uri)
        {
            return await _browserStandard.NavigateAsync(uri);
        }

        /// <inheritdoc />
        public async Task<IDocument> NavigateAsync(Uri uri)
        {
            return await _browserStandard.NavigateAsync(uri);
        }

        /// <inheritdoc />
        public async Task<IDocument> NavigateAsync(string uri, Dictionary<string, string> headers)
        {
            return await _browserStandard.NavigateAsync(uri, headers);
        }

        /// <inheritdoc />
        public async Task<IDocument> NavigateAsync(Uri uri, Dictionary<string, string> headers)
        {
            return await _browserStandard.NavigateAsync(uri, headers);
        }

        /// <inheritdoc />
        public async Task<IDocument> NavigateAsync(string uri, Dictionary<string, string> headers,
            Dictionary<string, string> formData)
        {
            return await _browserStandard.NavigateAsync(uri, headers, formData);
        }

        /// <inheritdoc />
        public async Task<IDocument> NavigateAsync(Uri uri, Dictionary<string, string> headers,
            Dictionary<string, string> formData)
        {
            return await _browserStandard.NavigateAsync(uri, headers, formData);
        }

        /// <inheritdoc />
        public IDocument Submit(string uri)
        {
            return _browserStandard.Submit(uri);
        }

        /// <inheritdoc />
        public IDocument Submit(Uri uri)
        {
            return _browserStandard.Submit(uri);
        }

        /// <inheritdoc />
        public IDocument Submit(string uri, Dictionary<string, string> formData)
        {
            return _browserStandard.Submit(uri, formData);
        }

        /// <inheritdoc />
        public IDocument Submit(Uri uri, Dictionary<string, string> formData)
        {
            return _browserStandard.Submit(uri, formData);
        }

        /// <inheritdoc />
        public IDocument Submit(string uri, Dictionary<string, string> formData, Dictionary<string, string> headers)
        {
            return _browserStandard.Submit(uri, formData, headers);
        }

        /// <inheritdoc />
        public IDocument Submit(Uri uri, Dictionary<string, string> formData, Dictionary<string, string> headers)
        {
            return _browserStandard.Submit(uri, formData, headers);
        }

        /// <inheritdoc />
        public async Task<IDocument> SubmitAsync(string uri)
        {
            return await _browserStandard.SubmitAsync(uri);
        }

        /// <inheritdoc />
        public async Task<IDocument> SubmitAsync(Uri uri)
        {
            return await _browserStandard.SubmitAsync(uri);
        }

        /// <inheritdoc />
        public async Task<IDocument> SubmitAsync(string uri, Dictionary<string, string> formData)
        {
            return await _browserStandard.SubmitAsync(uri, formData);
        }

        /// <inheritdoc />
        public async Task<IDocument> SubmitAsync(Uri uri, Dictionary<string, string> formData)
        {
            return await _browserStandard.SubmitAsync(uri, formData);
        }

        /// <inheritdoc />
        public async Task<IDocument> SubmitAsync(string uri, Dictionary<string, string> formData, Dictionary<string, string> headers)
        {
            return await _browserStandard.SubmitAsync(uri, formData, headers);
        }

        /// <inheritdoc />
        public async Task<IDocument> SubmitAsync(Uri uri, Dictionary<string, string> formData, Dictionary<string, string> headers)
        {
            return await _browserStandard.SubmitAsync(uri, formData, headers);
        }

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
            return Execute(oldDocument.Request);
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
            return await ExecuteTaskAsync(oldDocument.Request);
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
            return Execute(forwardDocument.Request);

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
            return await ExecuteTaskAsync(forwardDocument.Request);

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
            return Execute(oldDocument.Request);
        }

        /// <summary>
        /// Refresh page, re-submits last request asynchronously
        /// </summary>
        /// <returns></returns>
        public async Task<IDocument> RefreshAsync()
        {
            IDocument oldDocument = _history.Refresh();
            _restClient.BaseUrl = oldDocument.RequestUri;
            return await ExecuteTaskAsync(oldDocument.Request);
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
        /// Submits form
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        public IDocument SubmitForm(Form form)
        {
            return SubmitForm(form, null);
        }

        /// <summary>
        /// Submits form
        /// </summary>
        /// <param name="form"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public IDocument SubmitForm(Form form, Dictionary<string, string> headers)
        {
            if (form.Method.ToLower() == "get")
                return Navigate(form.Action, headers, form.FormValues);
            else if (form.Method.ToLower() == "post")
                return Submit(form.Action, form.FormValues, headers);

            /* Default to post */
            return Submit(form.Action, form.FormValues, headers);
        }

        /// <summary>
        /// Submits a form asynchronously
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        public async Task<IDocument> SubmitFormAsync(Form form)
        {
            return await SubmitFormAsync(form, null);
        }

        /// <summary>
        /// Submits a form asynchronously
        /// </summary>
        /// <param name="form"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public async Task<IDocument> SubmitFormAsync(Form form, Dictionary<string, string> headers)
        {
            if (form.Method.ToLower() == "get")
                return await NavigateAsync(form.Action, headers, form.FormValues);
            else if (form.Method.ToLower() == "post")
                return await SubmitAsync(form.Action, form.FormValues, headers);

            /* Default to post */
            return await SubmitAsync(form.Action, form.FormValues, headers);
        }

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