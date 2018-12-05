using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using BrowseSharp;
using BrowseSharp.Browsers.Core;
using BrowseSharp.History;
using BrowseSharp.Javascript;
using BrowseSharp.Style;
using RestSharp;

namespace BrowseSharpTest
{
    public class BrowserThreadFail 
    {
        public BrowserThreadFail()
        {
            _restClient = new RestClient();
            _history = new HistoryManager();
            _browser= new StandardCore(new JavascriptEngine(), new StyleEngine(),_restClient,new CookieContainer(), _history,false,false,"http" );
            _sleepAmount = 5000;
            _asyncSemaphore = new SemaphoreSlim(1, 1);
        }

        private StandardCore _browser;
        private int _sleepAmount;
        private RestClient _restClient;
        private HistoryManager _history;
        private SemaphoreSlim _asyncSemaphore;
        public async Task<IDocument> NavigateAsync(string uri)
        {
            _history.Navigate();
            _restClient.BaseUrl = new Uri(uri);
            _sleepAmount -= 1000;
            await Task.Delay(_sleepAmount + 1000);
            RestRequest request = new RestRequest();
            return await _browser.ExecuteTaskAsync(request);
        }
        
        public async Task<IDocument> NavigateAsyncSafe(string uri)
        {
            await _asyncSemaphore.WaitAsync();
            try
            {
                _history.Navigate();
                _restClient.BaseUrl = new Uri(uri);
                _sleepAmount -= 1000;
                await Task.Delay(_sleepAmount + 1000);
                RestRequest request = new RestRequest();
                return await _browser.ExecuteTaskAsync(request);
            }
            finally
            {
                _asyncSemaphore.Release();
            }
        }
    }
}