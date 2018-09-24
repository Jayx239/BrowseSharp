using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BrowseSharp.Browsers;
using BrowseSharp.Browsers.Core;
using BrowseSharp.Html;
using RestSharp;

namespace BrowseSharp
{
    /// <summary>
    /// Headless browser implimentation that creates documents for each web request.
    /// </summary>
    public class Browser : BrowserCore, IBrowser, IBrowserTyped, IBrowserHistorySync
    {
        /// <summary>
        /// Default Constructor
        /// </summary>
        public Browser(): base()
        {
            _browserStandard = new StandardCore(JavascriptEngine, StyleEngine, _restClient, _restClient.CookieContainer, _history, StyleScrapingEnabled, JavascriptScrapingEnabled, DefaultUriProtocol);
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
                _browserTyped.JavascriptScrapingEnabled = value;
            }
        }

        /// <summary>
        /// Enables or disables style scraping on each request, sets for all browsers
        /// </summary>
        public override bool StyleScrapingEnabled { get { return _styleScrapingEnabled; }
            set {
                _styleScrapingEnabled = value;
                _browserStandard.StyleScrapingEnabled = value;
                _browserTyped.StyleScrapingEnabled = value;
            }
        }

        /// <summary>
        /// Executes a request
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public IDocument Execute(IRestRequest request)
        {
            return _browserStandard.Execute(request);
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
        }

        /// <summary>
        /// Executes request asynchronously
        /// </summary>
        /// <param name="request"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<IDocument> ExecuteTaskAsync(IRestRequest request, CancellationToken token)
        {
            return await _browserStandard.ExecuteTaskAsync(request, token);
        }

        /// <summary>
        /// Executes request asynchronously
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<IDocument> ExecuteTaskAsync(IRestRequest request)
        {
            return await _browserStandard.ExecuteTaskAsync(request);
        }

        /// <summary>
        /// Executes get request asynchronously
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<IDocument> ExecuteGetTaskAsync(IRestRequest request)
        {
            return await _browserStandard.ExecuteGetTaskAsync(request);
        }

        /// <summary>
        /// Executes get request asynchronously
        /// </summary>
        /// <param name="request"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<IDocument> ExecuteGetTaskAsync(IRestRequest request, CancellationToken token)
        {
            return await _browserStandard.ExecuteGetTaskAsync(request, token);
        }

        /// <summary>
        /// Executes post request asynchronously
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<IDocument> ExecutePostTaskAsync(IRestRequest request)
        {
            return await _browserStandard.ExecutePostTaskAsync(request);
        }

        /// <summary>
        /// Executes post request asynchronously
        /// </summary>
        /// <param name="request"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<IDocument> ExecutePostTaskAsync(IRestRequest request, CancellationToken token)
        {
            return await _browserStandard.ExecutePostTaskAsync(request, token);
        }

        /// <summary>
        /// Performs get request
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        public IDocument Navigate(string uri)
        {
            return _browserStandard.Navigate(uri);
        }

        /// <summary>
        /// Performs get request
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        public IDocument Navigate(Uri uri)
        {
            return _browserStandard.Navigate(uri);
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
        }

        /// <summary>
        /// Performs get request asynchronously
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        public async Task<IDocument> NavigateAsync(string uri)
        {
            return await _browserStandard.NavigateAsync(uri);
        }

        /// <summary>
        /// Performs get request asynchronously
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        public async Task<IDocument> NavigateAsync(Uri uri)
        {
            return await _browserStandard.NavigateAsync(uri);
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
        }

        /// <summary>
        /// Performs a post request
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        public IDocument Submit(string uri)
        {
            return _browserStandard.Submit(uri);
        }

        /// <summary>
        /// Performs a post request
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        public IDocument Submit(Uri uri)
        {
            return _browserStandard.Submit(uri);
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
        }

        

        /// <summary>
        /// Performs a post request asynchronously
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        public async Task<IDocument> SubmitAsync(string uri)
        {
            return await _browserStandard.SubmitAsync(uri);
        }

        /// <summary>
        /// Performs a post request asynchronously
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        public async Task<IDocument> SubmitAsync(Uri uri)
        {
            return await _browserStandard.SubmitAsync(uri);
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
        }

        /// <summary>
        /// Navigate to next document in forward history
        /// </summary>
        /// <returns></returns>
        public IDocument Forward()
        {
            return _browserStandard.Forward();
        }

        /// <summary>
        /// Navigate to next document in forward history
        /// </summary>
        /// <param name="useCache"></param>
        /// <returns></returns>
        public IDocument Forward(bool useCache)
        {
            return _browserStandard.Forward(useCache);
        }

        /// <summary>
        /// Navigate to next document in forward history asynchronously
        /// </summary>
        /// <returns></returns>
        public async Task<IDocument> ForwardAsync()
        {
            return await _browserStandard.ForwardAsync();
        }

        /// <summary>
        /// Navigate to next document in forward history asynchronously
        /// </summary>
        /// <param name="useCache"></param>
        /// <returns></returns>
        public async Task<IDocument> ForwardAsync(bool useCache)
        {
            return await _browserStandard.ForwardAsync(useCache);
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
        }

        /// <summary>
        /// Refresh page, re-submits last request asynchronously
        /// </summary>
        /// <returns></returns>
        public async Task<IDocument> RefreshAsync()
        {
            return await _browserStandard.RefreshAsync();
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
            return _browserStandard.SubmitForm(form);
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
        }
        
        /// <summary>
        /// Submits a form asynchronously
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        public async Task<IDocument> SubmitFormAsync(Form form)
        {
            return await _browserStandard.SubmitFormAsync(form);
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
        }

        public IDocument<T> Execute<T>(IRestRequest request)
        {
            return _browserTyped.Execute<T>(request);
        }

        public IDocument<T> ExecuteAsGet<T>(IRestRequest request, string httpMethod)
        {
            return _browserTyped.ExecuteAsGet<T>(request, httpMethod);
        }

        public IDocument<T> ExecuteAsPost<T>(IRestRequest request, string httpMethod)
        {
            return _browserTyped.ExecuteAsPost<T>(request, httpMethod);
        }

        public async Task<IDocument<T>> ExecuteTaskAsync<T>(IRestRequest request, CancellationToken token)
        {
            return await _browserTyped.ExecuteTaskAsync<T>(request, token);
        }

        public async Task<IDocument<T>> ExecuteTaskAsync<T>(IRestRequest request)
        {
            return await _browserTyped.ExecuteTaskAsync<T>(request);
        }

        public async Task<IDocument<T>> ExecuteGetTaskAsync<T>(IRestRequest request)
        {
            return await _browserTyped.ExecuteGetTaskAsync<T>(request);
        }

        public async Task<IDocument<T>> ExecuteGetTaskAsync<T>(IRestRequest request, CancellationToken token)
        {
            return await _browserTyped.ExecuteGetTaskAsync<T>(request, token);
        }

        public async Task<IDocument<T>> ExecutePostTaskAsync<T>(IRestRequest request)
        {
            return await _browserTyped.ExecutePostTaskAsync<T>(request);
        }

        public async Task<IDocument<T>> ExecutePostTaskAsync<T>(IRestRequest request, CancellationToken token)
        {
            return await _browserTyped.ExecutePostTaskAsync<T>(request, token);
        }

        public IDocument<T> Navigate<T>(string uri)
        {
            return _browserTyped.Navigate<T>(uri);
        }

        public IDocument<T> Navigate<T>(Uri uri)
        {
            return _browserTyped.Navigate<T>(uri);
        }

        public IDocument<T> Navigate<T>(string uri, Dictionary<string, string> headers)
        {
            return _browserTyped.Navigate<T>(uri, headers);
        }

        public IDocument<T> Navigate<T>(Uri uri, Dictionary<string, string> headers)
        {
            return _browserTyped.Navigate<T>(uri, headers);
        }

        public IDocument<T> Navigate<T>(string uri, Dictionary<string, string> headers, Dictionary<string, string> formData)
        {
            return _browserTyped.Navigate<T>(uri, headers, formData);
        }

        public IDocument<T> Navigate<T>(Uri uri, Dictionary<string, string> headers, Dictionary<string, string> formData)
        {
            return _browserTyped.Navigate<T>(uri, headers, formData);
        }

        public async Task<IDocument<T>> NavigateAsync<T>(string uri)
        {
            return await _browserTyped.NavigateAsync<T>(uri);
        }

        public async Task<IDocument<T>> NavigateAsync<T>(Uri uri)
        {
            return await _browserTyped.NavigateAsync<T>(uri);
        }

        public async Task<IDocument<T>> NavigateAsync<T>(string uri, Dictionary<string, string> headers)
        {
            return await _browserTyped.NavigateAsync<T>(uri, headers);
        }

        public async Task<IDocument<T>> NavigateAsync<T>(Uri uri, Dictionary<string, string> headers)
        {
            return await _browserTyped.NavigateAsync<T>(uri, headers);
        }

        public async Task<IDocument<T>> NavigateAsync<T>(string uri, Dictionary<string, string> headers, Dictionary<string, string> formData)
        {
            return await _browserTyped.NavigateAsync<T>(uri, headers, formData);
        }

        public async Task<IDocument<T>> NavigateAsync<T>(Uri uri, Dictionary<string, string> headers, Dictionary<string, string> formData)
        {
            return await _browserTyped.NavigateAsync<T>(uri, headers, formData);
        }

        public IDocument<T> Submit<T>(string uri)
        {
            return _browserTyped.Submit<T>(uri);
        }

        public IDocument<T> Submit<T>(Uri uri)
        {
            return _browserTyped.Submit<T>(uri);
        }

        public IDocument<T> Submit<T>(string uri, Dictionary<string, string> formData)
        {
            return _browserTyped.Submit<T>(uri, formData);
        }

        public IDocument<T> Submit<T>(Uri uri, Dictionary<string, string> formData)
        {
            return _browserTyped.Submit<T>(uri, formData);
        }

        public IDocument<T> Submit<T>(string uri, Dictionary<string, string> formData, Dictionary<string, string> headers)
        {
            return _browserTyped.Submit<T>(uri, formData, headers);
        }

        public IDocument<T> Submit<T>(Uri uri, Dictionary<string, string> formData, Dictionary<string, string> headers)
        {
            return _browserTyped.Submit<T>(uri, formData, headers);
        }

        public IDocument<T> SubmitForm<T>(Form form)
        {
            return _browserTyped.SubmitForm<T>(form);
        }

        public IDocument<T> SubmitForm<T>(Form form, Dictionary<string, string> headers)
        {
            return _browserTyped.SubmitForm<T>(form, headers);
        }

        public async Task<IDocument<T>> SubmitAsync<T>(string uri)
        {
            return await _browserTyped.SubmitAsync<T>(uri);
        }

        public async Task<IDocument<T>> SubmitAsync<T>(Uri uri)
        {
            return await _browserTyped.SubmitAsync<T>(uri);
        }

        public async Task<IDocument<T>> SubmitAsync<T>(string uri, Dictionary<string, string> formData)
        {
            return await _browserTyped.SubmitAsync<T>(uri, formData);
        }

        public async Task<IDocument<T>> SubmitAsync<T>(Uri uri, Dictionary<string, string> formData)
        {
            return await _browserTyped.SubmitAsync<T>(uri, formData);
        }

        public async Task<IDocument<T>> SubmitAsync<T>(string uri, Dictionary<string, string> formData, Dictionary<string, string> headers)
        {
            return await _browserTyped.SubmitAsync<T>(uri, formData, headers);
        }

        public async Task<IDocument<T>> SubmitAsync<T>(Uri uri, Dictionary<string, string> formData, Dictionary<string, string> headers)
        {
            return await _browserTyped.SubmitAsync<T>(uri, formData, headers);
        }

        public async Task<IDocument<T>> SubmitFormAsync<T>(Form form)
        {
            return await _browserTyped.SubmitFormAsync<T>(form);
        }

        public async Task<IDocument<T>> SubmitFormAsync<T>(Form form, Dictionary<string, string> headers)
        {
            return await _browserTyped.SubmitFormAsync<T>(form, headers);
        }
    }
}