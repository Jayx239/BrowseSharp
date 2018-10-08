using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Cache;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using AngleSharp.Dom.Html;
using AngleSharp.Parser.Html;
using BrowseSharp.History;
using BrowseSharp.Javascript;
using BrowseSharp.Style;
using RestSharp;
using RestSharp.Authenticators;
using RestSharp.Deserializers;

namespace BrowseSharp.Browsers.Core
{
    /// <summary>
    /// Headless browser core containing browser core attributes
    /// </summary>
    public class BrowserCore : IBrowserCore
    {
        /// <summary>
        /// Default Constructor
        /// </summary>
        public BrowserCore()
        {
            _javascriptEngine = new JavascriptEngine();
            _styleEngine = new StyleEngine();
            _restClient = new RestClient();
            _restClient.CookieContainer = new CookieContainer();
            _history = new HistoryManager();
            _styleScrapingEnabled = true;
            _javascriptScrapingEnabled = true;
            DefaultUriProtocol = "http";

        }

        /// <summary>
        /// Browser core secondary constructor
        /// </summary>
        /// <param name="javascriptEngine"></param>
        /// <param name="styleEngine"></param>
        /// <param name="restClient"></param>
        /// <param name="cookieContainer"></param>
        /// <param name="historyManager"></param>
        /// <param name="styleScrapingEnabled"></param>
        /// <param name="javascriptScrapingEnabled"></param>
        /// <param name="defaultUriProtocol"></param>
        public BrowserCore(JavascriptEngine javascriptEngine, StyleEngine styleEngine, 
            RestClient restClient, CookieContainer cookieContainer, 
            HistoryManager historyManager, bool styleScrapingEnabled, 
            bool javascriptScrapingEnabled, string defaultUriProtocol)
        {
            _javascriptEngine = javascriptEngine;
            _styleEngine = styleEngine;
            _restClient = restClient;
            _restClient.CookieContainer = cookieContainer;
            _history = historyManager;
            _styleScrapingEnabled = styleScrapingEnabled;
            _javascriptScrapingEnabled = javascriptScrapingEnabled;
            DefaultUriProtocol = defaultUriProtocol;
        }

        /// <summary>
        /// List of documents stored from each request made
        /// </summary>
        public List<IDocument> Documents
        {
            get { return _history.History; }
        }

        /// <summary>
        /// Javascript engine used by browser
        /// </summary>
        public virtual JavascriptEngine JavascriptEngine { get { return _javascriptEngine; } }

        /// <summary>
        /// Javascript engine used by browser
        /// </summary>
        protected JavascriptEngine _javascriptEngine;

        /// <summary>
        /// Style engine for parsing css styles 
        /// </summary>
        public virtual StyleEngine StyleEngine { get { return _styleEngine; } }

        /// <summary>
        /// Style engine for parsing css styles 
        /// </summary>
        protected StyleEngine _styleEngine;

        /// <summary>
        /// Rest sharp http client for making web requests
        /// </summary>
        protected RestClient _restClient;

        /// <summary>
        /// Attribute for managing documents
        /// </summary>
        protected HistoryManager _history;

        /// <summary>
        /// Enables or disables javascript scraping on each request
        /// </summary>
        public virtual bool JavascriptScrapingEnabled { get { return _javascriptScrapingEnabled; } set { _javascriptScrapingEnabled = value; } }

        private bool _javascriptScrapingEnabled;

        /// <summary>
        /// Enables or disables style scraping on each request
        /// </summary>
        public virtual bool StyleScrapingEnabled { get { return _styleScrapingEnabled; } set { _styleScrapingEnabled = value; } }

        private bool _styleScrapingEnabled;

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
                if (value == "http" || value == "https")
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