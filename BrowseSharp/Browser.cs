using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Cache;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AngleSharp.Dom.Html;
using AngleSharp.Parser.Html;
using BrowseSharp.Browsers;
using BrowseSharp.Browsers.Core;
using BrowseSharp.History;
using BrowseSharp.Html;
using BrowseSharp.Javascript;
using BrowseSharp.Style;
using BrowseSharp.Toolbox;
using Jint.Parser;
using RestSharp;
using RestSharp.Authenticators;
using RestSharp.Deserializers;

namespace BrowseSharp
{
    /// <summary>
    /// Headless browser implimentation that creates documents for each web request.
    /// </summary>
    public class Browser : BrowserCore, IBrowser, IBrowserHistorySync, IBrowserHistoryAsync
    {
        /// <summary>
        /// Default Constructor
        /// </summary>
        public Browser(): base()
        {
            /*JavascriptEngine = new JavascriptEngine();
            StyleEngine = new StyleEngine();
            _restClient = new RestClient();
            _restClient.CookieContainer = new CookieContainer();
            _history = new HistoryManager();*/
            //_styleScrapingEnabled = true;
            //_javascriptScrapingEnabled = true;
            _browserStandard = new StandardCore(JavascriptEngine, StyleEngine, _restClient, _restClient.CookieContainer, _history, StyleScrapingEnabled, JavascriptScrapingEnabled, DefaultUriProtocol);
            //_browserTyped = new BrowserTyped(JavascriptEngine, StyleEngine, _restClient, _restClient.CookieContainer, _history, StyleScrapingEnabled, JavascriptScrapingEnabled, DefaultUriProtocol);
            //DefaultUriProtocol = "http";
            _browserTyped = new TypedCore(JavascriptEngine, StyleEngine, _restClient, _restClient.CookieContainer, _history, StyleScrapingEnabled, JavascriptScrapingEnabled, DefaultUriProtocol);

        }

        /// <summary>
        /// Standard browser client for making standard web requests
        /// </summary>
        StandardCore _browserStandard;

        /// <summary>
        /// Browser for making typed requests, deserializes response body to typed objects
        /// </summary>
        TypedCore _browserTyped;

        /// <summary>
        /// Enables or disables javascript scraping on each request, sets for all browsers
        /// </summary>
        public override bool JavascriptScrapingEnabled {
            get { return _javascriptScrapingEnabled; }
            set {
                _javascriptScrapingEnabled = value;
                _browserStandard.JavascriptScrapingEnabled = value;
            }
        }

        /// <summary>
        /// Enables or disables style scraping on each request, sets for all browsers
        /// </summary>
        public override bool StyleScrapingEnabled { get { return _styleScrapingEnabled; }
            set {
                _styleScrapingEnabled = value;
                _browserStandard.StyleScrapingEnabled = value;
            }
        }

        /*
        /// <summary>
        /// Cookie container containing cookies
        /// </summary>
        public CookieContainer CookieContainer
        {
            get { return _restClient.CookieContainer; }
            set { _restClient.CookieContainer = value; }
        }

        /// <summary>
        /// Automatic decompression attribute for client
        /// </summary>
        public bool AutomaticDecompression
        {
            get { return _restClient.AutomaticDecompression; }
            set { _restClient.AutomaticDecompression = value; }
        }

        /// <summary>
        /// Max number of redirects to be taken for a request
        /// </summary>
        public int? MaxRedirects
        {
            get { return _restClient.MaxRedirects; }
            set { _restClient.MaxRedirects = value; }
        }

        /// <summary>
        /// User agent that client should send in request
        /// </summary>
        public string UserAgent
        {
            get { return _restClient.UserAgent; }
            set { _restClient.UserAgent = value; }
        }

        /// <summary>
        /// Timeout for client
        /// </summary>
        public int Timeout
        {
            get { return _restClient.Timeout; }
            set { _restClient.Timeout = value; }
        }

        /// <summary>
        /// Read write timeout for client
        /// </summary>
        public int ReadWriteTimeout
        {
            get { return _restClient.ReadWriteTimeout; }
            set { _restClient.ReadWriteTimeout = value; }
        }

        /// <summary>
        /// Use synchronization context attribute for client
        /// </summary>
        public bool UseSynchronizationContext
        {
            get { return _restClient.UseSynchronizationContext; }
            set { _restClient.UseSynchronizationContext = value; }
        }

        /// <summary>
        /// Authenticator to be used by client
        /// </summary>
        public IAuthenticator Authenticator
        {
            get { return _restClient.Authenticator; }
            set { _restClient.Authenticator = value; }
        }

        /// <summary>
        /// Base url for client that is used for each request
        /// </summary>
        public Uri BaseUrl
        {
            get { return _restClient.BaseUrl; }
            set { _restClient.BaseUrl = value; }
        }

        /// <summary>
        /// Encoding for client request
        /// </summary>
        public Encoding Encoding
        {
            get { return _restClient.Encoding; }
            set { _restClient.Encoding = value; }
        }

        /// <summary>
        /// Pre-Authenticate parameter for client
        /// </summary>
        public bool PreAuthenticate
        {
            get { return _restClient.PreAuthenticate; }
            set { _restClient.PreAuthenticate = value; }
        }

        /// <summary>
        /// Default parameters for client
        /// </summary>
        public IList<Parameter> DefaultParameters
        {
            get { return _restClient.DefaultParameters; }
        }

        /// <summary>
        /// Base host for client
        /// </summary>
        public string BaseHost
        {
            get { return _restClient.BaseHost; }
            set { _restClient.BaseHost = value; }
        }

        /// <summary>
        /// The default protocol to be pre-pended to a uri if the uri string does not contain a protocol
        /// http or https
        /// </summary>
        public string DefaultUriProtocol
        {
            get => _defaultUriProtocol;
            set
            {
                if(value == "http" || value == "https")
                    _defaultUriProtocol = value;
            }
        }
        
        /// <summary>
        /// The default protocol to be pre-pended to a uri if the uri string does not contain a protocol
        /// </summary>
        private string _defaultUriProtocol;
        
        /// <summary>
        /// Deserialize method for client
        /// </summary>
        /// <param name="response"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public IRestResponse<T> Deserialize<T>(IRestResponse response)
        {
            return _restClient.Deserialize<T>(response);
        }

        /// <summary>
        /// Downloads data from response
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public byte[] DownloadData(IRestRequest request)
        {
            return _restClient.DownloadData(request);
        }

        /// <summary>
        /// X509Certificates for client
        /// </summary>
        public X509CertificateCollection ClientCertificates
        {
            get { return _restClient.ClientCertificates; }
            set { _restClient.ClientCertificates = value; }
        }

        /// <summary>
        /// Proxy for client
        /// </summary>
        public IWebProxy Proxy
        {
            get { return _restClient.Proxy; }
            set { _restClient.Proxy = value; }
        }

        /// <summary>
        /// Request cache policy for client
        /// </summary>
        public RequestCachePolicy CachePolicy
        {
            get { return _restClient.CachePolicy; }
            set { _restClient.CachePolicy = value; }
        }

        /// <summary>
        /// Pipelined attribute for client
        /// </summary>
        public bool Pipelined
        {
            get { return _restClient.Pipelined; }
            set { _restClient.Pipelined = value; }
        }

        /// <summary>
        /// Follow Redirects attribute for client
        /// </summary>
        public bool FollowRedirects
        {
            get { return _restClient.FollowRedirects; }
            set { _restClient.FollowRedirects = value; }
        }

        /// <summary>
        /// Helper for building uri for client
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public Uri BuildUri(IRestRequest request)
        {
            return _restClient.BuildUri(request);
        }

        /// <summary>
        /// Remote certification validation callback for client
        /// </summary>
        public RemoteCertificateValidationCallback RemoteCertificateValidationCallback
        {
            get { return _restClient.RemoteCertificateValidationCallback; }
            set { _restClient.RemoteCertificateValidationCallback = value; }
        }

        /// <summary>
        /// Adds handler for client
        /// </summary>
        /// <param name="contentType"></param>
        /// <param name="deserializer"></param>
        public void AddHandler(string contentType, IDeserializer deserializer)
        {
            _restClient.AddHandler(contentType, deserializer);
        }

        /// <summary>
        /// Removes handler from client
        /// </summary>
        /// <param name="contentType"></param>
        public void RemoveHandler(string contentType)
        {
            _restClient.RemoveHandler(contentType);
        }

        /// <summary>
        /// Clears all handlers for client
        /// </summary>
        public void ClearHandlers()
        {
            _restClient.ClearHandlers();
        }
        */
        /// <summary>
        /// Executes a request
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public IDocument Execute(IRestRequest request)
        {
            return _browserStandard.Execute(request);
            /*IRestResponse response = _restClient.Execute(request);
            IDocument document = PackageAndAddDocument(request, response);
            return document;*/
        }

        /// <summary>
        /// Executes get request
        /// </summary>
        /// <param name="request"></param>
        /// <param name="httpMethod"></param>
        /// <returns></returns>
        public IDocument ExecuteAsGet(IRestRequest request, string httpMethod)
        {
            return _browserStandard.ExecuteAsGet(request, httpMethod);
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
            return _browserStandard.ExecuteAsPost(request, httpMethod);
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
        public Task<IDocument> ExecuteTaskAsync(IRestRequest request, CancellationToken token)
        {
            return _browserStandard.ExecuteTaskAsync(request, token);
            Task<IRestResponse> responseTask = _restClient.ExecuteTaskAsync(request, token);
            Task<IDocument> documentTask = PackageAndAddDocumentAsync(request, responseTask);
            return documentTask;
        }

        /// <summary>
        /// Executes request asynchronously
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public Task<IDocument> ExecuteTaskAsync(IRestRequest request)
        {
            return _browserStandard.ExecuteTaskAsync(request);
            Task<IRestResponse> responseTask = _restClient.ExecuteTaskAsync(request);
            Task<IDocument> documentTask = PackageAndAddDocumentAsync(request, responseTask);
            return documentTask;
        }

        /// <summary>
        /// Executes get request asynchronously
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public Task<IDocument> ExecuteGetTaskAsync(IRestRequest request)
        {
            return _browserStandard.ExecuteGetTaskAsync(request);
            Task<IRestResponse> responseTask = _restClient.ExecuteGetTaskAsync(request);
            Task<IDocument> documentTask = PackageAndAddDocumentAsync(request, responseTask);
            return documentTask;
        }

        /// <summary>
        /// Executes get request asynchronously
        /// </summary>
        /// <param name="request"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public Task<IDocument> ExecuteGetTaskAsync(IRestRequest request, CancellationToken token)
        {
            return _browserStandard.ExecuteGetTaskAsync(request, token);
            Task<IRestResponse> responseTask = _restClient.ExecuteGetTaskAsync(request, token);
            Task<IDocument> documentTask = PackageAndAddDocumentAsync(request, responseTask);
            return documentTask;
        }

        /// <summary>
        /// Executes post request asynchronously
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public Task<IDocument> ExecutePostTaskAsync(IRestRequest request)
        {
            return _browserStandard.ExecutePostTaskAsync(request);
            Task<IRestResponse> responseTask = _restClient.ExecutePostTaskAsync(request);
            Task<IDocument> documentTask = PackageAndAddDocumentAsync(request, responseTask);
            return documentTask;
        }

        /// <summary>
        /// Executes post request asynchronously
        /// </summary>
        /// <param name="request"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public Task<IDocument> ExecutePostTaskAsync(IRestRequest request, CancellationToken token)
        {
            return _browserStandard.ExecutePostTaskAsync(request, token);
            Task<IRestResponse> responseTask = _restClient.ExecutePostTaskAsync(request, token);
            Task<IDocument> documentTask = PackageAndAddDocumentAsync(request, responseTask);
            return documentTask;
        }

        /// <summary>
        /// Performs get request
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        public IDocument Navigate(string uri)
        {
            return _browserStandard.Navigate(uri);
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
            return _browserStandard.Navigate(uri);
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
            return _browserStandard.Navigate(uri, headers);
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
            return _browserStandard.Navigate(uri, headers);
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
            return _browserStandard.Navigate(uri, headers, formData);
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
            return _browserStandard.Navigate(uri, headers, formData);
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
            return await _browserStandard.NavigateAsync(uri);
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
            return await _browserStandard.NavigateAsync(uri);
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
            return await _browserStandard.NavigateAsync(uri, headers);
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
            return await _browserStandard.NavigateAsync(uri, headers);
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
            return await _browserStandard.NavigateAsync(uri, headers, formData);
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
            return await _browserStandard.NavigateAsync(uri, headers, formData);
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
            return _browserStandard.Submit(uri);
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
            return _browserStandard.Submit(uri);
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
            return _browserStandard.Submit(uri, formData);
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
            return _browserStandard.Submit(uri, formData);
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
            return _browserStandard.Submit(uri, formData, headers);
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
            return _browserStandard.Submit(uri, formData, headers);
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
            return await _browserStandard.SubmitAsync(uri);
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
            return await _browserStandard.SubmitAsync(uri);
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
            return await _browserStandard.SubmitAsync(uri, formData);
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
            return await _browserStandard.SubmitAsync(uri, formData);
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
            return await _browserStandard.SubmitAsync(uri, formData, headers);
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
            return await _browserStandard.SubmitAsync(uri, formData, headers);
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
            return _browserStandard.Back();
        }

        /// <summary>
        /// Method for navigating to last browser state
        /// </summary>
        /// <param name="useCache">Determines whether to re-issue request or reload last document</param>
        public IDocument Back(bool useCache)
        {
            return _browserStandard.Back(useCache);
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
            return await _browserStandard.BackAsync();
        }

        /// <summary>
        /// Method for navigating to last browser state asynchronously
        /// </summary>
        /// <param name="useCache">Determines whether to re-issue request or reload last document</param>
        public async Task<IDocument> BackAsync(bool useCache)
        {
            return await _browserStandard.BackAsync(useCache);
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
            return _browserStandard.Forward();
            return Forward(false);
        }

        /// <summary>
        /// Navigate to next document in forward history
        /// </summary>
        /// <param name="useCache"></param>
        /// <returns></returns>
        public IDocument Forward(bool useCache)
        {
            return _browserStandard.Forward(useCache);
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
            return await _browserStandard.ForwardAsync();
            return await ForwardAsync(false);
        }

        /// <summary>
        /// Navigate to next document in forward history asynchronously
        /// </summary>
        /// <param name="useCache"></param>
        /// <returns></returns>
        public async Task<IDocument> ForwardAsync(bool useCache)
        {
            return await _browserStandard.ForwardAsync(useCache);
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
            return _browserStandard.Refresh();
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
            return await _browserStandard.RefreshAsync();
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
            return _browserStandard.SubmitForm(form);
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
            return _browserStandard.SubmitForm(form,headers);
            if (form.Method.ToLower() ==  "get")
                return Navigate(form.Action, headers, form.FormValues);
            else if (form.Method.ToLower() ==  "post")
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
            return await _browserStandard.SubmitFormAsync(form);
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
            return await _browserStandard.SubmitFormAsync(form, headers);
            if (form.Method.ToLower() ==  "get")
                return await NavigateAsync(form.Action, headers, form.FormValues);
            else if (form.Method.ToLower() ==  "post")
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
            if(JavascriptScrapingEnabled)
                JavascriptEngine.Add(document);
            if(StyleScrapingEnabled)
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
            if(JavascriptScrapingEnabled)
                result = JavascriptEngine.AddAsync(document);
            if(StyleScrapingEnabled)
                styleResult = StyleEngine.AddAsync(document);
            
            if(JavascriptScrapingEnabled && result != null)
                await result;
            if(StyleScrapingEnabled && styleResult != null)
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
        protected void AddFormData(IRestRequest request, Dictionary<string,string> formData)
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
        protected void AddHeaders(IRestRequest request, Dictionary<string,string> headers)
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