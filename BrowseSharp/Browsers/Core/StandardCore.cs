using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using BrowseSharp.History;
using BrowseSharp.Html;
using BrowseSharp.Javascript;
using BrowseSharp.Style;
using BrowseSharp.Toolbox;
using RestSharp;

namespace BrowseSharp.Browsers.Core
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
        public StandardCore(JavascriptEngine javascriptEngine, StyleEngine styleEngine, 
            RestClient restClient, CookieContainer cookieContainer, 
            HistoryManager historyManager, bool styleScrapingEnabled, 
            bool javascriptScrapingEnabled, string defaultUriProtocol): 
            base(javascriptEngine, styleEngine,restClient,
                cookieContainer,historyManager,styleScrapingEnabled,
                javascriptScrapingEnabled,defaultUriProtocol)
        {
            
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
        /// Gets current document from the current request
        /// </summary>
        /// <returns>Current Document</returns>
        public IDocument Document => _history.Document;

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