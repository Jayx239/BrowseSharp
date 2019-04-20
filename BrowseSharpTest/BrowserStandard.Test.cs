using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using BrowseSharp;
using BrowseSharp.Browsers;
using BrowseSharp.Types;
using BrowseSharp.Types.Html;
using NUnit.Framework;
using RestSharp;

/* Several tests depend on the RequestTester nodejs project
 You can find this project at https://github.com/Jayx239/RequestTester
 Be sure to set the proper port number that your RequestTester is listening on
 */

namespace BrowseSharpTest
{
    [TestFixture]
    public class BrowserStandardTest
    {
        /* RequestTester Configuration */
        public static int RequestTesterPort = 3000; // This is the port your RequestTester application is listening to
        public static string RequestTesterRouteUri = "https://requesttester.com/tester/view" ;//"http://localhost:" + RequestTesterPort + "/tester/view";
        
        [Test]
        public void TestExecute()
        {
            BrowserStandard browser = new BrowserStandard();
            browser.BaseUrl = new Uri("https://jayx239.github.io/BrowseSharpTest/");
            RestRequest request = new RestRequest();
            browser.Execute(request);
            Assert.True(browser.Documents.Count == 1);
            Assert.True(browser.Documents[0].Scripts.Count == 7);
            foreach (var script in browser.Documents[0].Scripts)
            {
                Assert.NotNull(script.SourceUri);
                Assert.NotNull(script.JavascriptString);
            }

            Assert.True(browser.Documents[0].Styles.Count == 2);
            foreach (var style in browser.Documents[0].Styles)
            {
                Assert.True(!string.IsNullOrEmpty(style.Content));
                Assert.NotNull(style.SourceUri);
            }

        }

        [Test]
        public void TestExecuteAsGet()
        {
            BrowserStandard browser = new BrowserStandard();
            browser.BaseUrl = new Uri("https://jayx239.github.io/BrowseSharpTest/");
            RestRequest request = new RestRequest();
            browser.ExecuteAsGet(request,"GET");
            Assert.True(browser.Documents.Count == 1);
            Assert.True(browser.Documents[0].Scripts.Count == 7);
            foreach (var script in browser.Documents[0].Scripts)
            {
                Assert.NotNull(script.SourceUri);
                Assert.NotNull(script.JavascriptString);
            }

            Assert.True(browser.Documents[0].Styles.Count == 2);
            foreach (var style in browser.Documents[0].Styles)
            {
                Assert.True(!string.IsNullOrEmpty(style.Content));
                Assert.NotNull(style.SourceUri);
            }
        }

        [Test]
        public void TestExecuteAsPost()
        {
            BrowserStandard browser = new BrowserStandard();
            browser.BaseUrl = new Uri("https://www.hashemian.com/tools/form-post-tester.php");
            IRestRequest request = new RestRequest();
            
            request.AddParameter("Username","FakeUserName");
            request.AddParameter("Password", "FakePassword123");
            request.AddParameter("SecretMessage", "This is a secret message");
            var response = browser.ExecuteAsPost(request,"Post");
            Assert.True(response.Response.Content.Contains("Username=FakeUserName"));
            Assert.True(response.Response.Content.Contains("Password=FakePassword123"));
            Assert.True(response.Response.Content.Contains("SecretMessage=This%20is%20a%20secret%20message"));
            
            var response2 = browser.Documents[0];
            Assert.True(response2.Response.Content.Contains("Username=FakeUserName"));
            Assert.True(response2.Response.Content.Contains("Password=FakePassword123"));
            Assert.True(response2.Response.Content.Contains("SecretMessage=This%20is%20a%20secret%20message"));

        }

        [Test]
        public async Task TestExecuteTaskAsyncToken()
        {
            BrowserStandard browser = new BrowserStandard();
            browser.BaseUrl = new Uri("https://jayx239.github.io/BrowseSharpTest/");
            RestRequest request = new RestRequest();

            CancellationToken cancellationToken = new CancellationToken();
            var response = await browser.ExecuteTaskAsync(request, cancellationToken);
            
            Assert.True(browser.Documents.Count == 1);
            Assert.True(browser.Documents[0].Scripts.Count == 7);
            foreach (var script in browser.Documents[0].Scripts)
            {
                Assert.NotNull(script.SourceUri);
                Assert.NotNull(script.JavascriptString);
            }
            
            Assert.True(browser.Documents[0].Styles.Count == 2);
            foreach (var style in browser.Documents[0].Styles)
            {
                Assert.True(!string.IsNullOrEmpty(style.Content));
                Assert.NotNull(style.SourceUri);
            }
            
            Assert.True(response.Scripts.Count == 7);
            foreach (var script in response.Scripts)
            {
                Assert.NotNull(script.SourceUri);
                Assert.NotNull(script.JavascriptString);
            }
            
            Assert.True(response.Styles.Count == 2);
            foreach (var style in response.Styles)
            {
                Assert.True(!string.IsNullOrEmpty(style.Content));
                Assert.NotNull(style.SourceUri);
            }

        }
        
        [Test]
        public async Task TestExecuteTaskAsync()
        {
            BrowserStandard browser = new BrowserStandard();
            browser.BaseUrl = new Uri("https://jayx239.github.io/BrowseSharpTest/");
            RestRequest request = new RestRequest();
            
            var response = await browser.ExecuteTaskAsync(request);
            
            Assert.True(browser.Documents.Count == 1);
            Assert.True(browser.Documents[0].Scripts.Count == 7);
            foreach (var script in browser.Documents[0].Scripts)
            {
                Assert.NotNull(script.SourceUri);
                Assert.NotNull(script.JavascriptString);
            }
            
            Assert.True(browser.Documents[0].Styles.Count == 2);
            foreach (var style in browser.Documents[0].Styles)
            {
                Assert.True(!string.IsNullOrEmpty(style.Content));
                Assert.NotNull(style.SourceUri);
            }
            
            Assert.True(response.Scripts.Count == 7);
            foreach (var script in response.Scripts)
            {
                Assert.NotNull(script.SourceUri);
                Assert.NotNull(script.JavascriptString);
            }
            
            Assert.True(response.Styles.Count == 2);
            foreach (var style in response.Styles)
            {
                Assert.True(!string.IsNullOrEmpty(style.Content));
                Assert.NotNull(style.SourceUri);
            }

        }

        [Test]
        public async Task TestExecuteGetTaskAsync()
        {
            BrowserStandard browser = new BrowserStandard();
            browser.BaseUrl = new Uri("https://jayx239.github.io/BrowseSharpTest/");
            RestRequest request = new RestRequest();
            
            var response = await browser.ExecuteGetTaskAsync(request);
            
            Assert.True(browser.Documents.Count == 1);
            Assert.True(browser.Documents[0].Scripts.Count == 7);
            foreach (var script in browser.Documents[0].Scripts)
            {
                Assert.NotNull(script.SourceUri);
                Assert.NotNull(script.JavascriptString);
            }
            
            Assert.True(browser.Documents[0].Styles.Count == 2);
            foreach (var style in browser.Documents[0].Styles)
            {
                Assert.True(!string.IsNullOrEmpty(style.Content));
                Assert.NotNull(style.SourceUri);
            }
            
            Assert.True(response.Scripts.Count == 7);
            foreach (var script in response.Scripts)
            {
                Assert.NotNull(script.SourceUri);
                Assert.NotNull(script.JavascriptString);
            }
            
            Assert.True(response.Styles.Count == 2);
            foreach (var style in response.Styles)
            {
                Assert.True(!string.IsNullOrEmpty(style.Content));
                Assert.NotNull(style.SourceUri);
            }

        }

        [Test]
        public async Task TestExecuteGetTaskAsyncToken()
        {
            BrowserStandard browser = new BrowserStandard();
            browser.BaseUrl = new Uri("https://jayx239.github.io/BrowseSharpTest/");
            RestRequest request = new RestRequest();
            
            CancellationToken cancellationToken = new CancellationToken();
            var response = await browser.ExecuteGetTaskAsync(request, cancellationToken);
            
            Assert.True(browser.Documents.Count == 1);
            Assert.True(browser.Documents[0].Scripts.Count == 7);
            foreach (var script in browser.Documents[0].Scripts)
            {
                Assert.NotNull(script.SourceUri);
                Assert.NotNull(script.JavascriptString);
            }
            
            Assert.True(browser.Documents[0].Styles.Count == 2);
            foreach (var style in browser.Documents[0].Styles)
            {
                Assert.True(!string.IsNullOrEmpty(style.Content));
                Assert.NotNull(style.SourceUri);
            }
            
            Assert.True(response.Scripts.Count == 7);
            foreach (var script in response.Scripts)
            {
                Assert.NotNull(script.SourceUri);
                Assert.NotNull(script.JavascriptString);
            }
            
            Assert.True(response.Styles.Count == 2);
            foreach (var style in response.Styles)
            {
                Assert.True(!string.IsNullOrEmpty(style.Content));
                Assert.NotNull(style.SourceUri);
            }

        }

        [Test]
        public async Task TestExecutePostTaskAsync()
        {
            BrowserStandard browser = new BrowserStandard();
            browser.BaseUrl = new Uri("https://www.hashemian.com/tools/form-post-tester.php");
            IRestRequest request = new RestRequest();
            
            request.AddParameter("Username","FakeUserName");
            request.AddParameter("Password", "FakePassword123");
            request.AddParameter("SecretMessage", "This is a secret message");
            var response = await browser.ExecutePostTaskAsync(request);
            Assert.True(response.Response.Content.Contains("Username=FakeUserName"));
            Assert.True(response.Response.Content.Contains("Password=FakePassword123"));
            Assert.True(response.Response.Content.Contains("SecretMessage=This%20is%20a%20secret%20message"));
            
            var response2 = browser.Documents[0];
            Assert.True(response2.Response.Content.Contains("Username=FakeUserName"));
            Assert.True(response2.Response.Content.Contains("Password=FakePassword123"));
            Assert.True(response2.Response.Content.Contains("SecretMessage=This%20is%20a%20secret%20message"));

        }

        [Test]
        public async Task TestExecutePostTaskAsyncToken()
        {
            BrowserStandard browser = new BrowserStandard();
            browser.BaseUrl = new Uri("https://www.hashemian.com/tools/form-post-tester.php");
            IRestRequest request = new RestRequest();
            
            request.AddParameter("Username","FakeUserName");
            request.AddParameter("Password", "FakePassword123");
            request.AddParameter("SecretMessage", "This is a secret message");
            CancellationToken cancellationToken = new CancellationToken();
            var response = await browser.ExecutePostTaskAsync(request,cancellationToken);
            Assert.True(response.Response.Content.Contains("Username=FakeUserName"));
            Assert.True(response.Response.Content.Contains("Password=FakePassword123"));
            Assert.True(response.Response.Content.Contains("SecretMessage=This%20is%20a%20secret%20message"));
            
            var response2 = browser.Documents[0];
            Assert.True(response2.Response.Content.Contains("Username=FakeUserName"));
            Assert.True(response2.Response.Content.Contains("Password=FakePassword123"));
            Assert.True(response2.Response.Content.Contains("SecretMessage=This%20is%20a%20secret%20message"));
        }

        [Test]
        public void TestNavigate()
        {
            BrowserStandard browser = new BrowserStandard();
            
            var response = browser.Navigate("https://jayx239.github.io/BrowseSharpTest/");
            
            Assert.True(browser.Documents.Count == 1);
            Assert.True(browser.Documents[0].Scripts.Count == 7);
            foreach (var script in browser.Documents[0].Scripts)
            {
                Assert.NotNull(script.SourceUri);
                Assert.NotNull(script.JavascriptString);
            }
            
            Assert.True(browser.Documents[0].Styles.Count == 2);
            foreach (var style in browser.Documents[0].Styles)
            {
                Assert.True(!string.IsNullOrEmpty(style.Content));
                Assert.NotNull(style.SourceUri);
            }
            
            Assert.True(response.Scripts.Count == 7);
            foreach (var script in response.Scripts)
            {
                Assert.NotNull(script.SourceUri);
                Assert.NotNull(script.JavascriptString);
            }
            
            Assert.True(response.Styles.Count == 2);
            foreach (var style in response.Styles)
            {
                Assert.True(!string.IsNullOrEmpty(style.Content));
                Assert.NotNull(style.SourceUri);
            }

        }
        
        /* Uses RequestTester node.js project */
        [Test]
        public void TestNavigateHeaders()
        {
            BrowserStandard browser = new BrowserStandard();

            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("x-csrf-token", "axsd82os21");
            
            var response = browser.Navigate(RequestTesterRouteUri, headers);

            foreach (var header in headers)
            {
                Assert.AreEqual(header.Value, response.HtmlDocument.QuerySelector("#" + header.Key).TextContent);
            }
        }
        
        /* Uses RequestTester node.js project */
        [Test]
        public void TestNavigateHeadersAndData()
        {
            BrowserStandard browser = new BrowserStandard();
            
            Dictionary<string, string> formData = new Dictionary<string, string>();
            formData.Add("Username","FakeUserName");
            formData.Add("Password", "FakePassword123");
            formData.Add("SecretMessage", "This is a secret message");
            
            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("x-csrf-token", "axsd82os21");
            
            var response = browser.Navigate(RequestTesterRouteUri, headers, formData);
            foreach (var data in formData)
            {
                Assert.AreEqual(data.Value,response.HtmlDocument.QuerySelector("#" + data.Key).TextContent);
            }
            
            foreach (var header in headers)
            {
                Assert.AreEqual(header.Value, response.HtmlDocument.QuerySelector("#" + header.Key).TextContent);
            }
        }
        
        [Test]
        public async Task TestNavigateAsync()
        {
            BrowserStandard browser = new BrowserStandard();
            
            var response = await browser.NavigateAsync("https://jayx239.github.io/BrowseSharpTest/");
            
            Assert.True(browser.Documents.Count == 1);
            Assert.True(browser.Documents[0].Scripts.Count == 7);
            foreach (var script in browser.Documents[0].Scripts)
            {
                Assert.NotNull(script.SourceUri);
                Assert.NotNull(script.JavascriptString);
            }
            
            Assert.True(browser.Documents[0].Styles.Count == 2);
            foreach (var style in browser.Documents[0].Styles)
            {
                Assert.True(!string.IsNullOrEmpty(style.Content));
                Assert.NotNull(style.SourceUri);
            }
            
            Assert.True(response.Scripts.Count == 7);
            foreach (var script in response.Scripts)
            {
                Assert.NotNull(script.SourceUri);
                Assert.NotNull(script.JavascriptString);
            }
            
            Assert.True(response.Styles.Count == 2);
            foreach (var style in response.Styles)
            {
                Assert.True(!string.IsNullOrEmpty(style.Content));
                Assert.NotNull(style.SourceUri);
            }

        }

        /* Uses RequestTester node.js project */
        [Test]
        public async Task TestNavigateAsyncHeaders()
        {
            BrowserStandard browser = new BrowserStandard();

            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("x-csrf-token", "axsd82os21");
            
            var response = await browser.NavigateAsync(RequestTesterRouteUri, headers);

            foreach (var header in headers)
            {
                Assert.AreEqual(header.Value, response.HtmlDocument.QuerySelector("#" + header.Key).TextContent);
            }
        }

        /* Uses RequestTester node.js project */
        [Test]
        public async Task TestNavigateAsyncHeadersAndData()
        {
            BrowserStandard browser = new BrowserStandard();
            
            Dictionary<string, string> formData = new Dictionary<string, string>();
            formData.Add("Username","FakeUserName");
            formData.Add("Password", "FakePassword123");
            formData.Add("SecretMessage", "This is a secret message");
            
            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("x-csrf-token", "axsd82os21");
            
            var response = await browser.NavigateAsync(RequestTesterRouteUri, headers, formData);
            foreach (var data in formData)
            {
                Assert.AreEqual(data.Value,response.HtmlDocument.QuerySelector("#" + data.Key).TextContent);
            }
            
            foreach (var header in headers)
            {
                Assert.AreEqual(header.Value, response.HtmlDocument.QuerySelector("#" + header.Key).TextContent);
            }
        }
        
        [Test]
        public void TestSubmit()
        {
            BrowserStandard browser = new BrowserStandard();

            Dictionary<string, string> formData = new Dictionary<string, string>();
            formData.Add("Username","FakeUserName");
            formData.Add("Password", "FakePassword123");
            formData.Add("SecretMessage", "This is a secret message");

            var response = browser.Submit("https://www.hashemian.com/tools/form-post-tester.php", formData);
            Assert.True(response.Response.Content.Contains("Username=FakeUserName"));
            Assert.True(response.Response.Content.Contains("Password=FakePassword123"));
            Assert.True(response.Response.Content.Contains("SecretMessage=This%20is%20a%20secret%20message"));
            
            var response2 = browser.Documents[0];
            Assert.True(response2.Response.Content.Contains("Username=FakeUserName"));
            Assert.True(response2.Response.Content.Contains("Password=FakePassword123"));
            Assert.True(response2.Response.Content.Contains("SecretMessage=This%20is%20a%20secret%20message"));

        }
        
        /* Uses RequestTester node.js project */
        [Test]
        public void TestSubmitHeaders()
        {
            BrowserStandard browser = new BrowserStandard();

            Dictionary<string, string> formData = new Dictionary<string, string>();
            formData.Add("Username","FakeUserName");
            formData.Add("Password", "FakePassword123");
            formData.Add("SecretMessage", "This is a secret message");
            
            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("x-csrf-token", "axsd82os21");
            
            var response = browser.Submit(RequestTesterRouteUri, formData, headers);
            foreach (var data in formData)
            {
                Assert.AreEqual(data.Value,response.HtmlDocument.QuerySelector("#" + data.Key).TextContent);
            }

            foreach (var header in headers)
            {
                Assert.AreEqual(header.Value, response.HtmlDocument.QuerySelector("#" + header.Key).TextContent);
            }
            
        }
        
        [Test]
        public async Task TestSubmitAsync()
        {
            BrowserStandard browser = new BrowserStandard();

            Dictionary<string, string> formData = new Dictionary<string, string>();
            formData.Add("Username","FakeUserName");
            formData.Add("Password", "FakePassword123");
            formData.Add("SecretMessage", "This is a secret message");

            var response = await browser.SubmitAsync("https://www.hashemian.com/tools/form-post-tester.php", formData);
            Assert.True(response.Response.Content.Contains("Username=FakeUserName"));
            Assert.True(response.Response.Content.Contains("Password=FakePassword123"));
            Assert.True(response.Response.Content.Contains("SecretMessage=This%20is%20a%20secret%20message"));
            
            var response2 = browser.Documents[0];
            Assert.True(response2.Response.Content.Contains("Username=FakeUserName"));
            Assert.True(response2.Response.Content.Contains("Password=FakePassword123"));
            Assert.True(response2.Response.Content.Contains("SecretMessage=This%20is%20a%20secret%20message"));

        }
        
        /* Uses RequestTester node.js project */
        [Test]
        public async Task TestSubmitAsyncHeaders()
        {
            BrowserStandard browser = new BrowserStandard();

            Dictionary<string, string> formData = new Dictionary<string, string>();
            formData.Add("Username","FakeUserName");
            formData.Add("Password", "FakePassword123");
            formData.Add("SecretMessage", "This is a secret message");
            
            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("x-csrf-token", "axsd82os21");
            
            var response = await browser.SubmitAsync(RequestTesterRouteUri, formData, headers);
            foreach (var data in formData)
            {
                Assert.AreEqual(data.Value,response.HtmlDocument.QuerySelector("#" + data.Key).TextContent);
            }

            foreach (var header in headers)
            {
                Assert.AreEqual(header.Value, response.HtmlDocument.QuerySelector("#" + header.Key).TextContent);
            }
            
        }
        
        #region History methods
        
        [Test]
        public void TestClearHistory()
        {
            BrowserStandard browser = new BrowserStandard();
            browser.Navigate("http://google.com");
            Assert.True(browser.History.Count == 1);
            browser.ClearHistory();
            Assert.True(browser.History.Count == 0);
        }

        [Test]
        public void TestClearForwardHistory()
        {
            BrowserStandard browser = new BrowserStandard();
            browser.Navigate("http://google.com");
            Assert.True(browser.History.Count == 1);
            browser.Navigate("https://facebook.com");
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
            BrowserStandard browser = new BrowserStandard();
            IDocument firstResponseDocument = browser.Navigate("http://google.com");
            Assert.True(firstResponseDocument == browser.Document);
            Assert.True(browser.History.Count == 1);
            browser.Navigate("https://facebook.com");
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
            BrowserStandard browser = new BrowserStandard();
            IDocument firstResponseDocument = browser.Navigate("http://google.com");
            Assert.True(firstResponseDocument == browser.Document);
            Assert.True(browser.History.Count == 1);
            browser.Navigate("https://facebook.com");
            Assert.True(browser.History.Count == 2);
            browser.Back(true);
            Assert.True(browser.History.Count == 1);
            Assert.True(browser.ForwardHistory.Count == 1);
            Assert.True(browser.Document == firstResponseDocument);

        }

        [Test]
        public async Task TestBackAsync()
        {
            BrowserStandard browser = new BrowserStandard();
            IDocument firstResponseDocument = browser.Navigate("http://google.com");
            Assert.True(firstResponseDocument == browser.Document);
            Assert.True(browser.History.Count == 1);
            browser.Navigate("https://facebook.com");
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
            BrowserStandard browser = new BrowserStandard();
            IDocument firstResponseDocument = browser.Navigate("http://google.com");
            Assert.True(firstResponseDocument == browser.Document);
            Assert.True(browser.History.Count == 1);
            browser.Navigate("https://facebook.com");
            Assert.True(browser.History.Count == 2);
            await browser.BackAsync(true);
            Assert.True(browser.History.Count == 1);
            Assert.True(browser.ForwardHistory.Count == 1);
            Assert.True(browser.Document == firstResponseDocument);

        }

        [Test]
        public void TestForward()
        {
            BrowserStandard browser = new BrowserStandard();
            IDocument firstResponseDocument = browser.Navigate("http://google.com");
            Assert.True(firstResponseDocument == browser.Document);
            Assert.True(browser.History.Count == 1);
            IDocument secondDocument = browser.Navigate("https://facebook.com");
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
            BrowserStandard browser = new BrowserStandard();
            IDocument firstResponseDocument = browser.Navigate("http://google.com");
            Assert.True(firstResponseDocument == browser.Document);
            Assert.True(browser.History.Count == 1);
            IDocument secondDocument = browser.Navigate("https://facebook.com");
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
            BrowserStandard browser = new BrowserStandard();
            IDocument firstResponseDocument = await browser.NavigateAsync("http://google.com");
            Assert.True(firstResponseDocument == browser.Document);
            Assert.True(browser.History.Count == 1);
            IDocument secondDocument = await browser.NavigateAsync("https://facebook.com");
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
            BrowserStandard browser = new BrowserStandard();
            IDocument firstResponseDocument = await browser.NavigateAsync("http://google.com");
            Assert.True(firstResponseDocument == browser.Document);
            Assert.True(browser.History.Count == 1);
            IDocument secondDocument = await browser.NavigateAsync("https://facebook.com");
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
            BrowserStandard browser = new BrowserStandard();
            browser.MaxHistorySize = 2;
            IDocument document1 = await browser.NavigateAsync("https://github.com");
            Assert.True(document1 == browser.Document);
            IDocument document2 = await browser.NavigateAsync(RequestTesterRouteUri);
            Assert.True(document2 == browser.Document);
            IDocument document3 = await browser.NavigateAsync("https://nuget.org");
            Assert.True(document3 == browser.Document);
            Assert.True(browser.History.Count == 2);
            Assert.True(browser.Documents[0] == document2);
        }
        
        [Test]
        public async Task TestMaxHistorySizeUnlimited()
        {
            BrowserStandard browser = new BrowserStandard();
        
            IDocument document1 = await browser.NavigateAsync("https://github.com");
            Assert.True(document1 == browser.Document);
            IDocument document2 = await browser.NavigateAsync(RequestTesterRouteUri);
            Assert.True(document2 == browser.Document);
            IDocument document3 = await browser.NavigateAsync("https://nuget.org");
            Assert.True(document3 == browser.Document);
            await browser.NavigateAsync("https://github.com");
            await browser.NavigateAsync(RequestTesterRouteUri);
            await browser.NavigateAsync("https://nuget.org");
            
            await browser.NavigateAsync("https://github.com");
            await browser.NavigateAsync(RequestTesterRouteUri);
            await browser.NavigateAsync("https://nuget.org");
            
            await browser.NavigateAsync("https://github.com");
            await browser.NavigateAsync(RequestTesterRouteUri);
            await browser.NavigateAsync("https://nuget.org");
            
            Assert.True(browser.History.Count == 12);
        }
        
        [Test]
        public void TestRefresh()
        {
            BrowserStandard browser = new BrowserStandard();
            IDocument document = browser.Navigate("https://github.com");
            IDocument documentRefreshed = browser.Refresh();
            Assert.True(documentRefreshed == browser.Document);
            Assert.True(documentRefreshed != document);
            IDocument secondDocument = browser.Navigate(RequestTesterRouteUri);
            IDocument thirdDocument = browser.Navigate("https://nuget.org");
            browser.Back();
            browser.Refresh();
            Assert.True(browser.Document != secondDocument);
            Assert.True(browser.Document.Response.ResponseUri == secondDocument.Response.ResponseUri);
        }

        [Test]
        public async Task TestRefreshAsync()
        {
            BrowserStandard browser = new BrowserStandard();
            IDocument document = await browser.NavigateAsync("https://github.com");
            IDocument documentRefreshed = await browser.RefreshAsync();
            Assert.True(documentRefreshed == browser.Document);
            Assert.True(documentRefreshed != document);
            IDocument secondDocument = browser.Navigate(RequestTesterRouteUri);
            IDocument thirdDocument = browser.Navigate("https://nuget.org");
            browser.Back();
            await browser.RefreshAsync();
            Assert.True(browser.Document != secondDocument);
            Assert.True(browser.Document.Response.ResponseUri == secondDocument.Response.ResponseUri);
        }
        
        #endregion
        
        [Test]
        public void TestJavascriptScrapingEnabled()
        {
            BrowserStandard browser = new BrowserStandard();
            browser.JavascriptScrapingEnabled = true;
            IDocument document = browser.Navigate("https://jayx239.github.io/BrowseSharpTest/");
            Assert.True(document.Scripts.Count > 0);
        }
        
        [Test]
        public void TestJavascriptScrapingDisabled()
        {
            BrowserStandard browser = new BrowserStandard();
            browser.JavascriptScrapingEnabled = false;
            IDocument document = browser.Navigate("https://jayx239.github.io/BrowseSharpTest/");
            Assert.True(document.Scripts.Count == 0);
        }
        
        [Test]
        public async Task TestJavascriptScrapingEnabledAsync()
        {
            BrowserStandard browser = new BrowserStandard();
            browser.JavascriptScrapingEnabled = true;
            IDocument document = await browser.NavigateAsync("https://jayx239.github.io/BrowseSharpTest/");
            Assert.True(document.Scripts.Count > 0);
        }
        
        [Test]
        public async Task TestJavascriptScrapingDisabledAsync()
        {
            BrowserStandard browser = new BrowserStandard();
            browser.JavascriptScrapingEnabled = false;
            IDocument document = await browser.NavigateAsync("https://jayx239.github.io/BrowseSharpTest/");
            Assert.True(document.Scripts.Count == 0);
        }
        
        [Test]
        public void TestStyleScrapingEnabled()
        {
            BrowserStandard browser = new BrowserStandard();
            browser.StyleScrapingEnabled = true;
            IDocument document = browser.Navigate("https://jayx239.github.io/BrowseSharpTest/");
            Assert.True(document.Styles.Count > 0);
        }
        
        [Test]
        public void TestStyleScrapingDisabled()
        {
            BrowserStandard browser = new BrowserStandard();
            browser.StyleScrapingEnabled = false;
            IDocument document = browser.Navigate("https://jayx239.github.io/BrowseSharpTest/");
            Assert.True(document.Styles.Count == 0);
        }
        
        [Test]
        public async Task TestStyleScrapingEnabledAsync()
        {
            BrowserStandard browser = new BrowserStandard();
            browser.StyleScrapingEnabled = true;
            IDocument document = await browser.NavigateAsync("https://jayx239.github.io/BrowseSharpTest/");
            Assert.True(document.Styles.Count > 0);
        }
        
        [Test]
        public async Task TestStyleScrapingDisabledAsync()
        {
            BrowserStandard browser = new BrowserStandard();
            browser.StyleScrapingEnabled = false;
            IDocument document = await browser.NavigateAsync("https://jayx239.github.io/BrowseSharpTest/");
            Assert.True(document.Styles.Count == 0);
        }

        [Test]
        public void TestSubmitForm()
        {
            BrowserStandard browser = new BrowserStandard();
            browser.Navigate("https://www.browsesharp.org/testsitesforms.html");
            Form postForm = browser.Document.Forms[0];
            Form getForm = browser.Document.Forms[1];
            postForm.SetValue("UserName", "TestUser");
            postForm.SetValue("Password", "TestPassword");
            
            IDocument postResponse = browser.SubmitForm(postForm);
            dynamic postResponseJson = new JavaScriptSerializer().Deserialize<dynamic>(postResponse.Body.TextContent);
            Assert.True(postResponseJson["formData"]["UserName"] == "TestUser");
            Assert.True(postResponseJson["formData"]["Password"] == "TestPassword");

            getForm.SetValue("Message", "This is the test message");
            getForm.SetValue("Email", "testemail@test.com");
            getForm.SetValue("Rating", "3");
            IDocument getResponse = browser.SubmitForm(getForm);
            dynamic getResponseJson = new JavaScriptSerializer().Deserialize<dynamic>(getResponse.Body.TextContent);
            Assert.True(getResponseJson["query"]["Message"] == "This is the test message");
            Assert.True(getResponseJson["query"]["Email"] == "testemail@test.com");
            Assert.True(getResponseJson["query"]["Rating"] == "3");
        }
        
        [Test]
        public void TestSubmitFormHeaders()
        {
            BrowserStandard browser = new BrowserStandard();
            browser.Navigate("https://www.browsesharp.org/testsitesforms.html");
            Form postForm = browser.Document.Forms[0];
            Form getForm = browser.Document.Forms[1];
            postForm.SetValue("UserName", "TestUser");
            postForm.SetValue("Password", "TestPassword");
            
            Dictionary<string, string> postHeaders = new Dictionary<string, string>();
            postHeaders.Add("x-my-header","MyHeaderValue");
            postHeaders.Add("other-header","this is the other header");
            
            IDocument postResponse = browser.SubmitForm(postForm, postHeaders);
            dynamic postResponseJson = new JavaScriptSerializer().Deserialize<dynamic>(postResponse.Body.TextContent);
            Assert.True(postResponseJson["formData"]["UserName"] == "TestUser");
            Assert.True(postResponseJson["formData"]["Password"] == "TestPassword");

            Assert.True(postResponseJson["headers"]["x-my-header"] == "MyHeaderValue");
            Assert.True(postResponseJson["headers"]["other-header"].ToString() == "this is the other header");
            
            getForm.SetValue("Message", "This is the test message");
            getForm.SetValue("Email", "testemail@test.com");
            getForm.SetValue("Rating", "3");
            
            Dictionary<string, string> getHeaders = new Dictionary<string, string>();
            getHeaders.Add("get-header","This is a get header");
            getHeaders.Add("other-header","Other header value");
            getHeaders.Add("athirdheader", "A 3rd header");
            
            IDocument getResponse = browser.SubmitForm(getForm, getHeaders);
            dynamic getResponseJson = new JavaScriptSerializer().Deserialize<dynamic>(getResponse.Body.TextContent);
            Assert.True(getResponseJson["query"]["Message"] == "This is the test message");
            Assert.True(getResponseJson["query"]["Email"] == "testemail@test.com");
            Assert.True(getResponseJson["query"]["Rating"] == "3");
            
            Assert.True(getResponseJson["headers"]["get-header"] == "This is a get header");
            Assert.True(getResponseJson["headers"]["other-header"].ToString() == "Other header value");
            Assert.True(getResponseJson["headers"]["athirdheader"].ToString() == "A 3rd header");
        }
        
        
        [Test]
        public async Task TestSubmitFormAsync()
        {
            BrowserStandard browser = new BrowserStandard();
            browser.Navigate("https://www.browsesharp.org/testsitesforms.html");
            Form postForm = browser.Document.Forms[0];
            Form getForm = browser.Document.Forms[1];
            postForm.SetValue("UserName", "TestUser");
            postForm.SetValue("Password", "TestPassword");
            
            IDocument postResponse = await browser.SubmitFormAsync(postForm);
            dynamic postResponseJson = new JavaScriptSerializer().Deserialize<dynamic>(postResponse.Body.TextContent);
            Assert.True(postResponseJson["formData"]["UserName"] == "TestUser");
            Assert.True(postResponseJson["formData"]["Password"] == "TestPassword");

            getForm.SetValue("Message", "This is the test message");
            getForm.SetValue("Email", "testemail@test.com");
            getForm.SetValue("Rating", "3");
            IDocument getResponse = await browser.SubmitFormAsync(getForm);
            dynamic getResponseJson = new JavaScriptSerializer().Deserialize<dynamic>(getResponse.Body.TextContent);
            Assert.True(getResponseJson["query"]["Message"] == "This is the test message");
            Assert.True(getResponseJson["query"]["Email"] == "testemail@test.com");
            Assert.True(getResponseJson["query"]["Rating"] == "3");
        }
        
        [Test]
        public async Task TestSubmitFormAsyncHeaders()
        {
            BrowserStandard browser = new BrowserStandard();
            browser.Navigate("https://www.browsesharp.org/testsitesforms.html");
            Form postForm = browser.Document.Forms[0];
            Form getForm = browser.Document.Forms[1];
            postForm.SetValue("UserName", "TestUser");
            postForm.SetValue("Password", "TestPassword");
            
            Dictionary<string, string> postHeaders = new Dictionary<string, string>();
            postHeaders.Add("x-my-header","MyHeaderValue");
            postHeaders.Add("other-header","this is the other header");
            
            IDocument postResponse = await browser.SubmitFormAsync(postForm, postHeaders);
            dynamic postResponseJson = new JavaScriptSerializer().Deserialize<dynamic>(postResponse.Body.TextContent);
            Assert.True(postResponseJson["formData"]["UserName"] == "TestUser");
            Assert.True(postResponseJson["formData"]["Password"] == "TestPassword");


            Assert.True(postResponseJson["headers"]["x-my-header"] == "MyHeaderValue");
            Assert.True(postResponseJson["headers"]["other-header"] == "this is the other header");
            
            getForm.SetValue("Message", "This is the test message");
            getForm.SetValue("Email", "testemail@test.com");
            getForm.SetValue("Rating", "3");
            
            Dictionary<string, string> getHeaders = new Dictionary<string, string>();
            getHeaders.Add("get-header","This is a get header");
            getHeaders.Add("other-header","Other header value");
            getHeaders.Add("athirdheader", "A 3rd header");
            
            IDocument getResponse = await browser.SubmitFormAsync(getForm, getHeaders);
            dynamic getResponseJson = new JavaScriptSerializer().Deserialize<dynamic>(getResponse.Body.TextContent);
            Assert.True(getResponseJson["query"]["Message"] == "This is the test message");
            Assert.True(getResponseJson["query"]["Email"] == "testemail@test.com");
            Assert.True(getResponseJson["query"]["Rating"] == "3");
            
            Assert.True(getResponseJson["headers"]["get-header"] == "This is a get header");
            Assert.True(getResponseJson["headers"]["other-header"].ToString() == "Other header value");
            Assert.True(getResponseJson["headers"]["athirdheader"].ToString() == "A 3rd header");
        }

        /// <summary>
        /// Tests that the submit and navigate methods work with a uri that does not have a protocol attached
        /// </summary>
        [Test]
        public void TestNoProtoclNavigateSubmit()
        {
            BrowserStandard browser = new BrowserStandard();
            browser.Navigate("google.com");
            browser.Navigate("google.com", null);
            browser.Navigate("google.com", null, null);
            browser.Submit("google.com");
            browser.Submit("google.com", null);
            browser.Submit("google.com", null,null);
        }
        
        /// <summary>
        /// Tests that the submit and navigate methods work with a uri that does not have a protocol attached
        /// </summary>
        [Test]
        public async Task TestNoProtoclNavigateSubmitAsync()
        {
            BrowserStandard browser = new BrowserStandard();
            await browser.NavigateAsync("google.com");
            await browser.NavigateAsync("google.com", null);
            await browser.NavigateAsync("google.com", null, null);
            await browser.SubmitAsync("google.com");
            await browser.SubmitAsync("google.com", null);
            await browser.SubmitAsync("google.com", null,null);
        }
    }
}