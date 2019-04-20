using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using BrowseSharp;
using BrowseSharp.Browsers;
using BrowseSharp.Types;
using BrowseSharp.Types.Html;
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
    public class BrowserThreadSafeTest
    {
        /* RequestTester Configuration */
        public static int RequestTesterPort = 3000; // This is the port your RequestTester application is listening to
        public static string RequestTesterRouteUri = "https://requesttester.com/tester/view";//"http://localhost:" + RequestTesterPort + "/tester/view";
        public static string RequestTesterRouteJsonUri = "https://requesttester.com/tester";
        #region BrowserStandard Tests
        [Test]
        public void TestExecute()
        {
            BrowserThreadSafe browser = new BrowserThreadSafe();
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
            BrowserThreadSafe browser = new BrowserThreadSafe();
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
            BrowserThreadSafe browser = new BrowserThreadSafe();
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
            BrowserThreadSafe browser = new BrowserThreadSafe();
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
            BrowserThreadSafe browser = new BrowserThreadSafe();
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
            BrowserThreadSafe browser = new BrowserThreadSafe();
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
            BrowserThreadSafe browser = new BrowserThreadSafe();
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
            BrowserThreadSafe browser = new BrowserThreadSafe();
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
            BrowserThreadSafe browser = new BrowserThreadSafe();
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
            BrowserThreadSafe browser = new BrowserThreadSafe();
            
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
            BrowserThreadSafe browser = new BrowserThreadSafe();

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
            BrowserThreadSafe browser = new BrowserThreadSafe();
            
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
            BrowserThreadSafe browser = new BrowserThreadSafe();
            
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
            BrowserThreadSafe browser = new BrowserThreadSafe();

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
            BrowserThreadSafe browser = new BrowserThreadSafe();
            
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
            BrowserThreadSafe browser = new BrowserThreadSafe();

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
            BrowserThreadSafe browser = new BrowserThreadSafe();

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
            BrowserThreadSafe browser = new BrowserThreadSafe();

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
            BrowserThreadSafe browser = new BrowserThreadSafe();

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

        [Test]
        public void TestClearHistory()
        {
            BrowserThreadSafe browser = new BrowserThreadSafe();
            browser.Navigate("http://google.com");
            Assert.True(browser.History.Count == 1);
            browser.ClearHistory();
            Assert.True(browser.History.Count == 0);
        }

        [Test]
        public void TestClearForwardHistory()
        {
            BrowserThreadSafe browser = new BrowserThreadSafe();
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
            BrowserThreadSafe browser = new BrowserThreadSafe();
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
            BrowserThreadSafe browser = new BrowserThreadSafe();
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
            BrowserThreadSafe browser = new BrowserThreadSafe();
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
            BrowserThreadSafe browser = new BrowserThreadSafe();
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
            BrowserThreadSafe browser = new BrowserThreadSafe();
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
            BrowserThreadSafe browser = new BrowserThreadSafe();
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
            BrowserThreadSafe browser = new BrowserThreadSafe();
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
            BrowserThreadSafe browser = new BrowserThreadSafe();
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
            BrowserThreadSafe browser = new BrowserThreadSafe();
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
            BrowserThreadSafe browser = new BrowserThreadSafe();
        
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
            BrowserThreadSafe browser = new BrowserThreadSafe();
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
            BrowserThreadSafe browser = new BrowserThreadSafe();
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

        [Test]
        public void TestJavascriptScrapingEnabled()
        {
            BrowserThreadSafe browser = new BrowserThreadSafe();
            browser.JavascriptScrapingEnabled = true;
            IDocument document = browser.Navigate("https://jayx239.github.io/BrowseSharpTest/");
            Assert.True(document.Scripts.Count > 0);
        }
        
        [Test]
        public void TestJavascriptScrapingDisabled()
        {
            BrowserThreadSafe browser = new BrowserThreadSafe();
            browser.JavascriptScrapingEnabled = false;
            IDocument document = browser.Navigate("https://jayx239.github.io/BrowseSharpTest/");
            Assert.True(document.Scripts.Count == 0);
        }
        
        [Test]
        public async Task TestJavascriptScrapingEnabledAsync()
        {
            BrowserThreadSafe browser = new BrowserThreadSafe();
            browser.JavascriptScrapingEnabled = true;
            IDocument document = await browser.NavigateAsync("https://jayx239.github.io/BrowseSharpTest/");
            Assert.True(document.Scripts.Count > 0);
        }
        
        [Test]
        public async Task TestJavascriptScrapingDisabledAsync()
        {
            BrowserThreadSafe browser = new BrowserThreadSafe();
            browser.JavascriptScrapingEnabled = false;
            IDocument document = await browser.NavigateAsync("https://jayx239.github.io/BrowseSharpTest/");
            Assert.True(document.Scripts.Count == 0);
        }
        
        [Test]
        public void TestStyleScrapingEnabled()
        {
            BrowserThreadSafe browser = new BrowserThreadSafe();
            browser.StyleScrapingEnabled = true;
            IDocument document = browser.Navigate("https://jayx239.github.io/BrowseSharpTest/");
            Assert.True(document.Styles.Count > 0);
        }
        
        [Test]
        public void TestStyleScrapingDisabled()
        {
            BrowserThreadSafe browser = new BrowserThreadSafe();
            browser.StyleScrapingEnabled = false;
            IDocument document = browser.Navigate("https://jayx239.github.io/BrowseSharpTest/");
            Assert.True(document.Styles.Count == 0);
        }
        
        [Test]
        public async Task TestStyleScrapingEnabledAsync()
        {
            BrowserThreadSafe browser = new BrowserThreadSafe();
            browser.StyleScrapingEnabled = true;
            IDocument document = await browser.NavigateAsync("https://jayx239.github.io/BrowseSharpTest/");
            Assert.True(document.Styles.Count > 0);
        }
        
        [Test]
        public async Task TestStyleScrapingDisabledAsync()
        {
            BrowserThreadSafe browser = new BrowserThreadSafe();
            browser.StyleScrapingEnabled = false;
            IDocument document = await browser.NavigateAsync("https://jayx239.github.io/BrowseSharpTest/");
            Assert.True(document.Styles.Count == 0);
        }

        [Test]
        public void TestSubmitForm()
        {
            BrowserThreadSafe browser = new BrowserThreadSafe();
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
            BrowserThreadSafe browser = new BrowserThreadSafe();
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
            BrowserThreadSafe browser = new BrowserThreadSafe();
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
            BrowserThreadSafe browser = new BrowserThreadSafe();
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
            Assert.True(postResponseJson["headers"]["other-header"].ToString() == "this is the other header");
            
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
            BrowserThreadSafe browser = new BrowserThreadSafe();
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
            BrowserThreadSafe browser = new BrowserThreadSafe();
            await browser.NavigateAsync("google.com");
            await browser.NavigateAsync("google.com", null);
            await browser.NavigateAsync("google.com", null, null);
            await browser.SubmitAsync("google.com");
            await browser.SubmitAsync("google.com", null);
            await browser.SubmitAsync("google.com", null,null);
        }
        #endregion

        #region Typed Tests
        [Test]
        public void TestExecuteTyped()
        {
            BrowserThreadSafe browser = new BrowserThreadSafe();
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
        public void TestExecuteAsGetTyped()
        {

            BrowserThreadSafe browser = new BrowserThreadSafe();
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
        public void TestExecuteAsPostTyped()
        {
            BrowserThreadSafe browser = new BrowserThreadSafe();
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
        public async Task TestExecuteGetTaskAsyncTyped()
        {

            BrowserThreadSafe browser = new BrowserThreadSafe();
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
        public async Task TestExecuteGetTaskAsyncTokenTyped()
        {

            BrowserThreadSafe browser = new BrowserThreadSafe();
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
        public async Task TestExecutePostTaskAsyncTyped()
        {

            BrowserThreadSafe browser = new BrowserThreadSafe();
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
        public async Task TestExecutePostTaskAsyncTokenTyped()
        {

            BrowserThreadSafe browser = new BrowserThreadSafe();
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
        public async Task TestExecuteTaskAsyncTyped()
        {

            BrowserThreadSafe browser = new BrowserThreadSafe();
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
        public async Task TestExecuteTaskAsyncTokenTyped()
        {

            BrowserThreadSafe browser = new BrowserThreadSafe();
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
        public void TestNavigateTyped()
        {
            BrowserThreadSafe browser = new BrowserThreadSafe();
            var response = browser.Navigate<Request>(RequestTesterRouteJsonUri);
            Assert.IsTrue(response.Data != null);
            Assert.IsTrue(response.Data is Request);
        }
        
        [Test]
        public void TestNavigateHeadersTyped()
        {
            BrowserThreadSafe browser = new BrowserThreadSafe();

            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("x-csrf-token", "axsd82os21");
            
            IDocument<Request> response = browser.Navigate<Request>(RequestTesterRouteJsonUri, headers);

            Request request = response.Data;
            Assert.IsTrue(request.Headers.ContainsKey("x-csrf-token"));
            Assert.IsTrue(request.Headers["x-csrf-token"] == "axsd82os21");
        }

        [Test]
        public void TestNavigateHeadersAndDataTyped()
        {
            BrowserThreadSafe browser = new BrowserThreadSafe();
            
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
        public async Task TestNavigateAsyncTyped()
        {
            BrowserThreadSafe browser = new BrowserThreadSafe();
            var response = await browser.NavigateAsync<Request>(RequestTesterRouteJsonUri);
            Assert.IsTrue(response.Data != null);
            Assert.IsTrue(response.Data is Request);
        }
        
        [Test]
        public async Task TestNavigateHeadersAsyncTyped()
        {
            BrowserThreadSafe browser = new BrowserThreadSafe();

            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("x-csrf-token", "axsd82os21");
            
            IDocument<Request> response = await browser.NavigateAsync<Request>(RequestTesterRouteJsonUri, headers);

            Request request = response.Data;
            Assert.IsTrue(request.Headers.ContainsKey("x-csrf-token"));
            Assert.IsTrue(request.Headers["x-csrf-token"] == "axsd82os21");
        }

        [Test]
        public async Task TestNavigateHeadersAndDataAsyncTyped()
        {
            BrowserThreadSafe browser = new BrowserThreadSafe();
            
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
        public void TestSubmitNothingTyped()
        {
            BrowserThreadSafe browser = new BrowserThreadSafe();
            
            IDocument<Request> response = browser.Submit<Request>(RequestTesterRouteJsonUri);
            Request request = response.Data;
            
            Assert.IsTrue(request.Headers.Count > 0);
            
        }
        
        [Test]
        public void TestSubmitTyped()
        {
            BrowserThreadSafe browser = new BrowserThreadSafe();

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
        public void TestSubmitHeadersTyped()
        {
            BrowserThreadSafe browser = new BrowserThreadSafe();

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
        public async Task TestSubmitAsyncNothingTyped()
        {
            BrowserThreadSafe browser = new BrowserThreadSafe();
            
            IDocument<Request> response = await browser.SubmitAsync<Request>(RequestTesterRouteJsonUri);
            Request request = response.Data;
            
            Assert.IsTrue(request.Headers.Count > 0);
            
        }
        
        [Test]
        public async Task TestSubmitAsyncTyped()
        {
            BrowserThreadSafe browser = new BrowserThreadSafe();

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
        public async Task TestSubmitAsyncHeadersTyped()
        {
            BrowserThreadSafe browser = new BrowserThreadSafe();

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
        
        #endregion

        #region Mixed Base and Typed

        [Test]
        public void TestSharedHistory()
        {
            BrowserThreadSafe browser = new BrowserThreadSafe();

            IDocument document1 = browser.Navigate("github.com");
            Assert.IsTrue(browser.History.Count == 1);
            
            IDocument document2 = browser.Navigate<dynamic>("nuget.com");
            Assert.IsTrue(browser.History.Count == 2);

            IDocument documentBack = browser.Back();
            Assert.IsTrue(browser.History.Count == 1);
            Assert.IsTrue(browser.History.Count == 1);
            Assert.IsTrue(documentBack.RequestUri == document1.RequestUri);
            Assert.IsTrue(browser.ForwardHistory[0].RequestUri == document2.RequestUri);
        }

        [Test]
        public void TestSharedJavascriptScrapingEnabledFlag()
        {
            BrowserThreadSafe browser = new BrowserThreadSafe();
            
            IDocument document1 = browser.Navigate("browsesharp.org");
            Assert.IsTrue(document1.Scripts.Count > 0);
            
            IDocument<dynamic> document2 = browser.Navigate<dynamic>("browsesharp.org");
            Assert.IsTrue(document2.Scripts.Count > 0);

            browser.JavascriptScrapingEnabled = false;

            IDocument document3 = browser.Navigate("browsesharp.org");
            Assert.IsTrue(document3.Scripts.Count == 0);
            
            IDocument<dynamic> document4 = browser.Navigate<dynamic>("browsesharp.org");
            Assert.IsTrue(document4.Scripts.Count == 0);


        }

        [Test]
        public void TestSharedSyleScrapingEnabledFlag()
        {
            BrowserThreadSafe browser = new BrowserThreadSafe();
            
            IDocument document1 = browser.Navigate("browsesharp.org");
            Assert.IsTrue(document1.Styles.Count > 0);
            
            IDocument<dynamic> document2 = browser.Navigate<dynamic>("browsesharp.org");
            Assert.IsTrue(document2.Styles.Count > 0);

            browser.StyleScrapingEnabled = false;

            IDocument document3 = browser.Navigate("browsesharp.org");
            Assert.IsTrue(document3.Styles.Count == 0);
            
            IDocument<dynamic> document4 = browser.Navigate<dynamic>("browsesharp.org");
            Assert.IsTrue(document4.Styles.Count == 0);
        }

        [Test]
        public void TestDocumentToTypedDocument()
        {
            BrowserThreadSafe browser = new BrowserThreadSafe();
            
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

        [Test]
        public async Task TestMultiAsyncNavigate()
        {
            BrowserThreadSafe browser = new BrowserThreadSafe();
            List<Task<IDocument>> navigateTasks = new List<Task<IDocument>>();
            navigateTasks.Add(browser.NavigateAsync("facebook.com"));
            navigateTasks.Add(browser.NavigateAsync("requesttester.com"));
            navigateTasks.Add(browser.NavigateAsync("nuget.org"));
            navigateTasks.Add(browser.NavigateAsync("browsesharp.org"));
            IDocument browseSharp = await navigateTasks[3];
            IDocument nuget = await navigateTasks[2];
            IDocument requestTester = await navigateTasks[1];
            IDocument facebook = await navigateTasks[0];
            Assert.AreEqual("www.facebook.com", facebook.Response.ResponseUri.Host);
            Assert.IsTrue(facebook.Response.Content.ToLower().Contains("facebook"));
            
            Assert.AreEqual( "requesttester.com", requestTester.Response.ResponseUri.Host);
            Assert.IsTrue(requestTester.Response.Content.ToLower().Contains("express"));
            Assert.AreEqual("www.nuget.org", nuget.Response.ResponseUri.Host);
            Assert.IsTrue(nuget.Response.Content.ToLower().Contains("nuget"));
            Assert.AreEqual("browsesharp.org", browseSharp.Response.ResponseUri.Host);
            Assert.IsTrue(browseSharp.Response.Content.ToLower().Contains("browsesharp"));
        }

        #endregion

        
        
    }
}