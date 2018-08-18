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
using BrowseSharp.Javascript;
using BrowseSharp.Style;
using Jint.Parser;
using RestSharp;
using RestSharp.Authenticators;
using RestSharp.Deserializers;

namespace BrowseSharp
{
    /// <summary>
    /// Headless browser implimentation that creates documents for each web request.
    /// </summary>
    public class Browser : IBrowser
    {
        /// <summary>
        /// Default Constructor
        /// </summary>
        public Browser()
        {
            _documents = new List<IDocument>();
            JavascriptEngine = new JavascriptEngine();
            StyleEngine = new StyleEngine();
            _restClient = new RestClient();
            _restClient.CookieContainer = new CookieContainer();
            _forwardHistory = new List<IDocument>();
            MaxHistorySize = -1;
        }

        /// <summary>
        /// List of documents stored from each request made
        /// </summary>
        public List<IDocument> Documents
        {
            get { return _documents; }
        }
        
        /// <summary>
        /// Javascript engine used by browser
        /// </summary>
        public JavascriptEngine JavascriptEngine { get; }
        
        /// <summary>
        /// Style engine for parsing css styles 
        /// </summary>
        public StyleEngine StyleEngine { get; }
        
        /// <summary>
        /// Rest sharp http client for making web requests
        /// </summary>
        private RestClient _restClient;

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
        public Task<IDocument> ExecuteTaskAsync(IRestRequest request, CancellationToken token)
        {
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
            Uri requestUri = new Uri(uri);
            return Navigate(requestUri);
        }
        
        /// <summary>
        /// Performs get request
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        public IDocument Navigate(Uri uri)
        {
            ClearForwardHistory();
            TrimHistory(true);
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
            Uri requestUri = new Uri(uri);
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
            ClearForwardHistory();
            TrimHistory(true);
            _restClient.BaseUrl = uri;
            RestRequest request = new RestRequest();
            AddHeaders(request,headers);
            return ExecuteAsGet(request,"GET");
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
            Uri requestUri = new Uri(uri);
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
            ClearForwardHistory();
            TrimHistory(true);
            _restClient.BaseUrl = uri;
            RestRequest request = new RestRequest();
            AddHeaders(request,headers);
            AddFormData(request, formData);
            return ExecuteAsGet(request,"GET");
        }
        
        /// <summary>
        /// Performs get request asynchronously
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        public async Task<IDocument> NavigateAsync(string uri)
        {
            Uri requestUri = new Uri(uri);
            return await NavigateAsync(requestUri);
        }
        
        /// <summary>
        /// Performs get request asynchronously
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        public async Task<IDocument> NavigateAsync(Uri uri)
        {
            ClearForwardHistory();
            TrimHistory(true);
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
            Uri requestUri = new Uri(uri);
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
            ClearForwardHistory();
            TrimHistory(true);
            _restClient.BaseUrl = uri;
            RestRequest request = new RestRequest();
            AddHeaders(request,headers);
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
            Uri requestUri = new Uri(uri);
            return await NavigateAsync(requestUri, headers, formData);
        }

        /// <summary>
        /// Performs get request asynchronously
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="headers"></param>
        /// <param name="formData"></param>
        /// <returns></returns>
        public async Task<IDocument> NavigateAsync(Uri uri, Dictionary<string, string> headers, Dictionary<string, string> formData)
        {
            ClearForwardHistory();
            TrimHistory(true);
            _restClient.BaseUrl = uri;
            RestRequest request = new RestRequest();
            AddHeaders(request,headers);
            AddFormData(request,formData);
            return await ExecuteTaskAsync(request);
        }
        
        /// <summary>
        /// Performs a post request
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        public IDocument Submit(string uri)
        {
            Uri requestUri = new Uri(uri);
            return Submit(requestUri);
        }

        /// <summary>
        /// Performs a post request
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        public IDocument Submit(Uri uri)
        {
            ClearForwardHistory();
            TrimHistory(true);
            _restClient.BaseUrl = uri;
            RestRequest request = new RestRequest();
            return ExecuteAsPost(request,"POST"); /* TODO: Check this */
        }
        
        /// <summary>
        /// Performs a post request
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="formData"></param>
        /// <returns></returns>
        public IDocument Submit(string uri, Dictionary<string, string> formData)
        {
            Uri requestUri = new Uri(uri);
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
            ClearForwardHistory();
            TrimHistory(true);
            _restClient.BaseUrl = uri;
            RestRequest request = new RestRequest();
            AddFormData(request, formData);
            return ExecuteAsPost(request,"POST");
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
            Uri requestUri = new Uri(uri);
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
            ClearForwardHistory();
            TrimHistory(true);
            _restClient.BaseUrl = uri;
            RestRequest request = new RestRequest();
            AddFormData(request, formData);
            AddHeaders(request,headers);
            return ExecuteAsPost(request,"POST");
        }

        /// <summary>
        /// Performs a post request asynchronously
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        public async Task<IDocument> SubmitAsync(string uri)
        {
            Uri requestUri = new Uri(uri);
            return await SubmitAsync(requestUri);
        }

        /// <summary>
        /// Performs a post request asynchronously
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        public async Task<IDocument> SubmitAsync(Uri uri)
        {
            ClearForwardHistory();
            TrimHistory(true);
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
            Uri requestUri = new Uri(uri);
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
            ClearForwardHistory();
            TrimHistory(true);
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
            Uri requestUri = new Uri(uri);
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
            ClearForwardHistory();
            TrimHistory(true);
            _restClient.BaseUrl = uri;
            RestRequest request = new RestRequest();
            AddFormData(request, formData);
            AddHeaders(request, headers);
            return await ExecutePostTaskAsync(request);
        }

        /// <summary>
        /// Contains all previous documents stored for each previous request
        /// </summary>
        public List<IDocument> History => Documents;
        
        /// <summary>
        /// Stores the forward history when the back method is called 
        /// </summary>
        public List<IDocument> ForwardHistory
        {
            get { return _forwardHistory; }
        }
        
        /// <summary>
        /// Clears browse history by re-initializing Documents
        /// </summary>
        public void ClearHistory()
        {
            _documents = new List<IDocument>();
            ClearForwardHistory();
        }

        /// <summary>
        /// Clears forward history
        /// </summary>
        public void ClearForwardHistory()
        {
            if (_forwardHistory == null)
            {
                _forwardHistory = new List<IDocument>();
                return;
            }

            if (_forwardHistory.Count > 0)
            {
                _forwardHistory.Clear();
            }
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
            ForwardHistory.Push(History.Pop());
            if (useCache)
                return History.Last();
            
            IDocument oldDocument = History.Pop();
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
            ForwardHistory.Push(History.Pop());
            if (useCache)
                return History.Last();
            
            IDocument oldDocument = History.Pop();
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
            if (_forwardHistory.Count < 1)
                return History.LastOrDefault();

            if (useCache)
            {
                History.Push(_forwardHistory.Pop());
                return Document();
            }
                
            IDocument forwardDocument = _forwardHistory.Pop();
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
            if (_forwardHistory.Count < 1)
                return History.LastOrDefault();

            if (useCache)
            {
                History.Push(_forwardHistory.Pop());
                return Document();
            }
                
            IDocument forwardDocument = _forwardHistory.Pop();
            return await ExecuteTaskAsync(forwardDocument.Request);

        }

        /// <summary>
        /// Max amount of history/Documents to be stored by the browser (-1 for no limit)
        /// </summary>
        public int MaxHistorySize { get; set; }

        /// <summary>
        /// Refresh page, re-submits last request
        /// </summary>
        /// <returns></returns>
        public IDocument Refresh()
        {
            IDocument oldDocument = History.Pop();
            return Execute(oldDocument.Request);
        }

        /// <summary>
        /// Refresh page, re-submits last request asynchronously
        /// </summary>
        /// <returns></returns>
        public async Task<IDocument> RefreshAsync()
        {
            IDocument oldDocument = History.Pop();
            return await ExecuteTaskAsync(oldDocument.Request);
        }

        /// <summary>
        /// Gets current document from the current request
        /// </summary>
        /// <returns>Current Document</returns>
        public IDocument Document()
        {
            return Documents.Last();
        }

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
        private IDocument PackageAndAddDocument(IRestRequest request, IRestResponse response)
        {
            HtmlParser parser = new HtmlParser();
            IHtmlDocument htmlDocument = parser.Parse(response.Content);
            IDocument document = new Document(request, response, htmlDocument);
            JavascriptEngine.Add(document);
            StyleEngine.Add(document);
            Documents.Add(document);
            return document;
        }

        /// <summary>
        /// Creates a document from a request and response asynchronously
        /// </summary>
        /// <param name="request"></param>
        /// <param name="responseTask"></param>
        /// <returns></returns>
        private async Task<IDocument> PackageAndAddDocumentAsync(IRestRequest request, Task<IRestResponse> responseTask)
        {
            IRestResponse response = await responseTask;
            HtmlParser parser = new HtmlParser();
            IHtmlDocument htmlDocument = parser.Parse(response.Content);
            IDocument document = new Document(request, response, htmlDocument);
            Task<int> result = JavascriptEngine.AddAsync(document);
            Task<int> styleResult = StyleEngine.AddAsync(document);
            await result;
            await styleResult;
            Documents.Add(document);
            return document;
        }

        /// <summary>
        /// Adds form data to the request input
        /// </summary>
        /// <param name="request"></param>
        /// <param name="formData"></param>
        private void AddFormData(IRestRequest request, Dictionary<string,string> formData)
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
        private void AddHeaders(IRestRequest request, Dictionary<string,string> headers)
        {
            if (headers == null)
                return;
            
            foreach (var formInput in headers)
            {
                request.AddHeader(formInput.Key, formInput.Value);
            }
        }

        /// <summary>
        /// Trims history to the max history size
        /// </summary>
        /// <param name="beforeNavigate">Indicates whether method is called before or after navigation</param>
        private void TrimHistory(bool beforeNavigate)
        {
            if (MaxHistorySize < 0)
                return;
            
            if(beforeNavigate)
            {
                while(History.Count >= MaxHistorySize)
                    History.RemoveAt(0);    
            }
            else
            {
                while(History.Count > MaxHistorySize)
                    History.RemoveAt(0);
            }   
        }

    }
}