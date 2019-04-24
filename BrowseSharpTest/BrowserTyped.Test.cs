using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BrowseSharp;
using BrowseSharp.Browsers;
using BrowseSharp.Common;
using BrowseSharp.Common.Html;
using BrowseSharpTest.Models;
using NUnit.Framework;
using RestSharp;

/* Several tests depend on the RequestTester nodejs project
 You can find this project at https://github.com/Jayx239/RequestTester
 Be sure to set the proper port number that your RequestTester is listening on
 */

namespace BrowseSharpTest
{
    [TestFixture]
    public class BrowserTypedTest
    {
        /* RequestTester Configuration */
        public static int RequestTesterPort = 3000; // This is the port your RequestTester application is listening to
        public static string RequestTesterRouteUri = "https://requesttester.com/tester/view";//"http://localhost:" + RequestTesterPort + "/tester/view";
        public static string RequestTesterRouteJsonUri = "https://requesttester.com/tester";

        

        #region Typed Tests
        [Test]
        public void TestExecute()
        {
            BrowserTyped browser = new BrowserTyped();
            browser.BaseUrl = new Uri(RequestTesterRouteJsonUri);
            RestRequest request = new RestRequest();
            IDocument<dynamic> document = browser.Execute<dynamic>(request);

            dynamic data = document.Data;

            browser.BaseUrl = new Uri("https://dog.ceo/api/breeds/image/random");
            RestRequest dogRequest = new RestRequest();
            IDocument<DogResponse> dogDocument = browser.Execute<DogResponse>(dogRequest);
            Assert.IsTrue(dogDocument.Data.Status == "success");
            Assert.IsTrue(dogDocument.Data.Message.StartsWith("http"));
            Assert.IsTrue(dogDocument.Data.Message != null);
            IDocument<RandomObject> invalidDataResponseDocument = browser.Execute<RandomObject>(dogRequest);
        }

        [Test]
        public void TestExecuteAsGet()
        {

            BrowserTyped browser = new BrowserTyped();
            browser.BaseUrl = new Uri(RequestTesterRouteJsonUri);
            RestRequest request = new RestRequest();
            IDocument<dynamic> document = browser.ExecuteAsGet<dynamic>(request, "GET");

            dynamic data = document.Data;

            browser.BaseUrl = new Uri("https://dog.ceo/api/breeds/image/random");
            RestRequest dogRequest = new RestRequest();
            IDocument<DogResponse> dogDocument = browser.ExecuteAsGet<DogResponse>(dogRequest, "GET");
            Assert.IsTrue(dogDocument.Data.Status == "success");
            Assert.IsTrue(dogDocument.Data.Message.StartsWith("http"));
            Assert.IsTrue(dogDocument.Data.Message != null);
            IDocument<RandomObject> invalidDataResponseDocument = browser.ExecuteAsGet<RandomObject>(dogRequest, "GET");
            
        }

        [Test]
        public void TestExecuteAsPost()
        {
            BrowserTyped browser = new BrowserTyped();
            browser.BaseUrl = new Uri(RequestTesterRouteJsonUri);
            IRestRequest request = new RestRequest();

            request.AddParameter("Username", "FakeUserName");
            request.AddParameter("Password", "FakePassword123");
            request.AddParameter("SecretMessage", "This is a secret message");
            var response = browser.ExecuteAsPost<Request>(request, "Post");
            Assert.IsTrue(response.Data.FormData.ContainsKey("Username"));
            Assert.IsTrue(response.Data.FormData.ContainsKey("Password"));
            Assert.IsTrue(response.Data.FormData.ContainsKey("SecretMessage"));
            Assert.IsTrue(response.Data.FormData["Username"] == "FakeUserName");
            Assert.IsTrue(response.Data.FormData["Password"] == "FakePassword123");
            Assert.IsTrue(response.Data.FormData["SecretMessage"] == "This is a secret message");

        }

        [Test]
        public async Task TestExecuteGetTaskAsync()
        {

            BrowserTyped browser = new BrowserTyped();
            browser.BaseUrl = new Uri(RequestTesterRouteJsonUri);
            RestRequest request = new RestRequest();
            IDocument<dynamic> document = await browser.ExecuteGetTaskAsync<dynamic>(request);

            dynamic data = document.Data;

            browser.BaseUrl = new Uri("https://dog.ceo/api/breeds/image/random");
            RestRequest dogRequest = new RestRequest();
            IDocument<DogResponse> dogDocument = await browser.ExecuteGetTaskAsync<DogResponse>(dogRequest);
            Assert.IsTrue(dogDocument.Data.Status == "success");
            Assert.IsTrue(dogDocument.Data.Message.StartsWith("http"));
            Assert.IsTrue(dogDocument.Data.Message != null);
            IDocument<RandomObject> invalidDataResponseDocument = browser.ExecuteAsGet<RandomObject>(dogRequest, "GET");
        }
        
        [Test]
        public async Task TestExecuteGetTaskAsyncToken()
        {

            BrowserTyped browser = new BrowserTyped();
            browser.BaseUrl = new Uri(RequestTesterRouteJsonUri);
            RestRequest request = new RestRequest();
            CancellationToken cancellationToken = new CancellationToken();
            IDocument<dynamic> document = await browser.ExecuteGetTaskAsync<dynamic>(request, cancellationToken);

            dynamic data = document.Data;

            browser.BaseUrl = new Uri("https://dog.ceo/api/breeds/image/random");
            RestRequest dogRequest = new RestRequest();
            IDocument<DogResponse> dogDocument = await browser.ExecuteGetTaskAsync<DogResponse>(dogRequest);
            Assert.IsTrue(dogDocument.Data.Status == "success");
            Assert.IsTrue(dogDocument.Data.Message.StartsWith("http"));
            Assert.IsTrue(dogDocument.Data.Message != null);
            IDocument<RandomObject> invalidDataResponseDocument = browser.ExecuteAsGet<RandomObject>(dogRequest, "GET");
        }
        
        [Test]
        public async Task TestExecutePostTaskAsync()
        {

            BrowserTyped browser = new BrowserTyped();
            browser.BaseUrl = new Uri(RequestTesterRouteJsonUri);
            IRestRequest request = new RestRequest();

            request.AddParameter("Username", "FakeUserName");
            request.AddParameter("Password", "FakePassword123");
            request.AddParameter("SecretMessage", "This is a secret message");
            var response = await browser.ExecutePostTaskAsync<Request>(request);
            Assert.IsTrue(response.Data.FormData.ContainsKey("Username"));
            Assert.IsTrue(response.Data.FormData.ContainsKey("Password"));
            Assert.IsTrue(response.Data.FormData.ContainsKey("SecretMessage"));
            Assert.IsTrue(response.Data.FormData["Username"] == "FakeUserName");
            Assert.IsTrue(response.Data.FormData["Password"] == "FakePassword123");
            Assert.IsTrue(response.Data.FormData["SecretMessage"] == "This is a secret message");
        }
        
        [Test]
        public async Task TestExecutePostTaskAsyncToken()
        {

            BrowserTyped browser = new BrowserTyped();
            browser.BaseUrl = new Uri(RequestTesterRouteJsonUri);
            IRestRequest request = new RestRequest();

            request.AddParameter("Username", "FakeUserName");
            request.AddParameter("Password", "FakePassword123");
            request.AddParameter("SecretMessage", "This is a secret message");
            CancellationToken cancellationToken = new CancellationToken();
            var response = await browser.ExecutePostTaskAsync<Request>(request, cancellationToken);
            Assert.IsTrue(response.Data.FormData.ContainsKey("Username"));
            Assert.IsTrue(response.Data.FormData.ContainsKey("Password"));
            Assert.IsTrue(response.Data.FormData.ContainsKey("SecretMessage"));
            Assert.IsTrue(response.Data.FormData["Username"] == "FakeUserName");
            Assert.IsTrue(response.Data.FormData["Password"] == "FakePassword123");
            Assert.IsTrue(response.Data.FormData["SecretMessage"] == "This is a secret message");
            
        }
        [Test]
        public async Task TestExecuteTaskAsync()
        {

            BrowserTyped browser = new BrowserTyped();
            browser.BaseUrl = new Uri(RequestTesterRouteJsonUri);
            IRestRequest request = new RestRequest();

            request.AddParameter("Username", "FakeUserName");
            request.AddParameter("Password", "FakePassword123");
            request.AddParameter("SecretMessage", "This is a secret message");
            var response = await browser.ExecuteTaskAsync<Request>(request);
            Assert.IsTrue(response.Data.Query.ContainsKey("Username"));
            Assert.IsTrue(response.Data.Query.ContainsKey("Password"));
            Assert.IsTrue(response.Data.Query.ContainsKey("SecretMessage"));
            Assert.IsTrue(response.Data.Query["Username"] == "FakeUserName");
            Assert.IsTrue(response.Data.Query["Password"] == "FakePassword123");
            Assert.IsTrue(response.Data.Query["SecretMessage"] == "This is a secret message");
        }
        
        [Test]
        public async Task TestExecuteTaskAsyncToken()
        {

            BrowserTyped browser = new BrowserTyped();
            browser.BaseUrl = new Uri(RequestTesterRouteJsonUri);
            IRestRequest request = new RestRequest();

            request.AddParameter("Username", "FakeUserName");
            request.AddParameter("Password", "FakePassword123");
            request.AddParameter("SecretMessage", "This is a secret message");
            CancellationToken cancellationToken = new CancellationToken();
            var response = await browser.ExecuteTaskAsync<Request>(request, cancellationToken);
            Assert.IsTrue(response.Data.Query.ContainsKey("Username"));
            Assert.IsTrue(response.Data.Query.ContainsKey("Password"));
            Assert.IsTrue(response.Data.Query.ContainsKey("SecretMessage"));
            Assert.IsTrue(response.Data.Query["Username"] == "FakeUserName");
            Assert.IsTrue(response.Data.Query["Password"] == "FakePassword123");
            Assert.IsTrue(response.Data.Query["SecretMessage"] == "This is a secret message");
            
        }
        
        [Test]
        public void TestNavigate()
        {
            BrowserTyped browser = new BrowserTyped();
            var response = browser.Navigate<Request>(RequestTesterRouteJsonUri);
            Assert.IsTrue(response.Data != null);
            Assert.IsTrue(response.Data is Request);
        }
        
        [Test]
        public void TestNavigateHeaders()
        {
            BrowserTyped browser = new BrowserTyped();

            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("x-csrf-token", "axsd82os21");
            
            IDocument<Request> response = browser.Navigate<Request>(RequestTesterRouteJsonUri, headers);

            Request request = response.Data;
            Assert.IsTrue(request.Headers.ContainsKey("x-csrf-token"));
            Assert.IsTrue(request.Headers["x-csrf-token"] == "axsd82os21");
        }

        [Test]
        public void TestNavigateHeadersAndData()
        {
            BrowserTyped browser = new BrowserTyped();
            
            Dictionary<string, string> formData = new Dictionary<string, string>();
            formData.Add("Username","FakeUserName");
            formData.Add("Password", "FakePassword123");
            formData.Add("SecretMessage", "This is a secret message");
            
            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("x-csrf-token", "axsd82os21");
            
            IDocument<Request> response = browser.Navigate<Request>(RequestTesterRouteJsonUri, headers, formData);
            Request request = response.Data;
            
            Assert.IsTrue(request.Headers["x-csrf-token"] == "axsd82os21");
            Assert.IsTrue(request.Query["Username"] == "FakeUserName");
            Assert.IsTrue(request.Query["Password"] == "FakePassword123");
            Assert.IsTrue(request.Query["SecretMessage"] == "This is a secret message");
        }
        
        [Test]
        public async Task TestNavigateAsync()
        {
            BrowserTyped browser = new BrowserTyped();
            var response = await browser.NavigateAsync<Request>(RequestTesterRouteJsonUri);
            Assert.IsTrue(response.Data != null);
            Assert.IsTrue(response.Data is Request);
        }
        
        [Test]
        public async Task TestNavigateHeadersAsync()
        {
            BrowserTyped browser = new BrowserTyped();

            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("x-csrf-token", "axsd82os21");
            
            IDocument<Request> response = await browser.NavigateAsync<Request>(RequestTesterRouteJsonUri, headers);

            Request request = response.Data;
            Assert.IsTrue(request.Headers.ContainsKey("x-csrf-token"));
            Assert.IsTrue(request.Headers["x-csrf-token"] == "axsd82os21");
        }

        [Test]
        public async Task TestNavigateHeadersAndDataAsync()
        {
            BrowserTyped browser = new BrowserTyped();
            
            Dictionary<string, string> formData = new Dictionary<string, string>();
            formData.Add("Username","FakeUserName");
            formData.Add("Password", "FakePassword123");
            formData.Add("SecretMessage", "This is a secret message");
            
            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("x-csrf-token", "axsd82os21");
            
            IDocument<Request> response = await browser.NavigateAsync<Request>(RequestTesterRouteJsonUri, headers, formData);
            Request request = response.Data;
            
            Assert.IsTrue(request.Headers["x-csrf-token"] == "axsd82os21");
            Assert.IsTrue(request.Query["Username"] == "FakeUserName");
            Assert.IsTrue(request.Query["Password"] == "FakePassword123");
            Assert.IsTrue(request.Query["SecretMessage"] == "This is a secret message");
        }

        
        [Test]
        public void TestSubmitNothing()
        {
            BrowserTyped browser = new BrowserTyped();
            
            IDocument<Request> response = browser.Submit<Request>(RequestTesterRouteJsonUri);
            Request request = response.Data;
            
            Assert.IsTrue(request.Headers.Count > 0);
            
        }
        
        [Test]
        public void TestSubmit()
        {
            BrowserTyped browser = new BrowserTyped();

            Dictionary<string, string> formData = new Dictionary<string, string>();
            formData.Add("Username","FakeUserName");
            formData.Add("Password", "FakePassword123");
            formData.Add("SecretMessage", "This is a secret message");
            
            IDocument<Request> response = browser.Submit<Request>(RequestTesterRouteJsonUri, formData);
            Request request = response.Data;
            
            Assert.IsTrue(request.FormData["Username"] == "FakeUserName");
            Assert.IsTrue(request.FormData["Password"] == "FakePassword123");
            Assert.IsTrue(request.FormData["SecretMessage"] == "This is a secret message");
        }
        
        
        [Test]
        public void TestSubmitHeaders()
        {
            BrowserTyped browser = new BrowserTyped();

            Dictionary<string, string> formData = new Dictionary<string, string>();
            formData.Add("Username","FakeUserName");
            formData.Add("Password", "FakePassword123");
            formData.Add("SecretMessage", "This is a secret message");
            
            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("x-csrf-token", "axsd82os21");
            
            IDocument<Request> response = browser.Submit<Request>(RequestTesterRouteJsonUri, formData, headers);
            Request request = response.Data;
            Assert.IsTrue(request.Headers["x-csrf-token"] == "axsd82os21");
            Assert.IsTrue(request.FormData["Username"] == "FakeUserName");
            Assert.IsTrue(request.FormData["Password"] == "FakePassword123");
            Assert.IsTrue(request.FormData["SecretMessage"] == "This is a secret message");
        }
        
        [Test]
        public async Task TestSubmitAsyncNothing()
        {
            BrowserTyped browser = new BrowserTyped();
            
            IDocument<Request> response = await browser.SubmitAsync<Request>(RequestTesterRouteJsonUri);
            Request request = response.Data;
            
            Assert.IsTrue(request.Headers.Count > 0);
            
        }
        
        [Test]
        public async Task TestSubmitAsync()
        {
            BrowserTyped browser = new BrowserTyped();

            Dictionary<string, string> formData = new Dictionary<string, string>();
            formData.Add("Username","FakeUserName");
            formData.Add("Password", "FakePassword123");
            formData.Add("SecretMessage", "This is a secret message");
            
            IDocument<Request> response = await browser.SubmitAsync<Request>(RequestTesterRouteJsonUri, formData);
            Request request = response.Data;
            
            Assert.IsTrue(request.FormData["Username"] == "FakeUserName");
            Assert.IsTrue(request.FormData["Password"] == "FakePassword123");
            Assert.IsTrue(request.FormData["SecretMessage"] == "This is a secret message");
        }
        
        
        [Test]
        public async Task TestSubmitAsyncHeaders()
        {
            BrowserTyped browser = new BrowserTyped();

            Dictionary<string, string> formData = new Dictionary<string, string>();
            formData.Add("Username","FakeUserName");
            formData.Add("Password", "FakePassword123");
            formData.Add("SecretMessage", "This is a secret message");
            
            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("x-csrf-token", "axsd82os21");
            
            IDocument<Request> response = await browser.SubmitAsync<Request>(RequestTesterRouteJsonUri, formData, headers);
            Request request = response.Data;
            Assert.IsTrue(request.Headers["x-csrf-token"] == "axsd82os21");
            Assert.IsTrue(request.FormData["Username"] == "FakeUserName");
            Assert.IsTrue(request.FormData["Password"] == "FakePassword123");
            Assert.IsTrue(request.FormData["SecretMessage"] == "This is a secret message");
        }
        
        [Test]
        public void TestDocumentToTypedDocument()
        {
            BrowserTyped browser = new BrowserTyped();
            
            browser.BaseUrl = new Uri("https://dog.ceo/api/breeds/image/random");
            RestRequest dogRequest = new RestRequest();
            IDocument<DogResponse> dogDocument = browser.Execute<DogResponse>(dogRequest);
            
            IDocument<DogResponse> documentGot = browser.DocumentTyped<DogResponse>();
            Assert.IsTrue(documentGot == dogDocument);
            Assert.IsTrue(documentGot is IDocument<DogResponse>);
            Assert.IsTrue(documentGot.Data is DogResponse);
            Assert.IsTrue(documentGot.Data.Status == "success");
            Assert.IsTrue(documentGot.Data.Message != null);
            Assert.IsTrue(documentGot.Data.Status == dogDocument.Data.Status);
            Assert.IsTrue(documentGot.Data.Message == dogDocument.Data.Message);
        }
        
        #region History methods
        
        [Test]
        public void TestClearHistory()
        {
            BrowserTyped browser = new BrowserTyped();
            browser.Navigate<dynamic>("http://google.com");
            Assert.True(browser.History.Count == 1);
            browser.ClearHistory();
            Assert.True(browser.History.Count == 0);
        }

        [Test]
        public void TestClearForwardHistory()
        {
            BrowserTyped browser = new BrowserTyped();
            browser.Navigate<dynamic>("http://google.com");
            Assert.True(browser.History.Count == 1);
            browser.Navigate<dynamic>("https://facebook.com");
            Assert.True(browser.History.Count == 2);
            browser.Back();
            Assert.True(browser.History.Count == 1);
            Assert.True(browser.ForwardHistory.Count == 1);
            browser.ClearForwardHistory();
            Assert.True(browser.ForwardHistory.Count == 0);
        }

        [Test]
        public void TestBack()
        {
            BrowserTyped browser = new BrowserTyped();
            IDocument firstResponseDocument = browser.Navigate<dynamic>("http://google.com");
            Assert.True(firstResponseDocument == browser.Document);
            Assert.True(browser.History.Count == 1);
            browser.Navigate<dynamic>("https://facebook.com");
            Assert.True(browser.History.Count == 2);
            browser.Back();
            Assert.True(browser.History.Count == 1);
            Assert.True(browser.ForwardHistory.Count == 1);
            Assert.True(browser.Document != firstResponseDocument);
            Assert.True(browser.Document.Response.ResponseUri == firstResponseDocument.Response.ResponseUri);
        }
        
        [Test]
        public void TestBackCache()
        {
            BrowserTyped browser = new BrowserTyped();
            IDocument firstResponseDocument = browser.Navigate<dynamic>("http://google.com");
            Assert.True(firstResponseDocument == browser.Document);
            Assert.True(browser.History.Count == 1);
            browser.Navigate<dynamic>("https://facebook.com");
            Assert.True(browser.History.Count == 2);
            browser.Back(true);
            Assert.True(browser.History.Count == 1);
            Assert.True(browser.ForwardHistory.Count == 1);
            Assert.True(browser.Document == firstResponseDocument);

        }

        [Test]
        public async Task TestBackAsync()
        {
            BrowserTyped browser = new BrowserTyped();
            IDocument firstResponseDocument = browser.Navigate<dynamic>("http://google.com");
            Assert.True(firstResponseDocument == browser.Document);
            Assert.True(browser.History.Count == 1);
            browser.Navigate<dynamic>("https://facebook.com");
            Assert.True(browser.History.Count == 2);
            await browser.BackAsync();
            Assert.True(browser.History.Count == 1);
            Assert.True(browser.ForwardHistory.Count == 1);
            Assert.True(browser.Document != firstResponseDocument);
            Assert.True(browser.Document.Response.ResponseUri == firstResponseDocument.Response.ResponseUri);

        }
        
        [Test]
        public async Task TestBackAsyncCache()
        {
            BrowserTyped browser = new BrowserTyped();
            IDocument firstResponseDocument = browser.Navigate<dynamic>("http://google.com");
            Assert.True(firstResponseDocument == browser.Document);
            Assert.True(browser.History.Count == 1);
            browser.Navigate<dynamic>("https://facebook.com");
            Assert.True(browser.History.Count == 2);
            await browser.BackAsync(true);
            Assert.True(browser.History.Count == 1);
            Assert.True(browser.ForwardHistory.Count == 1);
            Assert.True(browser.Document == firstResponseDocument);

        }

        [Test]
        public void TestForward()
        {
            BrowserTyped browser = new BrowserTyped();
            IDocument firstResponseDocument = browser.Navigate<dynamic>("http://google.com");
            Assert.True(firstResponseDocument == browser.Document);
            Assert.True(browser.History.Count == 1);
            IDocument secondDocument = browser.Navigate<dynamic>("https://facebook.com");
            Assert.True(browser.History.Count == 2);
            browser.Back();
            Assert.True(browser.History.Count == 1);
            Assert.True(browser.ForwardHistory.Count == 1);
            Assert.True(browser.Document != firstResponseDocument);
            Assert.True(browser.Document.Response.ResponseUri == firstResponseDocument.Response.ResponseUri);
            
            browser.Forward();
            Assert.True(browser.ForwardHistory.Count == 0);
            Assert.True(browser.History.Count == 2);
            Assert.True(secondDocument.Response.ResponseUri == browser.Document.Response.ResponseUri);
        }
        
        [Test]
        public void TestForwardCache()
        {
            BrowserTyped browser = new BrowserTyped();
            IDocument firstResponseDocument = browser.Navigate<dynamic>("http://google.com");
            Assert.True(firstResponseDocument == browser.Document);
            Assert.True(browser.History.Count == 1);
            IDocument secondDocument = browser.Navigate<dynamic>("https://facebook.com");
            Assert.True(browser.History.Count == 2);
            browser.Back();
            Assert.True(browser.History.Count == 1);
            Assert.True(browser.ForwardHistory.Count == 1);
            Assert.True(browser.Document != firstResponseDocument);
            Assert.True(browser.Document.Response.ResponseUri == firstResponseDocument.Response.ResponseUri);
            
            browser.Forward(true);
            Assert.True(browser.ForwardHistory.Count == 0);
            Assert.True(browser.History.Count == 2);
            Assert.True(secondDocument == browser.Document);
        }
        
        [Test]
        public async Task TestForwardAsync()
        {
            BrowserTyped browser = new BrowserTyped();
            IDocument firstResponseDocument = await browser.NavigateAsync<dynamic>("http://google.com");
            Assert.True(firstResponseDocument == browser.Document);
            Assert.True(browser.History.Count == 1);
            IDocument secondDocument = await browser.NavigateAsync<dynamic>("https://facebook.com");
            Assert.True(browser.History.Count == 2);
            await browser.BackAsync();
            Assert.True(browser.History.Count == 1);
            Assert.True(browser.ForwardHistory.Count == 1);
            Assert.True(browser.Document != firstResponseDocument);
            Assert.True(browser.Document.Response.ResponseUri == firstResponseDocument.Response.ResponseUri);
            
            await browser.ForwardAsync(true);
            Assert.True(browser.ForwardHistory.Count == 0);
            Assert.True(browser.History.Count == 2);
            Assert.True(secondDocument == browser.Document);
        }

        [Test]
        public async Task TestForwardAsyncCache()
        {
            BrowserTyped browser = new BrowserTyped();
            IDocument firstResponseDocument = await browser.NavigateAsync<dynamic>("http://google.com");
            Assert.True(firstResponseDocument == browser.Document);
            Assert.True(browser.History.Count == 1);
            IDocument secondDocument = await browser.NavigateAsync<dynamic>("https://facebook.com");
            Assert.True(browser.History.Count == 2);
            await browser.BackAsync();
            Assert.True(browser.History.Count == 1);
            Assert.True(browser.ForwardHistory.Count == 1);
            Assert.True(browser.Document != firstResponseDocument);
            Assert.True(browser.Document.Response.ResponseUri == firstResponseDocument.Response.ResponseUri);
            
            await browser.ForwardAsync();
            Assert.True(browser.ForwardHistory.Count == 0);
            Assert.True(browser.History.Count == 2);
            Assert.True(secondDocument.Response.ResponseUri == browser.Document.Response.ResponseUri);
        }

        [Test]
        public async Task TestMaxHistorySize()
        {
            BrowserTyped browser = new BrowserTyped();
            browser.MaxHistorySize = 2;
            IDocument document1 = await browser.NavigateAsync<dynamic>("https://github.com");
            Assert.True(document1 == browser.Document);
            IDocument document2 = await browser.NavigateAsync<dynamic>(RequestTesterRouteUri);
            Assert.True(document2 == browser.Document);
            IDocument document3 = await browser.NavigateAsync<dynamic>("https://nuget.org");
            Assert.True(document3 == browser.Document);
            Assert.True(browser.History.Count == 2);
            Assert.True(browser.Documents[0] == document2);
        }
        
        [Test]
        public async Task TestMaxHistorySizeUnlimited()
        {
            BrowserTyped browser = new BrowserTyped();
        
            IDocument document1 = await browser.NavigateAsync<dynamic>("https://github.com");
            Assert.True(document1 == browser.Document);
            IDocument document2 = await browser.NavigateAsync<dynamic>(RequestTesterRouteUri);
            Assert.True(document2 == browser.Document);
            IDocument document3 = await browser.NavigateAsync<dynamic>("https://nuget.org");
            Assert.True(document3 == browser.Document);
            await browser.NavigateAsync<dynamic>("https://github.com");
            await browser.NavigateAsync<dynamic>(RequestTesterRouteUri);
            await browser.NavigateAsync<dynamic>("https://nuget.org");
            
            await browser.NavigateAsync<dynamic>("https://github.com");
            await browser.NavigateAsync<dynamic>(RequestTesterRouteUri);
            await browser.NavigateAsync<dynamic>("https://nuget.org");
            
            await browser.NavigateAsync<dynamic>("https://github.com");
            await browser.NavigateAsync<dynamic>(RequestTesterRouteUri);
            await browser.NavigateAsync<dynamic>("https://nuget.org");
            
            Assert.True(browser.History.Count == 12);
        }
        
        [Test]
        public void TestRefresh()
        {
            BrowserTyped browser = new BrowserTyped();
            IDocument document = browser.Navigate<dynamic>("https://github.com");
            IDocument documentRefreshed = browser.Refresh();
            Assert.True(documentRefreshed == browser.Document);
            Assert.True(documentRefreshed != document);
            IDocument secondDocument = browser.Navigate<dynamic>(RequestTesterRouteUri);
            IDocument thirdDocument = browser.Navigate<dynamic>("https://nuget.org");
            browser.Back();
            browser.Refresh();
            Assert.True(browser.Document != secondDocument);
            Assert.True(browser.Document.Response.ResponseUri == secondDocument.Response.ResponseUri);
        }

        [Test]
        public async Task TestRefreshAsync()
        {
            BrowserTyped browser = new BrowserTyped();
            IDocument document = await browser.NavigateAsync<dynamic>("https://github.com");
            IDocument documentRefreshed = await browser.RefreshAsync();
            Assert.True(documentRefreshed == browser.Document);
            Assert.True(documentRefreshed != document);
            IDocument secondDocument = browser.Navigate<dynamic>(RequestTesterRouteUri);
            IDocument thirdDocument = browser.Navigate<dynamic>("https://nuget.org");
            browser.Back();
            await browser.RefreshAsync();
            Assert.True(browser.Document != secondDocument);
            Assert.True(browser.Document.Response.ResponseUri == secondDocument.Response.ResponseUri);
        }
        #endregion
        
        #endregion
        
        [Test]
        public void TestJavascriptScrapingEnabled()
        {
            BrowserTyped browser = new BrowserTyped();
            browser.JavascriptScrapingEnabled = true;
            IDocument document = browser.Navigate<dynamic>("https://jayx239.github.io/BrowseSharpTest/");
            Assert.True(document.Scripts.Count > 0);
        }
        
        [Test]
        public void TestJavascriptScrapingDisabled()
        {
            BrowserTyped browser = new BrowserTyped();
            browser.JavascriptScrapingEnabled = false;
            IDocument document = browser.Navigate<dynamic>("https://jayx239.github.io/BrowseSharpTest/");
            Assert.True(document.Scripts.Count == 0);
        }
        
        [Test]
        public async Task TestJavascriptScrapingEnabledAsync()
        {
            BrowserTyped browser = new BrowserTyped();
            browser.JavascriptScrapingEnabled = true;
            IDocument document = await browser.NavigateAsync<dynamic>("https://jayx239.github.io/BrowseSharpTest/");
            Assert.True(document.Scripts.Count > 0);
        }
        
        [Test]
        public async Task TestJavascriptScrapingDisabledAsync()
        {
            BrowserTyped browser = new BrowserTyped();
            browser.JavascriptScrapingEnabled = false;
            IDocument document = await browser.NavigateAsync<dynamic>("https://jayx239.github.io/BrowseSharpTest/");
            Assert.True(document.Scripts.Count == 0);
        }
        
        [Test]
        public void TestStyleScrapingEnabled()
        {
            BrowserTyped browser = new BrowserTyped();
            browser.StyleScrapingEnabled = true;
            IDocument document = browser.Navigate<dynamic>("https://jayx239.github.io/BrowseSharpTest/");
            Assert.True(document.Styles.Count > 0);
        }
        
        [Test]
        public void TestStyleScrapingDisabled()
        {
            BrowserTyped browser = new BrowserTyped();
            browser.StyleScrapingEnabled = false;
            IDocument document = browser.Navigate<dynamic>("https://jayx239.github.io/BrowseSharpTest/");
            Assert.True(document.Styles.Count == 0);
        }
        
        [Test]
        public async Task TestStyleScrapingEnabledAsync()
        {
            BrowserTyped browser = new BrowserTyped();
            browser.StyleScrapingEnabled = true;
            IDocument document = await browser.NavigateAsync<dynamic>("https://jayx239.github.io/BrowseSharpTest/");
            Assert.True(document.Styles.Count > 0);
        }
        
        [Test]
        public async Task TestStyleScrapingDisabledAsync()
        {
            BrowserTyped browser = new BrowserTyped();
            browser.StyleScrapingEnabled = false;
            IDocument document = await browser.NavigateAsync<dynamic>("https://jayx239.github.io/BrowseSharpTest/");
            Assert.True(document.Styles.Count == 0);
        }

        [Test]
        public void TestSubmitForm()
        {
            BrowserTyped browser = new BrowserTyped();
            browser.Navigate<dynamic>("https://www.browsesharp.org/testsitesforms.html");
            Form postForm = browser.Document.Forms[0];
            Form getForm = browser.Document.Forms[1];
            postForm.SetValue("UserName", "TestUser");
            postForm.SetValue("Password", "TestPassword");
            
            IDocument<Request> postResponse = browser.SubmitForm<Request>(postForm);
            Request postResponseJson = postResponse.Data;
            Assert.True(postResponseJson.FormData["UserName"] == "TestUser");
            Assert.True(postResponseJson.FormData["Password"] == "TestPassword");

            getForm.SetValue("Message", "This is the test message");
            getForm.SetValue("Email", "testemail@test.com");
            getForm.SetValue("Rating", "3");
            IDocument<Request> getResponse = browser.SubmitForm<Request>(getForm);
            Request getResponseJson = getResponse.Data;
            Assert.True(getResponseJson.Query["Message"] == "This is the test message");
            Assert.True(getResponseJson.Query["Email"] == "testemail@test.com");
            Assert.True(getResponseJson.Query["Rating"] == "3");
        }
        
        [Test]
        public void TestSubmitFormHeaders()
        {
            BrowserTyped browser = new BrowserTyped();
            browser.Navigate<dynamic>("https://www.browsesharp.org/testsitesforms.html");
            Form postForm = browser.Document.Forms[0];
            Form getForm = browser.Document.Forms[1];
            postForm.SetValue("UserName", "TestUser");
            postForm.SetValue("Password", "TestPassword");
            
            Dictionary<string, string> postHeaders = new Dictionary<string, string>();
            postHeaders.Add("x-my-header","MyHeaderValue");
            postHeaders.Add("other-header","this is the other header");
            
            IDocument<Request> postResponse = browser.SubmitForm<Request>(postForm, postHeaders);
            Request postResponseJson = postResponse.Data;
            Assert.True(postResponseJson.FormData["UserName"] == "TestUser");
            Assert.True(postResponseJson.FormData["Password"] == "TestPassword");

            Assert.True(postResponseJson.Headers["x-my-header"] == "MyHeaderValue");
            Assert.True(postResponseJson.Headers["other-header"] == "this is the other header");
            
            getForm.SetValue("Message", "This is the test message");
            getForm.SetValue("Email", "testemail@test.com");
            getForm.SetValue("Rating", "3");
            
            Dictionary<string, string> getHeaders = new Dictionary<string, string>();
            getHeaders.Add("get-header","This is a get header");
            getHeaders.Add("other-header","Other header value");
            getHeaders.Add("athirdheader", "A 3rd header");
            
            IDocument<Request> getResponse = browser.SubmitForm<Request>(getForm, getHeaders);
            Request getResponseJson = getResponse.Data;
            Assert.True(getResponseJson.Query["Message"] == "This is the test message");
            Assert.True(getResponseJson.Query["Email"] == "testemail@test.com");
            Assert.True(getResponseJson.Query["Rating"] == "3");
            
            Assert.True(getResponseJson.Headers["get-header"].ToString() == "This is a get header");
            Assert.True(getResponseJson.Headers["other-header"].ToString() == "Other header value");
            Assert.True(getResponseJson.Headers["athirdheader"].ToString() == "A 3rd header");
        }
        
        
        [Test]
        public async Task TestSubmitFormAsync()
        {
            BrowserTyped browser = new BrowserTyped();
            browser.Navigate<dynamic>("https://www.browsesharp.org/testsitesforms.html");
            Form postForm = browser.Document.Forms[0];
            Form getForm = browser.Document.Forms[1];
            postForm.SetValue("UserName", "TestUser");
            postForm.SetValue("Password", "TestPassword");
            
            IDocument<Request> postResponse = await browser.SubmitFormAsync<Request>(postForm);
            Request postResponseJson = postResponse.Data;
            Assert.True(postResponseJson.FormData["UserName"] == "TestUser");
            Assert.True(postResponseJson.FormData["Password"] == "TestPassword");

            getForm.SetValue("Message", "This is the test message");
            getForm.SetValue("Email", "testemail@test.com");
            getForm.SetValue("Rating", "3");
            IDocument<Request> getResponse = await browser.SubmitFormAsync<Request>(getForm);
            Request getResponseJson = getResponse.Data;
            Assert.True(getResponseJson.Query["Message"] == "This is the test message");
            Assert.True(getResponseJson.Query["Email"] == "testemail@test.com");
            Assert.True(getResponseJson.Query["Rating"] == "3");
        }
        
        [Test]
        public async Task TestSubmitFormAsyncHeaders()
        {
            BrowserTyped browser = new BrowserTyped();
            browser.Navigate<dynamic>("https://www.browsesharp.org/testsitesforms.html");
            Form postForm = browser.Document.Forms[0];
            Form getForm = browser.Document.Forms[1];
            postForm.SetValue("UserName", "TestUser");
            postForm.SetValue("Password", "TestPassword");
            
            Dictionary<string, string> postHeaders = new Dictionary<string, string>();
            postHeaders.Add("x-my-header","MyHeaderValue");
            postHeaders.Add("other-header","this is the other header");
            
            IDocument<Request> postResponse = await browser.SubmitFormAsync<Request>(postForm, postHeaders);
            Request postResponseJson = postResponse.Data;
            Assert.True(postResponseJson.FormData["UserName"] == "TestUser");
            Assert.True(postResponseJson.FormData["Password"] == "TestPassword");

            Assert.True(postResponseJson.Headers["x-my-header"] == "MyHeaderValue");
            Assert.True(postResponseJson.Headers["other-header"] == "this is the other header");
            
            getForm.SetValue("Message", "This is the test message");
            getForm.SetValue("Email", "testemail@test.com");
            getForm.SetValue("Rating", "3");
            
            Dictionary<string, string> getHeaders = new Dictionary<string, string>();
            getHeaders.Add("get-header","This is a get header");
            getHeaders.Add("other-header","Other header value");
            getHeaders.Add("athirdheader", "A 3rd header");
            
            IDocument<Request> getResponse = await browser.SubmitFormAsync<Request>(getForm, getHeaders);
            dynamic getResponseJson = getResponse.Data;
            Assert.True(getResponseJson.Query["Message"] == "This is the test message");
            Assert.True(getResponseJson.Query["Email"] == "testemail@test.com");
            Assert.True(getResponseJson.Query["Rating"] == "3");
            
            Assert.True(getResponseJson.Headers["get-header"] == "This is a get header");
            Assert.True(getResponseJson.Headers["other-header"] == "Other header value");
            Assert.True(getResponseJson.Headers["athirdheader"] == "A 3rd header");
        }

        /// <summary>
        /// Tests that the submit and navigate methods work with a uri that does not have a protocol attached
        /// </summary>
        [Test]
        public void TestNoProtoclNavigateSubmit()
        {
            BrowserTyped browser = new BrowserTyped();
            browser.Navigate<dynamic>("google.com");
            browser.Navigate<dynamic>("google.com", null);
            browser.Navigate<dynamic>("google.com", null, null);
            browser.Submit<dynamic>("google.com");
            browser.Submit<dynamic>("google.com", null);
            browser.Submit<dynamic>("google.com", null,null);
        }
        
        /// <summary>
        /// Tests that the submit and navigate methods work with a uri that does not have a protocol attached
        /// </summary>
        [Test]
        public async Task TestNoProtoclNavigateSubmitAsync()
        {
            BrowserTyped browser = new BrowserTyped();
            await browser.NavigateAsync<dynamic>("google.com");
            await browser.NavigateAsync<dynamic>("google.com", null);
            await browser.NavigateAsync<dynamic>("google.com", null, null);
            await browser.SubmitAsync<dynamic>("google.com");
            await browser.SubmitAsync<dynamic>("google.com", null);
            await browser.SubmitAsync<dynamic>("google.com", null,null);
        }
    }
}