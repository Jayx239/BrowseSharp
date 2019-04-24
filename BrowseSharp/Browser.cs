using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BrowseSharp.Browsers.Core;
using BrowseSharp.Common;
using BrowseSharp.Common.History;
using BrowseSharp.Common.Html;
using RestSharp;

namespace BrowseSharp
{
    /// <summary>
    /// Headless browser implementation that creates documents for each web request.
    /// </summary>
    public class Browser : BrowserCore, IBrowser, IBrowserTyped, IHistorySync, IHistoryAsync
    {
        /// <summary>
        /// Default Constructor
        /// </summary>
        public Browser(): base()
        {
            _browserStandard = new StandardCore(base.JavascriptEngine, base.StyleEngine, _restClient, _restClient.CookieContainer, _history, base.StyleScrapingEnabled, base.JavascriptScrapingEnabled, DefaultUriProtocol);
            _browserTyped = new TypedCore(base.JavascriptEngine, base.StyleEngine, _restClient, _restClient.CookieContainer, _history, base.StyleScrapingEnabled, base.JavascriptScrapingEnabled, DefaultUriProtocol);

        }

        /// <summary>
        /// Standard browser client for making standard web requests
        /// </summary>
        private StandardCore _browserStandard;

        /// <summary>
        /// Browser for making typed requests, deserializes response body to typed objects
        /// </summary>
        private TypedCore _browserTyped;

        /// <summary>
        /// Enables or disables javascript scraping on each request, sets for all browsers
        /// </summary>
        public override bool JavascriptScrapingEnabled {
            get { return base.JavascriptScrapingEnabled; }
            set {
                base.JavascriptScrapingEnabled = value;
                _browserStandard.JavascriptScrapingEnabled = value;
                _browserTyped.JavascriptScrapingEnabled = value;
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
                _browserTyped.StyleScrapingEnabled = value;
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
        public virtual IDocument Navigate(string uri)
        {
            return _browserStandard.Navigate(uri);
        }

        /// <inheritdoc />
        public virtual IDocument Navigate(Uri uri)
        {
            return _browserStandard.Navigate(uri);
        }

        /// <inheritdoc />
        public virtual IDocument Navigate(string uri, Dictionary<string, string> headers)
        {
            return _browserStandard.Navigate(uri, headers);
        }

        /// <inheritdoc />
        public virtual IDocument Navigate(Uri uri, Dictionary<string, string> headers)
        {
            return _browserStandard.Navigate(uri, headers);
        }

        /// <inheritdoc />
        public virtual IDocument Navigate(string uri, Dictionary<string, string> headers, Dictionary<string, string> formData)
        {
            return _browserStandard.Navigate(uri, headers, formData);
        }

        /// <inheritdoc />
        public virtual IDocument Navigate(Uri uri, Dictionary<string, string> headers, Dictionary<string, string> formData)
        {
            return _browserStandard.Navigate(uri, headers, formData);
        }

        /// <inheritdoc />
        public virtual async Task<IDocument> NavigateAsync(string uri)
        {
            return await _browserStandard.NavigateAsync(uri);
        }

        /// <inheritdoc />
        public virtual async Task<IDocument> NavigateAsync(Uri uri)
        {
            return await _browserStandard.NavigateAsync(uri);
        }

        /// <inheritdoc />
        public virtual async Task<IDocument> NavigateAsync(string uri, Dictionary<string, string> headers)
        {
            return await _browserStandard.NavigateAsync(uri, headers);
        }

        /// <inheritdoc />
        public virtual async Task<IDocument> NavigateAsync(Uri uri, Dictionary<string, string> headers)
        {
            return await _browserStandard.NavigateAsync(uri, headers);
        }

        /// <inheritdoc />
        public virtual async Task<IDocument> NavigateAsync(string uri, Dictionary<string, string> headers,
            Dictionary<string, string> formData)
        {
            return await _browserStandard.NavigateAsync(uri, headers, formData);
        }

        /// <inheritdoc />
        public virtual async Task<IDocument> NavigateAsync(Uri uri, Dictionary<string, string> headers,
            Dictionary<string, string> formData)
        {
            return await _browserStandard.NavigateAsync(uri, headers, formData);
        }

        /// <inheritdoc />
        public virtual IDocument Submit(string uri)
        {
            return _browserStandard.Submit(uri);
        }

        /// <inheritdoc />
        public virtual IDocument Submit(Uri uri)
        {
            return _browserStandard.Submit(uri);
        }

        /// <inheritdoc />
        public virtual IDocument Submit(string uri, Dictionary<string, string> formData)
        {
            return _browserStandard.Submit(uri, formData);
        }

        /// <inheritdoc />
        public virtual IDocument Submit(Uri uri, Dictionary<string, string> formData)
        {
            return _browserStandard.Submit(uri, formData);
        }

        /// <inheritdoc />
        public virtual IDocument Submit(string uri, Dictionary<string, string> formData, Dictionary<string, string> headers)
        {
            return _browserStandard.Submit(uri, formData, headers);
        }

        /// <inheritdoc />
        public virtual IDocument Submit(Uri uri, Dictionary<string, string> formData, Dictionary<string, string> headers)
        {
            return _browserStandard.Submit(uri, formData, headers);
        }

        /// <inheritdoc />
        public virtual async Task<IDocument> SubmitAsync(string uri)
        {
            return await _browserStandard.SubmitAsync(uri);
        }

        /// <inheritdoc />
        public virtual async Task<IDocument> SubmitAsync(Uri uri)
        {
            return await _browserStandard.SubmitAsync(uri);
        }

        /// <inheritdoc />
        public virtual async Task<IDocument> SubmitAsync(string uri, Dictionary<string, string> formData)
        {
            return await _browserStandard.SubmitAsync(uri, formData);
        }

        /// <inheritdoc />
        public virtual async Task<IDocument> SubmitAsync(Uri uri, Dictionary<string, string> formData)
        {
            return await _browserStandard.SubmitAsync(uri, formData);
        }

        /// <inheritdoc />
        public virtual async Task<IDocument> SubmitAsync(string uri, Dictionary<string, string> formData, Dictionary<string, string> headers)
        {
            return await _browserStandard.SubmitAsync(uri, formData, headers);
        }

        /// <inheritdoc />
        public virtual async Task<IDocument> SubmitAsync(Uri uri, Dictionary<string, string> formData, Dictionary<string, string> headers)
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
        public virtual void ClearHistory()
        {
            _history.ClearHistory();
        }

        /// <summary>
        /// Clears forward history
        /// </summary>
        public virtual void ClearForwardHistory()
        {
            _history.ClearForwardHistory();
        }

        /// <summary>
        /// Method for navigating to last browser state by re-issuing the previous request
        /// </summary>
        public virtual IDocument Back()
        {
            return Back(false);
        }

        /// <summary>
        /// Method for navigating to last browser state
        /// </summary>
        /// <param name="useCache">Determines whether to re-issue request or reload last document</param>
        public virtual IDocument Back(bool useCache)
        {
            IDocument oldDocument = _history.Back(useCache);
            if (useCache)
                return oldDocument;

            _browserStandard.BaseUrl = oldDocument.RequestUri;
            return _browserStandard.Execute(oldDocument.Request);
        }


        /// <summary>
        /// Method for navigating to last browser state by re-issuing the previous request asynchronously
        /// </summary>
        public virtual async Task<IDocument> BackAsync()
        {
            return await BackAsync(false);
        }

        /// <summary>
        /// Method for navigating to last browser state asynchronously
        /// </summary>
        /// <param name="useCache">Determines whether to re-issue request or reload last document</param>
        public virtual async Task<IDocument> BackAsync(bool useCache)
        {
            IDocument oldDocument = _history.Back(useCache);
            if (useCache)
                return oldDocument;

            _browserStandard.BaseUrl = oldDocument.RequestUri;
            return await _browserStandard.ExecuteTaskAsync(oldDocument.Request);
        }

        /// <summary>
        /// Navigate to next document in forward history
        /// </summary>
        /// <returns></returns>
        public virtual IDocument Forward()
        {
            return Forward(false);
        }

        /// <summary>
        /// Navigate to next document in forward history
        /// </summary>
        /// <param name="useCache"></param>
        /// <returns></returns>
        public virtual IDocument Forward(bool useCache)
        {
            IDocument forwardDocument = _history.Forward(useCache);
            if (useCache)
                return forwardDocument;

            _browserStandard.BaseUrl = forwardDocument.RequestUri;
            return _browserStandard.Execute(forwardDocument.Request);
        }

        /// <summary>
        /// Navigate to next document in forward history asynchronously
        /// </summary>
        /// <returns></returns>
        public virtual async Task<IDocument> ForwardAsync()
        {
            return await ForwardAsync(false);
        }

        /// <summary>
        /// Navigate to next document in forward history asynchronously
        /// </summary>
        /// <param name="useCache"></param>
        /// <returns></returns>
        public virtual async Task<IDocument> ForwardAsync(bool useCache)
        {
            IDocument forwardDocument = _history.Forward(useCache);
            if (useCache)
                return forwardDocument;

            _browserStandard.BaseUrl = forwardDocument.RequestUri;
            return await _browserStandard.ExecuteTaskAsync(forwardDocument.Request);
        }

        /// <summary>
        /// Max amount of history/Documents to be stored by the browser (-1 for no limit)
        /// </summary>
        public virtual int MaxHistorySize { get { return _history.MaxHistorySize; } set { _history.MaxHistorySize = value; } }

        /// <summary>
        /// Refresh page, re-submits last request
        /// </summary>
        /// <returns></returns>
        public virtual IDocument Refresh()
        {
            IDocument oldDocument = _history.Refresh();
            _browserStandard.BaseUrl = oldDocument.RequestUri;
            return _browserStandard.Execute(oldDocument.Request);
        }

        /// <summary>
        /// Refresh page, re-submits last request asynchronously
        /// </summary>
        /// <returns></returns>
        public virtual async Task<IDocument> RefreshAsync()
        {
            IDocument oldDocument = _history.Refresh();
            _browserStandard.BaseUrl = oldDocument.RequestUri;
            return await _browserStandard.ExecuteTaskAsync(oldDocument.Request);
        }

        /// <summary>
        /// Gets current document from the current request
        /// </summary>
        /// <returns>Current Document</returns>
        public virtual IDocument Document => _history.Document;
        
        /// <summary>
        /// Submits form
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        public virtual IDocument SubmitForm(Form form)
        {
            return _browserStandard.SubmitForm(form);
        }

        /// <inheritdoc />
        public virtual IDocument SubmitForm(Form form, Dictionary<string, string> headers)
        {
            return _browserStandard.SubmitForm(form,headers);
        }
        
        /// <inheritdoc />
        public virtual async Task<IDocument> SubmitFormAsync(Form form)
        {
            return await _browserStandard.SubmitFormAsync(form);
        }

        /// <inheritdoc />
        public virtual async Task<IDocument> SubmitFormAsync(Form form, Dictionary<string, string> headers)
        {
            return await _browserStandard.SubmitFormAsync(form, headers);
        }
        
        /// <inheritdoc />
        public virtual IDocument<T> Execute<T>(IRestRequest request)
        {
            return _browserTyped.Execute<T>(request);
        }
        
        /// <inheritdoc />
        public virtual IDocument<T> ExecuteAsGet<T>(IRestRequest request, string httpMethod)
        {
            return _browserTyped.ExecuteAsGet<T>(request, httpMethod);
        }

        /// <inheritdoc />
        public virtual IDocument<T> ExecuteAsPost<T>(IRestRequest request, string httpMethod)
        {
            return _browserTyped.ExecuteAsPost<T>(request, httpMethod);
        }

        /// <inheritdoc />
        public virtual async Task<IDocument<T>> ExecuteTaskAsync<T>(IRestRequest request, CancellationToken token)
        {
            return await _browserTyped.ExecuteTaskAsync<T>(request, token);
        }

        /// <inheritdoc />
        public virtual async Task<IDocument<T>> ExecuteTaskAsync<T>(IRestRequest request)
        {
            return await _browserTyped.ExecuteTaskAsync<T>(request);
        }

        /// <inheritdoc />
        public virtual async Task<IDocument<T>> ExecuteGetTaskAsync<T>(IRestRequest request)
        {
            return await _browserTyped.ExecuteGetTaskAsync<T>(request);
        }

        /// <inheritdoc />
        public virtual async Task<IDocument<T>> ExecuteGetTaskAsync<T>(IRestRequest request, CancellationToken token)
        {
            return await _browserTyped.ExecuteGetTaskAsync<T>(request, token);
        }

        /// <inheritdoc />
        public virtual async Task<IDocument<T>> ExecutePostTaskAsync<T>(IRestRequest request)
        {
            return await _browserTyped.ExecutePostTaskAsync<T>(request);
        }

        /// <inheritdoc />
        public virtual async Task<IDocument<T>> ExecutePostTaskAsync<T>(IRestRequest request, CancellationToken token)
        {
            return await _browserTyped.ExecutePostTaskAsync<T>(request, token);
        }

        /// <inheritdoc />
        public virtual IDocument<T> Navigate<T>(string uri)
        {
            return _browserTyped.Navigate<T>(uri);
        }

        /// <inheritdoc />
        public virtual IDocument<T> Navigate<T>(Uri uri)
        {
            return _browserTyped.Navigate<T>(uri);
        }

        /// <inheritdoc />
        public virtual IDocument<T> Navigate<T>(string uri, Dictionary<string, string> headers)
        {
            return _browserTyped.Navigate<T>(uri, headers);
        }

        /// <inheritdoc />
        public virtual IDocument<T> Navigate<T>(Uri uri, Dictionary<string, string> headers)
        {
            return _browserTyped.Navigate<T>(uri, headers);
        }

        /// <inheritdoc />
        public virtual IDocument<T> Navigate<T>(string uri, Dictionary<string, string> headers, Dictionary<string, string> formData)
        {
            return _browserTyped.Navigate<T>(uri, headers, formData);
        }

        /// <inheritdoc />
        public virtual IDocument<T> Navigate<T>(Uri uri, Dictionary<string, string> headers, Dictionary<string, string> formData)
        {
            return _browserTyped.Navigate<T>(uri, headers, formData);
        }

        /// <inheritdoc />
        public virtual async Task<IDocument<T>> NavigateAsync<T>(string uri)
        {
            return await _browserTyped.NavigateAsync<T>(uri);
        }

        /// <inheritdoc />
        public virtual async Task<IDocument<T>> NavigateAsync<T>(Uri uri)
        {
            return await _browserTyped.NavigateAsync<T>(uri);
        }

        /// <inheritdoc />
        public virtual async Task<IDocument<T>> NavigateAsync<T>(string uri, Dictionary<string, string> headers)
        {
            return await _browserTyped.NavigateAsync<T>(uri, headers);
        }

        /// <inheritdoc />
        public virtual async Task<IDocument<T>> NavigateAsync<T>(Uri uri, Dictionary<string, string> headers)
        {
            return await _browserTyped.NavigateAsync<T>(uri, headers);
        }
        
        /// <inheritdoc />
        public virtual async Task<IDocument<T>> NavigateAsync<T>(string uri, Dictionary<string, string> headers, Dictionary<string, string> formData)
        {
            return await _browserTyped.NavigateAsync<T>(uri, headers, formData);
        }

        /// <inheritdoc />
        public virtual async Task<IDocument<T>> NavigateAsync<T>(Uri uri, Dictionary<string, string> headers, Dictionary<string, string> formData)
        {
            return await _browserTyped.NavigateAsync<T>(uri, headers, formData);
        }

        /// <inheritdoc />
        public virtual IDocument<T> Submit<T>(string uri)
        {
            return _browserTyped.Submit<T>(uri);
        }

        /// <inheritdoc />
        public virtual IDocument<T> Submit<T>(Uri uri)
        {
            return _browserTyped.Submit<T>(uri);
        }

        /// <inheritdoc />
        public virtual IDocument<T> Submit<T>(string uri, Dictionary<string, string> formData)
        {
            return _browserTyped.Submit<T>(uri, formData);
        }

        /// <inheritdoc />
        public virtual IDocument<T> Submit<T>(Uri uri, Dictionary<string, string> formData)
        {
            return _browserTyped.Submit<T>(uri, formData);
        }

        /// <inheritdoc />
        public virtual IDocument<T> Submit<T>(string uri, Dictionary<string, string> formData, Dictionary<string, string> headers)
        {
            return _browserTyped.Submit<T>(uri, formData, headers);
        }

        /// <inheritdoc />
        public virtual IDocument<T> Submit<T>(Uri uri, Dictionary<string, string> formData, Dictionary<string, string> headers)
        {
            return _browserTyped.Submit<T>(uri, formData, headers);
        }

        /// <inheritdoc />
        public virtual IDocument<T> SubmitForm<T>(Form form)
        {
            return _browserTyped.SubmitForm<T>(form);
        }

        /// <inheritdoc />
        public virtual IDocument<T> SubmitForm<T>(Form form, Dictionary<string, string> headers)
        {
            return _browserTyped.SubmitForm<T>(form, headers);
        }

        /// <inheritdoc />
        public virtual async Task<IDocument<T>> SubmitAsync<T>(string uri)
        {
            return await _browserTyped.SubmitAsync<T>(uri);
        }

        /// <inheritdoc />
        public virtual async Task<IDocument<T>> SubmitAsync<T>(Uri uri)
        {
            return await _browserTyped.SubmitAsync<T>(uri);
        }

        /// <inheritdoc />
        public virtual async Task<IDocument<T>> SubmitAsync<T>(string uri, Dictionary<string, string> formData)
        {
            return await _browserTyped.SubmitAsync<T>(uri, formData);
        }

        /// <inheritdoc />
        public virtual async Task<IDocument<T>> SubmitAsync<T>(Uri uri, Dictionary<string, string> formData)
        {
            return await _browserTyped.SubmitAsync<T>(uri, formData);
        }

        /// <inheritdoc />
        public virtual async Task<IDocument<T>> SubmitAsync<T>(string uri, Dictionary<string, string> formData, Dictionary<string, string> headers)
        {
            return await _browserTyped.SubmitAsync<T>(uri, formData, headers);
        }

        /// <inheritdoc />
        public virtual async Task<IDocument<T>> SubmitAsync<T>(Uri uri, Dictionary<string, string> formData, Dictionary<string, string> headers)
        {
            return await _browserTyped.SubmitAsync<T>(uri, formData, headers);
        }

        /// <inheritdoc />
        public virtual async Task<IDocument<T>> SubmitFormAsync<T>(Form form)
        {
            return await _browserTyped.SubmitFormAsync<T>(form);
        }

        /// <inheritdoc />
        public virtual async Task<IDocument<T>> SubmitFormAsync<T>(Form form, Dictionary<string, string> headers)
        {
            return await _browserTyped.SubmitFormAsync<T>(form, headers);
        }

        /// <inheritdoc/>
        public virtual IDocument<T> DocumentTyped<T>()
        {
            return _browserTyped.DocumentTyped<T>();
        }
    }
}