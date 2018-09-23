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
    /// Headless browser implimentation that creates documents for each web request.
    /// </summary>
    public class StandardCore : BrowserCore, IBrowser
    {
        /// <summary>
        /// Default Constructor
        /// </summary>
        public StandardCore(): base()
        {

        }

        public StandardCore(JavascriptEngine javascriptEngine, StyleEngine styleEngine, 
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
        
        /// <summary>
        /// Executes a request
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public IDocument Execute(IRestRequest request)
        {
            IRestResponse response = _restClient.Execute(request);
            IDocument document = PackageAndAddDocument(request, response);
            return document;
        }

        /// <summary>
        /// Executes get request
        /// </summary>
        /// <param name="request"></param>
        /// <param name="httpMethod"></param>
        /// <returns></returns>
        public IDocument ExecuteAsGet(IRestRequest request, string httpMethod)
        {
            IRestResponse response = _restClient.ExecuteAsGet(request, httpMethod);
            IDocument document = PackageAndAddDocument(request, response);
            return document;
        }

        /// <summary>
        /// Executes post request
        /// </summary>
        /// <param name="request"></param>
        /// <param name="httpMethod"></param>
        /// <returns></returns>
        public IDocument ExecuteAsPost(IRestRequest request, string httpMethod)
        {
            IRestResponse response = _restClient.ExecuteAsPost(request, httpMethod);
            IDocument document = PackageAndAddDocument(request, response);
            return document;
        }

        /// <summary>
        /// Executes request asynchronously
        /// </summary>
        /// <param name="request"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<IDocument> ExecuteTaskAsync(IRestRequest request, CancellationToken token)
        {
            Task<IRestResponse> responseTask = _restClient.ExecuteTaskAsync(request, token);
            Task<IDocument> documentTask = PackageAndAddDocumentAsync(request, responseTask);
            return await documentTask;
        }

        /// <summary>
        /// Executes request asynchronously
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<IDocument> ExecuteTaskAsync(IRestRequest request)
        {
            Task<IRestResponse> responseTask = _restClient.ExecuteTaskAsync(request);
            Task<IDocument> documentTask = PackageAndAddDocumentAsync(request, responseTask);
            return await documentTask;
        }

        /// <summary>
        /// Executes get request asynchronously
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<IDocument> ExecuteGetTaskAsync(IRestRequest request)
        {
            Task<IRestResponse> responseTask = _restClient.ExecuteGetTaskAsync(request);
            Task<IDocument> documentTask = PackageAndAddDocumentAsync(request, responseTask);
            return await documentTask;
        }

        /// <summary>
        /// Executes get request asynchronously
        /// </summary>
        /// <param name="request"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<IDocument> ExecuteGetTaskAsync(IRestRequest request, CancellationToken token)
        {
            Task<IRestResponse> responseTask = _restClient.ExecuteGetTaskAsync(request, token);
            Task<IDocument> documentTask = PackageAndAddDocumentAsync(request, responseTask);
            return await documentTask;
        }

        /// <summary>
        /// Executes post request asynchronously
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<IDocument> ExecutePostTaskAsync(IRestRequest request)
        {
            Task<IRestResponse> responseTask = _restClient.ExecutePostTaskAsync(request);
            Task<IDocument> documentTask = PackageAndAddDocumentAsync(request, responseTask);
            return await documentTask;
        }

        /// <summary>
        /// Executes post request asynchronously
        /// </summary>
        /// <param name="request"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<IDocument> ExecutePostTaskAsync(IRestRequest request, CancellationToken token)
        {
            Task<IRestResponse> responseTask = _restClient.ExecutePostTaskAsync(request, token);
            Task<IDocument> documentTask = PackageAndAddDocumentAsync(request, responseTask);
            return await documentTask;
        }

        /// <summary>
        /// Performs get request
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        public IDocument Navigate(string uri)
        {
            Uri requestUri = UriHelper.Uri(uri, DefaultUriProtocol);
            return Navigate(requestUri);
        }

        /// <summary>
        /// Performs get request
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        public IDocument Navigate(Uri uri)
        {
            _history.Navigate();
            _restClient.BaseUrl = uri;
            RestRequest request = new RestRequest();
            return Execute(request);
        }

        /// <summary>
        /// Performs get request
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public IDocument Navigate(string uri, Dictionary<string, string> headers)
        {
            Uri requestUri = UriHelper.Uri(uri, DefaultUriProtocol);
            return Navigate(requestUri, headers);
        }

        /// <summary>
        /// Performs get request
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public IDocument Navigate(Uri uri, Dictionary<string, string> headers)
        {
            _history.Navigate();
            _restClient.BaseUrl = uri;
            RestRequest request = new RestRequest();
            AddHeaders(request, headers);
            return ExecuteAsGet(request, "GET");
        }

        /// <summary>
        /// Performs get request
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="headers"></param>
        /// <param name="formData"></param>
        /// <returns></returns>
        public IDocument Navigate(string uri, Dictionary<string, string> headers, Dictionary<string, string> formData)
        {
            Uri requestUri = UriHelper.Uri(uri, DefaultUriProtocol);
            return Navigate(requestUri, headers, formData);
        }

        /// <summary>
        /// Performs get request
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="headers"></param>
        /// <param name="formData"></param>
        /// <returns></returns>
        public IDocument Navigate(Uri uri, Dictionary<string, string> headers, Dictionary<string, string> formData)
        {
            _history.Navigate();
            _restClient.BaseUrl = uri;
            RestRequest request = new RestRequest();
            AddHeaders(request, headers);
            AddFormData(request, formData);
            return ExecuteAsGet(request, "GET");
        }

        /// <summary>
        /// Performs get request asynchronously
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        public async Task<IDocument> NavigateAsync(string uri)
        {
            Uri requestUri = UriHelper.Uri(uri, DefaultUriProtocol);
            return await NavigateAsync(requestUri);
        }

        /// <summary>
        /// Performs get request asynchronously
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        public async Task<IDocument> NavigateAsync(Uri uri)
        {
            _history.Navigate();
            _restClient.BaseUrl = uri;
            RestRequest request = new RestRequest();
            return await ExecuteTaskAsync(request);
        }

        /// <summary>
        /// Performs get request asynchronously
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public async Task<IDocument> NavigateAsync(string uri, Dictionary<string, string> headers)
        {
            Uri requestUri = UriHelper.Uri(uri, DefaultUriProtocol);
            return await NavigateAsync(requestUri, headers);
        }

        /// <summary>
        /// Performs get request asynchronously
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public async Task<IDocument> NavigateAsync(Uri uri, Dictionary<string, string> headers)
        {
            _history.Navigate();
            _restClient.BaseUrl = uri;
            RestRequest request = new RestRequest();
            AddHeaders(request, headers);
            return await ExecuteTaskAsync(request);
        }

        /// <summary>
        /// Performs get request asynchronously
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="headers"></param>
        /// <param name="formData"></param>
        /// <returns></returns>
        public async Task<IDocument> NavigateAsync(string uri, Dictionary<string, string> headers,
            Dictionary<string, string> formData)
        {
            Uri requestUri = UriHelper.Uri(uri, DefaultUriProtocol);
            return await NavigateAsync(requestUri, headers, formData);
        }

        /// <summary>
        /// Performs get request asynchronously
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="headers"></param>
        /// <param name="formData"></param>
        /// <returns></returns>
        public async Task<IDocument> NavigateAsync(Uri uri, Dictionary<string, string> headers,
            Dictionary<string, string> formData)
        {
            _history.Navigate();
            _restClient.BaseUrl = uri;
            RestRequest request = new RestRequest();
            AddHeaders(request, headers);
            AddFormData(request, formData);
            return await ExecuteTaskAsync(request);
        }

        /// <summary>
        /// Performs a post request
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        public IDocument Submit(string uri)
        {
            Uri requestUri = UriHelper.Uri(uri, DefaultUriProtocol);
            return Submit(requestUri);
        }

        /// <summary>
        /// Performs a post request
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        public IDocument Submit(Uri uri)
        {
            _history.Submit();
            _restClient.BaseUrl = uri;
            RestRequest request = new RestRequest();
            return ExecuteAsPost(request, "POST"); /* TODO: Check this */
        }

        /// <summary>
        /// Performs a post request
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="formData"></param>
        /// <returns></returns>
        public IDocument Submit(string uri, Dictionary<string, string> formData)
        {
            Uri requestUri = UriHelper.Uri(uri, DefaultUriProtocol);
            return Submit(requestUri, formData);
        }

        /// <summary>
        /// Performs a post request
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="formData"></param>
        /// <returns></returns>
        public IDocument Submit(Uri uri, Dictionary<string, string> formData)
        {
            _history.Submit();
            _restClient.BaseUrl = uri;
            RestRequest request = new RestRequest();
            AddFormData(request, formData);
            return ExecuteAsPost(request, "POST");
        }

        /// <summary>
        /// Performs a post request
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="formData"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public IDocument Submit(string uri, Dictionary<string, string> formData, Dictionary<string, string> headers)
        {
            Uri requestUri = UriHelper.Uri(uri, DefaultUriProtocol);
            return Submit(requestUri, formData, headers);
        }

        /// <summary>
        /// Performs a post request
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="formData"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public IDocument Submit(Uri uri, Dictionary<string, string> formData, Dictionary<string, string> headers)
        {
            _history.Submit();
            _restClient.BaseUrl = uri;
            RestRequest request = new RestRequest();
            AddFormData(request, formData);
            AddHeaders(request, headers);
            return ExecuteAsPost(request, "POST");
        }



        /// <summary>
        /// Performs a post request asynchronously
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        public async Task<IDocument> SubmitAsync(string uri)
        {
            Uri requestUri = UriHelper.Uri(uri, DefaultUriProtocol);
            return await SubmitAsync(requestUri);
        }

        /// <summary>
        /// Performs a post request asynchronously
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        public async Task<IDocument> SubmitAsync(Uri uri)
        {
            _history.Submit();
            _restClient.BaseUrl = uri;
            RestRequest request = new RestRequest();
            return await ExecutePostTaskAsync(request);
        }

        /// <summary>
        /// Performs a post request asynchronously
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="formData"></param>
        /// <returns></returns>
        public async Task<IDocument> SubmitAsync(string uri, Dictionary<string, string> formData)
        {
            Uri requestUri = UriHelper.Uri(uri, DefaultUriProtocol);
            return await SubmitAsync(requestUri, formData);
        }

        /// <summary>
        /// Performs a post request asynchronously
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="formData"></param>
        /// <returns></returns>
        public async Task<IDocument> SubmitAsync(Uri uri, Dictionary<string, string> formData)
        {
            _history.Submit();
            _restClient.BaseUrl = uri;
            RestRequest request = new RestRequest();
            AddFormData(request, formData);
            return await ExecutePostTaskAsync(request);
        }

        /// <summary>
        /// Performs a post request asynchronously
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="formData"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public async Task<IDocument> SubmitAsync(string uri, Dictionary<string, string> formData, Dictionary<string, string> headers)
        {
            Uri requestUri = UriHelper.Uri(uri, DefaultUriProtocol);
            return await SubmitAsync(requestUri, formData, headers);
        }

        /// <summary>
        /// Performs a post request asynchronously
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="formData"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public async Task<IDocument> SubmitAsync(Uri uri, Dictionary<string, string> formData, Dictionary<string, string> headers)
        {
            _history.Submit();
            _restClient.BaseUrl = uri;
            RestRequest request = new RestRequest();
            AddFormData(request, formData);
            AddHeaders(request, headers);
            return await ExecutePostTaskAsync(request);
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

    }
}