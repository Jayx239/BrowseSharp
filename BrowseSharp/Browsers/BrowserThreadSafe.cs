using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BrowseSharp.Common;
using BrowseSharp.Common.Html;

namespace BrowseSharp.Browsers
{
    public class BrowserThreadSafe : Browser
    {
        public BrowserThreadSafe() : base()
        {
            _asyncSemaphore = new SemaphoreSlim(1, 1);
        }

        private SemaphoreSlim _asyncSemaphore;

        /// <inheritdoc/>
        public override IDocument Navigate(Uri uri)
        {
            _asyncSemaphore.Wait();
            try
            {
                return base.Navigate(uri);
            }
            finally
            {
                _asyncSemaphore.Release();
            }
        }

        /// <inheritdoc />
        public override IDocument Navigate(string uri, Dictionary<string, string> headers)
        {
            _asyncSemaphore.Wait();
            try
            {
                return base.Navigate(uri, headers);
            }
            finally
            {
                _asyncSemaphore.Release();
            }
        }

        /// <inheritdoc />
        public override IDocument Navigate(Uri uri, Dictionary<string, string> headers)
        {
            _asyncSemaphore.Wait();
            try
            {
                return base.Navigate(uri, headers);
            }
            finally
            {
                _asyncSemaphore.Release();
            }
        }

        /// <inheritdoc />
        public override IDocument Navigate(string uri, Dictionary<string, string> headers,
            Dictionary<string, string> formData)
        {
            _asyncSemaphore.Wait();
            try
            {
                return base.Navigate(uri, headers, formData);
            }
            finally
            {
                _asyncSemaphore.Release();
            }
        }

        /// <inheritdoc />
        public override IDocument Navigate(Uri uri, Dictionary<string, string> headers,
            Dictionary<string, string> formData)
        {
            _asyncSemaphore.Wait();
            try
            {
                return base.Navigate(uri, headers, formData);
            }
            finally
            {
                _asyncSemaphore.Release();
            }
        }

        /// <inheritdoc />
        public override async Task<IDocument> NavigateAsync(string uri)
        {
            await _asyncSemaphore.WaitAsync();
            try
            {
                return await base.NavigateAsync(uri);
            }
            finally
            {
                _asyncSemaphore.Release();
            }
        }

        /// <inheritdoc />
        public override async Task<IDocument> NavigateAsync(Uri uri)
        {
            await _asyncSemaphore.WaitAsync();
            try
            {
                return await base.NavigateAsync(uri);
            }
            finally
            {
                _asyncSemaphore.Release();
            }
        }

        /// <inheritdoc />
        public override async Task<IDocument> NavigateAsync(string uri, Dictionary<string, string> headers)
        {
            await _asyncSemaphore.WaitAsync();
            try
            {
                return await base.NavigateAsync(uri, headers);
            }
            finally
            {
                _asyncSemaphore.Release();
            }
        }

        /// <inheritdoc />
        public override async Task<IDocument> NavigateAsync(Uri uri, Dictionary<string, string> headers)
        {
            await _asyncSemaphore.WaitAsync();
            try
            {
                return await base.NavigateAsync(uri, headers);
            }
            finally
            {
                _asyncSemaphore.Release();
            }
        }

        /// <inheritdoc />
        public override async Task<IDocument> NavigateAsync(string uri, Dictionary<string, string> headers,
            Dictionary<string, string> formData)
        {
            await _asyncSemaphore.WaitAsync();
            try
            {
                return await base.NavigateAsync(uri, headers, formData);
            }
            finally
            {
                _asyncSemaphore.Release();
            }
        }

        /// <inheritdoc />
        public override async Task<IDocument> NavigateAsync(Uri uri, Dictionary<string, string> headers,
            Dictionary<string, string> formData)
        {
            await _asyncSemaphore.WaitAsync();
            try
            {
                return await base.NavigateAsync(uri, headers, formData);
            }
            finally
            {
                _asyncSemaphore.Release();
            }
        }

        /// <inheritdoc />
        public override IDocument Submit(string uri)
        {
            _asyncSemaphore.Wait();
            try
            {
                return base.Submit(uri);
            }
            finally
            {
                _asyncSemaphore.Release();
            }
        }

        /// <inheritdoc />
        public override IDocument Submit(Uri uri)
        {
            _asyncSemaphore.Wait();
            try
            {
                return base.Submit(uri);
            }
            finally
            {
                _asyncSemaphore.Release();
            }
        }

        /// <inheritdoc />
        public override IDocument Submit(string uri, Dictionary<string, string> formData)
        {
            _asyncSemaphore.Wait();
            try
            {
                return base.Submit(uri, formData);
            }
            finally
            {
                _asyncSemaphore.Release();
            }
        }

        /// <inheritdoc />
        public override IDocument Submit(Uri uri, Dictionary<string, string> formData)
        {
            _asyncSemaphore.Wait();
            try
            {
                return base.Submit(uri, formData);
            }
            finally
            {
                _asyncSemaphore.Release();
            }
        }

        /// <inheritdoc />
        public override IDocument Submit(string uri, Dictionary<string, string> formData,
            Dictionary<string, string> headers)
        {
            _asyncSemaphore.Wait();
            try
            {
                return base.Submit(uri, formData, headers);
            }
            finally
            {
                _asyncSemaphore.Release();
            }
        }

        /// <inheritdoc />
        public override IDocument Submit(Uri uri, Dictionary<string, string> formData,
            Dictionary<string, string> headers)
        {
            _asyncSemaphore.Wait();
            try
            {
                return base.Submit(uri, formData, headers);
            }
            finally
            {
                _asyncSemaphore.Release();
            }
        }

        /// <inheritdoc />
        public override async Task<IDocument> SubmitAsync(string uri)
        {
            await _asyncSemaphore.WaitAsync();
            try
            {
                return await base.SubmitAsync(uri);
            }
            finally
            {
                _asyncSemaphore.Release();
            }
        }

        /// <inheritdoc />
        public override async Task<IDocument> SubmitAsync(Uri uri)
        {
            await _asyncSemaphore.WaitAsync();
            try
            {
                return await base.SubmitAsync(uri);
            }
            finally
            {
                _asyncSemaphore.Release();
            }
        }

        /// <inheritdoc />
        public override async Task<IDocument> SubmitAsync(string uri, Dictionary<string, string> formData)
        {
            await _asyncSemaphore.WaitAsync();
            try
            {
                return await base.SubmitAsync(uri, formData);
            }
            finally
            {
                _asyncSemaphore.Release();
            }
        }

        /// <inheritdoc />
        public override async Task<IDocument> SubmitAsync(Uri uri, Dictionary<string, string> formData)
        {
            await _asyncSemaphore.WaitAsync();
            try
            {
                return await base.SubmitAsync(uri, formData);
            }
            finally
            {
                _asyncSemaphore.Release();
            }
        }

        /// <inheritdoc />
        public override async Task<IDocument> SubmitAsync(string uri, Dictionary<string, string> formData,
            Dictionary<string, string> headers)
        {
            await _asyncSemaphore.WaitAsync();
            try
            {
                return await base.SubmitAsync(uri, formData, headers);
            }
            finally
            {
                _asyncSemaphore.Release();
            }
        }

        /// <inheritdoc />
        public override async Task<IDocument> SubmitAsync(Uri uri, Dictionary<string, string> formData,
            Dictionary<string, string> headers)
        {
            await _asyncSemaphore.WaitAsync();
            try
            {
                return await base.SubmitAsync(uri, formData, headers);
            }
            finally
            {
                _asyncSemaphore.Release();
            }
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
        public override void ClearHistory()
        {
            _asyncSemaphore.Wait();
            try
            {
                _history.ClearHistory();
            }
            finally
            {
                _asyncSemaphore.Release();
            }
        }

        /// <summary>
        /// Clears forward history
        /// </summary>
        public override void ClearForwardHistory()
        {
            _asyncSemaphore.Wait();
            try
            {
                _history.ClearForwardHistory();
            }
            finally
            {
                _asyncSemaphore.Release();
            }
        }

        /// <summary>
        /// Method for navigating to last browser state by re-issuing the previous request
        /// </summary>
        public override IDocument Back()
        {
            return Back(false);
        }

        /// <summary>
        /// Method for navigating to last browser state
        /// </summary>
        /// <param name="useCache">Determines whether to re-issue request or reload last document</param>
        public override IDocument Back(bool useCache)
        {
            _asyncSemaphore.Wait();
            try
            {
                return base.Back(useCache);
            }
            finally
            {
                _asyncSemaphore.Release();
            }
        }


        /// <summary>
        /// Method for navigating to last browser state by re-issuing the previous request asynchronously
        /// </summary>
        public override async Task<IDocument> BackAsync()
        {
            return await BackAsync(false);
        }

        /// <summary>
        /// Method for navigating to last browser state asynchronously
        /// </summary>
        /// <param name="useCache">Determines whether to re-issue request or reload last document</param>
        public override async Task<IDocument> BackAsync(bool useCache)
        {
            await _asyncSemaphore.WaitAsync();
            try
            {
                return await base.BackAsync(useCache);
            }
            finally
            {
                _asyncSemaphore.Release();
            }
        }

        /// <summary>
        /// Navigate to next document in forward history
        /// </summary>
        /// <returns></returns>
        public override IDocument Forward()
        {
            return Forward(false);
        }

        /// <summary>
        /// Navigate to next document in forward history
        /// </summary>
        /// <param name="useCache"></param>
        /// <returns></returns>
        public override IDocument Forward(bool useCache)
        {
            _asyncSemaphore.Wait();
            try
            {
                return base.Forward(useCache);
            }
            finally
            {
                _asyncSemaphore.Release();
            }
        }

        /// <summary>
        /// Navigate to next document in forward history asynchronously
        /// </summary>
        /// <returns></returns>
        public override async Task<IDocument> ForwardAsync()
        {
            return await ForwardAsync(false);
        }

        /// <summary>
        /// Navigate to next document in forward history asynchronously
        /// </summary>
        /// <param name="useCache"></param>
        /// <returns></returns>
        public override async Task<IDocument> ForwardAsync(bool useCache)
        {
            await _asyncSemaphore.WaitAsync();
            try
            {
                return await base.ForwardAsync(useCache);
            }
            finally
            {
                _asyncSemaphore.Release();
            }
        }

        /// <summary>
        /// Max amount of history/Documents to be stored by the browser (-1 for no limit)
        /// </summary>
        public override int MaxHistorySize
        {
            get { return _history.MaxHistorySize; }
            set { _history.MaxHistorySize = value; }
        }

        /// <summary>
        /// Refresh page, re-submits last request
        /// </summary>
        /// <returns></returns>
        public override IDocument Refresh()
        {
            _asyncSemaphore.Wait();
            try
            {
                return base.Refresh();
            }
            finally
            {
                _asyncSemaphore.Release();
            }
        }

        /// <summary>
        /// Refresh page, re-submits last request asynchronously
        /// </summary>
        /// <returns></returns>
        public override async Task<IDocument> RefreshAsync()
        {
            await _asyncSemaphore.WaitAsync();
            try
            {
                return await base.RefreshAsync();
            }
            finally
            {
                _asyncSemaphore.Release();
            }
        }

        /// <summary>
        /// Gets current document from the current request
        /// </summary>
        /// <returns>Current Document</returns>
        public override IDocument Document => _history.Document;

        /// <summary>
        /// Submits form
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        public override IDocument SubmitForm(Form form)
        {
            _asyncSemaphore.Wait();
            try
            {
                return base.SubmitForm(form);
            }
            finally
            {
                _asyncSemaphore.Release();
            }
        }

        /// <inheritdoc />
        public override IDocument SubmitForm(Form form, Dictionary<string, string> headers)
        {
            _asyncSemaphore.Wait();
            try
            {
                return base.SubmitForm(form, headers);
            }
            finally
            {
                _asyncSemaphore.Release();
            }
        }

        /// <inheritdoc />
        public override async Task<IDocument> SubmitFormAsync(Form form)
        {
            await _asyncSemaphore.WaitAsync();
            try
            {
                return await base.SubmitFormAsync(form);
            }
            finally
            {
                _asyncSemaphore.Release();
            }
        }

        /// <inheritdoc />
        public override async Task<IDocument> SubmitFormAsync(Form form, Dictionary<string, string> headers)
        {
            await _asyncSemaphore.WaitAsync();
            try
            {
                return await base.SubmitFormAsync(form, headers);
            }
            finally
            {
                _asyncSemaphore.Release();
            }
        }

        /// <inheritdoc />
        public override IDocument<T> Navigate<T>(string uri)
        {
            _asyncSemaphore.Wait();
            try
            {
                return base.Navigate<T>(uri);
            }
            finally
            {
                _asyncSemaphore.Release();
            }
        }

        /// <inheritdoc />
        public override IDocument<T> Navigate<T>(Uri uri)
        {
            _asyncSemaphore.Wait();
            try
            {
                return base.Navigate<T>(uri);
            }
            finally
            {
                _asyncSemaphore.Release();
            }
        }

        /// <inheritdoc />
        public override IDocument<T> Navigate<T>(string uri, Dictionary<string, string> headers)
        {
            _asyncSemaphore.Wait();
            try
            {
                return base.Navigate<T>(uri, headers);
            }
            finally
            {
                _asyncSemaphore.Release();
            }
        }

        /// <inheritdoc />
        public override IDocument<T> Navigate<T>(Uri uri, Dictionary<string, string> headers)
        {
            _asyncSemaphore.Wait();
            try
            {
                return base.Navigate<T>(uri, headers);
            }
            finally
            {
                _asyncSemaphore.Release();
            }
        }

        /// <inheritdoc />
        public override IDocument<T> Navigate<T>(string uri, Dictionary<string, string> headers,
            Dictionary<string, string> formData)
        {
            _asyncSemaphore.Wait();
            try
            {
                return base.Navigate<T>(uri, headers, formData);
            }
            finally
            {
                _asyncSemaphore.Release();
            }
        }

        /// <inheritdoc />
        public override IDocument<T> Navigate<T>(Uri uri, Dictionary<string, string> headers,
            Dictionary<string, string> formData)
        {
            _asyncSemaphore.Wait();
            try
            {
                return base.Navigate<T>(uri, headers, formData);
            }
            finally
            {
                _asyncSemaphore.Release();
            }
        }

        /// <inheritdoc />
        public override async Task<IDocument<T>> NavigateAsync<T>(string uri)
        {
            await _asyncSemaphore.WaitAsync();
            try
            {
                return await base.NavigateAsync<T>(uri);
            }
            finally
            {
                _asyncSemaphore.Release();
            }
        }

        /// <inheritdoc />
        public override async Task<IDocument<T>> NavigateAsync<T>(Uri uri)
        {
            await _asyncSemaphore.WaitAsync();
            try
            {
                return await base.NavigateAsync<T>(uri);
            }
            finally
            {
                _asyncSemaphore.Release();
            }
        }

        /// <inheritdoc />
        public override async Task<IDocument<T>> NavigateAsync<T>(string uri, Dictionary<string, string> headers)
        {
            await _asyncSemaphore.WaitAsync();
            try
            {
                return await base.NavigateAsync<T>(uri, headers);
            }
            finally
            {
                _asyncSemaphore.Release();
            }
        }

        /// <inheritdoc />
        public override async Task<IDocument<T>> NavigateAsync<T>(Uri uri, Dictionary<string, string> headers)
        {
            await _asyncSemaphore.WaitAsync();
            try
            {
                return await base.NavigateAsync<T>(uri, headers);
            }
            finally
            {
                _asyncSemaphore.Release();
            }
        }

        /// <inheritdoc />
        public override async Task<IDocument<T>> NavigateAsync<T>(string uri, Dictionary<string, string> headers,
            Dictionary<string, string> formData)
        {
            await _asyncSemaphore.WaitAsync();
            try
            {
                return await base.NavigateAsync<T>(uri, headers, formData);
            }
            finally
            {
                _asyncSemaphore.Release();
            }
        }

        /// <inheritdoc />
        public override async Task<IDocument<T>> NavigateAsync<T>(Uri uri, Dictionary<string, string> headers,
            Dictionary<string, string> formData)
        {
            await _asyncSemaphore.WaitAsync();
            try
            {
                return await base.NavigateAsync<T>(uri, headers, formData);
            }
            finally
            {
                _asyncSemaphore.Release();
            }
        }

        /// <inheritdoc />
        public override IDocument<T> Submit<T>(string uri)
        {
            _asyncSemaphore.Wait();
            try
            {
                return base.Submit<T>(uri);
            }
            finally
            {
                _asyncSemaphore.Release();
            }
        }

        /// <inheritdoc />
        public override IDocument<T> Submit<T>(Uri uri)
        {
            _asyncSemaphore.Wait();
            try
            {
                return base.Submit<T>(uri);
            }
            finally
            {
                _asyncSemaphore.Release();
            }
        }

        /// <inheritdoc />
        public override IDocument<T> Submit<T>(string uri, Dictionary<string, string> formData)
        {
            _asyncSemaphore.Wait();
            try
            {
                return base.Submit<T>(uri, formData);
            }
            finally
            {
                _asyncSemaphore.Release();
            }
        }

        /// <inheritdoc />
        public override IDocument<T> Submit<T>(Uri uri, Dictionary<string, string> formData)
        {
            _asyncSemaphore.Wait();
            try
            {
                return base.Submit<T>(uri, formData);
            }
            finally
            {
                _asyncSemaphore.Release();
            }
        }

        /// <inheritdoc />
        public override IDocument<T> Submit<T>(string uri, Dictionary<string, string> formData,
            Dictionary<string, string> headers)
        {
            _asyncSemaphore.Wait();
            try
            {
                return base.Submit<T>(uri, formData, headers);
            }
            finally
            {
                _asyncSemaphore.Release();
            }
        }

        /// <inheritdoc />
        public override IDocument<T> Submit<T>(Uri uri, Dictionary<string, string> formData,
            Dictionary<string, string> headers)
        {
            _asyncSemaphore.Wait();
            try
            {
                return base.Submit<T>(uri, formData, headers);
            }
            finally
            {
                _asyncSemaphore.Release();
            }
        }

        /// <inheritdoc />
        public override IDocument<T> SubmitForm<T>(Form form)
        {
            _asyncSemaphore.Wait();
            try
            {
                return base.SubmitForm<T>(form);
            }
            finally
            {
                _asyncSemaphore.Release();
            }
        }

        /// <inheritdoc />
        public override IDocument<T> SubmitForm<T>(Form form, Dictionary<string, string> headers)
        {
            _asyncSemaphore.Wait();
            try
            {
                return base.SubmitForm<T>(form, headers);
            }
            finally
            {
                _asyncSemaphore.Release();
            }
        }

        /// <inheritdoc />
        public override async Task<IDocument<T>> SubmitAsync<T>(string uri)
        {
            await _asyncSemaphore.WaitAsync();
            try
            {
                return await base.SubmitAsync<T>(uri);
            }
            finally
            {
                _asyncSemaphore.Release();
            }
        }

        /// <inheritdoc />
        public override async Task<IDocument<T>> SubmitAsync<T>(Uri uri)
        {
            await _asyncSemaphore.WaitAsync();
            try
            {
                return await base.SubmitAsync<T>(uri);
            }
            finally
            {
                _asyncSemaphore.Release();
            }
        }

        /// <inheritdoc />
        public override async Task<IDocument<T>> SubmitAsync<T>(string uri, Dictionary<string, string> formData)
        {
            await _asyncSemaphore.WaitAsync();
            try
            {
                return await base.SubmitAsync<T>(uri, formData);
            }
            finally
            {
                _asyncSemaphore.Release();
            }
        }

        /// <inheritdoc />
        public override async Task<IDocument<T>> SubmitAsync<T>(Uri uri, Dictionary<string, string> formData)
        {
            await _asyncSemaphore.WaitAsync();
            try
            {
                return await base.SubmitAsync<T>(uri, formData);
            }
            finally
            {
                _asyncSemaphore.Release();
            }
        }

        /// <inheritdoc />
        public override async Task<IDocument<T>> SubmitAsync<T>(string uri, Dictionary<string, string> formData,
            Dictionary<string, string> headers)
        {
            await _asyncSemaphore.WaitAsync();
            try
            {
                return await base.SubmitAsync<T>(uri, formData, headers);
            }
            finally
            {
                _asyncSemaphore.Release();
            }
        }

        /// <inheritdoc />
        public override async Task<IDocument<T>> SubmitAsync<T>(Uri uri, Dictionary<string, string> formData,
            Dictionary<string, string> headers)
        {
            await _asyncSemaphore.WaitAsync();
            try
            {
                return await base.SubmitAsync<T>(uri, formData, headers);
            }
            finally
            {
                _asyncSemaphore.Release();
            }
        }

        /// <inheritdoc />
        public override async Task<IDocument<T>> SubmitFormAsync<T>(Form form)
        {
            await _asyncSemaphore.WaitAsync();
            try
            {
                return await base.SubmitFormAsync<T>(form);
            }
            finally
            {
                _asyncSemaphore.Release();
            }
        }

        /// <inheritdoc />
        public override async Task<IDocument<T>> SubmitFormAsync<T>(Form form, Dictionary<string, string> headers)
        {
            await _asyncSemaphore.WaitAsync();
            try
            {
                return await base.SubmitFormAsync<T>(form, headers);
            }
            finally
            {
                _asyncSemaphore.Release();
            }
        }

        /// <inheritdoc/>
        public override IDocument<T> DocumentTyped<T>()
        {
            _asyncSemaphore.Wait();
            try
            {
                return base.DocumentTyped<T>();
            }
            finally
            {
                _asyncSemaphore.Release();
            }
        }
    }
}