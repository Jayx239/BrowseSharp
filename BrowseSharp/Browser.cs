using System;
using System.Collections.Generic;
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
using RestSharp;
using RestSharp.Authenticators;
using RestSharp.Deserializers;

namespace BrowseSharp
{
    public class Browser : IBrowser
    {
        public Browser()
        {
            Documents = new List<IDocument>();
            JavascriptEngine = new JavascriptEngine();
            StyleEngine = new StyleEngine();
            _restClient = new RestClient();
            _restClient.CookieContainer = new CookieContainer();
        }

        public List<IDocument> Documents { get; }
        public JavascriptEngine JavascriptEngine { get; }
        public StyleEngine StyleEngine { get; }
        private RestClient _restClient;

        public CookieContainer CookieContainer
        {
            get { return _restClient.CookieContainer; }
            set { _restClient.CookieContainer = value; }
        }

        public bool AutomaticDecompression
        {
            get { return _restClient.AutomaticDecompression; }
            set { _restClient.AutomaticDecompression = value; }
        }

        public int? MaxRedirects
        {
            get { return _restClient.MaxRedirects; }
            set { _restClient.MaxRedirects = value; }
        }

        public string UserAgent
        {
            get { return _restClient.UserAgent; }
            set { _restClient.UserAgent = value; }
        }

        public int Timeout
        {
            get { return _restClient.Timeout; }
            set { _restClient.Timeout = value; }
        }

        public int ReadWriteTimeout
        {
            get { return _restClient.ReadWriteTimeout; }
            set { _restClient.ReadWriteTimeout = value; }
        }

        public bool UseSynchronizationContext
        {
            get { return _restClient.UseSynchronizationContext; }
            set { _restClient.UseSynchronizationContext = value; }
        }

        public IAuthenticator Authenticator
        {
            get { return _restClient.Authenticator; }
            set { _restClient.Authenticator = value; }
        }

        public Uri BaseUrl
        {
            get { return _restClient.BaseUrl; }
            set { _restClient.BaseUrl = value; }
        }

        public Encoding Encoding
        {
            get { return _restClient.Encoding; }
            set { _restClient.Encoding = value; }
        }

        public bool PreAuthenticate
        {
            get { return _restClient.PreAuthenticate; }
            set { _restClient.PreAuthenticate = value; }
        }

        public IList<Parameter> DefaultParameters
        {
            get { return _restClient.DefaultParameters; }
        }

        public string BaseHost
        {
            get { return _restClient.BaseHost; }
            set { _restClient.BaseHost = value; }
        }

        public IRestResponse<T> Deserialize<T>(IRestResponse response)
        {
            return _restClient.Deserialize<T>(response);
        }

        public byte[] DownloadData(IRestRequest request)
        {
            return _restClient.DownloadData(request);
        }

        public X509CertificateCollection ClientCertificates
        {
            get { return _restClient.ClientCertificates; }
            set { _restClient.ClientCertificates = value; }
        }

        public IWebProxy Proxy
        {
            get { return _restClient.Proxy; }
            set { _restClient.Proxy = value; }
        }

        public RequestCachePolicy CachePolicy
        {
            get { return _restClient.CachePolicy; }
            set { _restClient.CachePolicy = value; }
        }

        public bool Pipelined
        {
            get { return _restClient.Pipelined; }
            set { _restClient.Pipelined = value; }
        }

        public bool FollowRedirects
        {
            get { return _restClient.FollowRedirects; }
            set { _restClient.FollowRedirects = value; }
        }

        public Uri BuildUri(IRestRequest request)
        {
            return _restClient.BuildUri(request);
        }

        public RemoteCertificateValidationCallback RemoteCertificateValidationCallback
        {
            get { return _restClient.RemoteCertificateValidationCallback; }
            set { _restClient.RemoteCertificateValidationCallback = value; }
        }

        public void AddHandler(string contentType, IDeserializer deserializer)
        {
            _restClient.AddHandler(contentType, deserializer);
        }

        public void RemoveHandler(string contentType)
        {
            _restClient.RemoveHandler(contentType);
        }

        public void ClearHandlers()
        {
            _restClient.ClearHandlers();
        }

        public IDocument Execute(IRestRequest request)
        {
            IRestResponse response = _restClient.Execute(request);
            IDocument document = PackageAndAddDocument(request, response);
            return document;
        }

        public IDocument ExecuteAsGet(IRestRequest request, string httpMethod)
        {
            IRestResponse response = _restClient.ExecuteAsGet(request, httpMethod);
            IDocument document = PackageAndAddDocument(request, response);
            return document;
        }

        public IDocument ExecuteAsPost(IRestRequest request, string httpMethod)
        {
            IRestResponse response = _restClient.ExecuteAsPost(request, httpMethod);
            IDocument document = PackageAndAddDocument(request, response);
            return document;
        }

        public Task<IDocument> ExecuteTaskAsync(IRestRequest request, CancellationToken token)
        {
            Task<IRestResponse> responseTask = _restClient.ExecuteTaskAsync(request, token);
            Task<IDocument> documentTask = PackageAndAddDocumentAsync(request, responseTask);
            return documentTask;
        }

        public Task<IDocument> ExecuteTaskAsync(IRestRequest request)
        {
            Task<IRestResponse> responseTask = _restClient.ExecuteTaskAsync(request);
            Task<IDocument> documentTask = PackageAndAddDocumentAsync(request, responseTask);
            return documentTask;
        }

        public Task<IDocument> ExecuteGetTaskAsync(IRestRequest request)
        {
            Task<IRestResponse> responseTask = _restClient.ExecuteGetTaskAsync(request);
            Task<IDocument> documentTask = PackageAndAddDocumentAsync(request, responseTask);
            return documentTask;
        }

        public Task<IDocument> ExecuteGetTaskAsync(IRestRequest request, CancellationToken token)
        {
            Task<IRestResponse> responseTask = _restClient.ExecuteGetTaskAsync(request, token);
            Task<IDocument> documentTask = PackageAndAddDocumentAsync(request, responseTask);
            return documentTask;
        }

        public Task<IDocument> ExecutePostTaskAsync(IRestRequest request)
        {
            Task<IRestResponse> responseTask = _restClient.ExecutePostTaskAsync(request);
            Task<IDocument> documentTask = PackageAndAddDocumentAsync(request, responseTask);
            return documentTask;
        }

        public Task<IDocument> ExecutePostTaskAsync(IRestRequest request, CancellationToken token)
        {
            Task<IRestResponse> responseTask = _restClient.ExecutePostTaskAsync(request, token);
            Task<IDocument> documentTask = PackageAndAddDocumentAsync(request, responseTask);
            return documentTask;
        }

        public IDocument Navigate(string uri)
        {
            Uri requestUri = new Uri(uri);
            return Navigate(requestUri);
        }
        
        public IDocument Navigate(Uri uri)
        {
            _restClient.BaseUrl = uri;
            RestRequest request = new RestRequest();
            return Execute(request);
        }

        public IDocument Navigate(string uri, Dictionary<string, string> headers)
        {
            Uri requestUri = new Uri(uri);
            return Navigate(requestUri, headers);
        }

        public IDocument Navigate(Uri uri, Dictionary<string, string> headers)
        {
            _restClient.BaseUrl = uri;
            RestRequest request = new RestRequest();
            AddHeaders(request,headers);
            return ExecuteAsGet(request,"GET");
        }

        public IDocument Navigate(string uri, Dictionary<string, string> headers, Dictionary<string, string> formData)
        {
            Uri requestUri = new Uri(uri);
            return Navigate(requestUri, headers, formData);
        }

        public IDocument Navigate(Uri uri, Dictionary<string, string> headers, Dictionary<string, string> formData)
        {
            _restClient.BaseUrl = uri;
            RestRequest request = new RestRequest();
            AddHeaders(request,headers);
            AddFormData(request, formData);
            return ExecuteAsGet(request,"GET");
        }
        
        public async Task<IDocument> NavigateAsync(string uri)
        {
            Uri requestUri = new Uri(uri);
            return await NavigateAsync(requestUri);
        }
        
        public async Task<IDocument> NavigateAsync(Uri uri)
        {
            _restClient.BaseUrl = uri;
            RestRequest request = new RestRequest();
            return await ExecuteTaskAsync(request);
        }

        public async Task<IDocument> NavigateAsync(string uri, Dictionary<string, string> headers)
        {
            Uri requestUri = new Uri(uri);
            return Navigate(requestUri, headers);
        }

        public async Task<IDocument> NavigateAsync(Uri uri, Dictionary<string, string> headers)
        {
            _restClient.BaseUrl = uri;
            RestRequest request = new RestRequest();
            AddHeaders(request,headers);
            return await ExecuteTaskAsync(request);
        }

        public async Task<IDocument> NavigateAsync(string uri, Dictionary<string, string> headers,
            Dictionary<string, string> formData)
        {
            Uri requestUri = new Uri(uri);
            return Navigate(requestUri, headers, formData);
        }

        public async Task<IDocument> NavigateAsync(Uri uri, Dictionary<string, string> headers, Dictionary<string, string> formData)
        {
            _restClient.BaseUrl = uri;
            RestRequest request = new RestRequest();
            AddHeaders(request,headers);
            AddFormData(request,formData);
            return await ExecuteTaskAsync(request);
        }
        
        public IDocument Submit(string uri)
        {
            Uri requestUri = new Uri(uri);
            return Submit(requestUri);
        }

        public IDocument Submit(Uri uri)
        {
            _restClient.BaseUrl = uri;
            RestRequest request = new RestRequest();
            return ExecuteAsPost(request,"POST"); /* TODO: Check this */
        }
        
        public IDocument Submit(string uri, Dictionary<string, string> formData)
        {
            Uri requestUri = new Uri(uri);
            return Submit(requestUri, formData);
        }

        public IDocument Submit(Uri uri, Dictionary<string, string> formData)
        {
            _restClient.BaseUrl = uri;
            RestRequest request = new RestRequest();
            AddFormData(request, formData);
            return ExecuteAsPost(request,"POST");
        }
        
        public IDocument Submit(string uri, Dictionary<string, string> formData, Dictionary<string, string> headers)
        {
            Uri requestUri = new Uri(uri);
            return Submit(requestUri, formData, headers);
        }

        public IDocument Submit(Uri uri, Dictionary<string, string> formData, Dictionary<string, string> headers)
        {
            _restClient.BaseUrl = uri;
            RestRequest request = new RestRequest();
            AddFormData(request, formData);
            AddHeaders(request,headers);
            return ExecuteAsPost(request,"POST");
        }

        public async Task<IDocument> SubmitAsync(string uri)
        {
            Uri requestUri = new Uri(uri);
            return await SubmitAsync(requestUri);
        }

        public async Task<IDocument> SubmitAsync(Uri uri)
        {
            _restClient.BaseUrl = uri;
            RestRequest request = new RestRequest();
            return await ExecutePostTaskAsync(request);
        }

        public async Task<IDocument> SubmitAsync(string uri, Dictionary<string, string> formData)
        {
            Uri requestUri = new Uri(uri);
            return await SubmitAsync(requestUri, formData);
        }

        public async Task<IDocument> SubmitAsync(Uri uri, Dictionary<string, string> formData)
        {
            _restClient.BaseUrl = uri;
            RestRequest request = new RestRequest();
            AddFormData(request, formData);
            return await ExecutePostTaskAsync(request);
        }
        
        public async Task<IDocument> SubmitAsync(string uri, Dictionary<string, string> formData, Dictionary<string, string> headers)
        {
            Uri requestUri = new Uri(uri);
            return await SubmitAsync(requestUri, formData, headers);
        }

        public async Task<IDocument> SubmitAsync(Uri uri, Dictionary<string, string> formData, Dictionary<string, string> headers)
        {
            _restClient.BaseUrl = uri;
            RestRequest request = new RestRequest();
            AddFormData(request, formData);
            AddHeaders(request, headers);
            return await ExecutePostTaskAsync(request);
        }
        
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

        private void AddFormData(IRestRequest request, Dictionary<string,string> formData)
        {
            if (formData == null)
                return;
            
            foreach (var formInput in formData)
            {
                request.AddParameter(formInput.Key, formInput.Value);
            }
        }
        
        private void AddHeaders(IRestRequest request, Dictionary<string,string> headers)
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