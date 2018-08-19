using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;
using BrowseSharp;
using BrowseSharp.Toolbox;
using RestSharp;

/* Several tests depend on the RequestTester nodejs project
 You can find this project at https://github.com/Jayx239/RequestTester
 Be sure to set the proper port number that your RequestTester is listening on
 */

namespace BrowseSharpTest
{
    [TestFixture]
    public class Tests
    {
        /* RequestTester Configuration */
        public static int RequestTesterPort = 3000; // This is the port your RequestTester application is listening to
        public static string RequestTesterRouteUri = "http://localhost:" + RequestTesterPort + "/tester/view";
        
        [Test]
        public void TestExecute()
        {
            Browser browser = new Browser();
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
            Browser browser = new BrowseSharp.Browser();
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
            Browser browser = new Browser();
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
            Browser browser = new BrowseSharp.Browser();
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
            Browser browser = new BrowseSharp.Browser();
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
            Browser browser = new BrowseSharp.Browser();
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
            Browser browser = new BrowseSharp.Browser();
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
            Browser browser = new Browser();
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
            Browser browser = new Browser();
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
            Browser browser = new BrowseSharp.Browser();
            
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
            Browser browser = new Browser();

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
            Browser browser = new Browser();
            
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
            Browser browser = new BrowseSharp.Browser();
            
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
            Browser browser = new Browser();

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
            Browser browser = new Browser();
            
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
            Browser browser = new Browser();

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
            Browser browser = new Browser();

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
            Browser browser = new Browser();

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
            Browser browser = new Browser();

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
            Browser browser = new Browser();
            browser.Navigate("http://google.com");
            Assert.True(browser.History.Count == 1);
            browser.ClearHistory();
            Assert.True(browser.History.Count == 0);
        }

        [Test]
        public void TestClearForwardHistory()
        {
            Browser browser = new Browser();
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
            Browser browser = new Browser();
            IDocument firstResponseDocument = browser.Navigate("http://google.com");
            Assert.True(firstResponseDocument == browser.Document());
            Assert.True(browser.History.Count == 1);
            browser.Navigate("https://facebook.com");
            Assert.True(browser.History.Count == 2);
            browser.Back();
            Assert.True(browser.History.Count == 1);
            Assert.True(browser.ForwardHistory.Count == 1);
            Assert.True(browser.Document() != firstResponseDocument);

        }
        
        [Test]
        public void TestBackCache()
        {
            Browser browser = new Browser();
            IDocument firstResponseDocument = browser.Navigate("http://google.com");
            Assert.True(firstResponseDocument == browser.Document());
            Assert.True(browser.History.Count == 1);
            browser.Navigate("https://facebook.com");
            Assert.True(browser.History.Count == 2);
            browser.Back(true);
            Assert.True(browser.History.Count == 1);
            Assert.True(browser.ForwardHistory.Count == 1);
            Assert.True(browser.Document() == firstResponseDocument);

        }

        [Test]
        public async Task TestBackAsync()
        {
            Browser browser = new Browser();
            IDocument firstResponseDocument = browser.Navigate("http://google.com");
            Assert.True(firstResponseDocument == browser.Document());
            Assert.True(browser.History.Count == 1);
            browser.Navigate("https://facebook.com");
            Assert.True(browser.History.Count == 2);
            await browser.BackAsync();
            Assert.True(browser.History.Count == 1);
            Assert.True(browser.ForwardHistory.Count == 1);
            Assert.True(browser.Document() != firstResponseDocument);

        }
        
        [Test]
        public async Task TestBackAsyncCache()
        {
            Browser browser = new Browser();
            IDocument firstResponseDocument = browser.Navigate("http://google.com");
            Assert.True(firstResponseDocument == browser.Document());
            Assert.True(browser.History.Count == 1);
            browser.Navigate("https://facebook.com");
            Assert.True(browser.History.Count == 2);
            await browser.BackAsync(true);
            Assert.True(browser.History.Count == 1);
            Assert.True(browser.ForwardHistory.Count == 1);
            Assert.True(browser.Document() == firstResponseDocument);

        }

        [Test]
        public void TestForward()
        {
            Browser browser = new Browser();
            IDocument firstResponseDocument = browser.Navigate("http://google.com");
            Assert.True(firstResponseDocument == browser.Document());
            Assert.True(browser.History.Count == 1);
            IDocument secondDocument = browser.Navigate("https://facebook.com");
            Assert.True(browser.History.Count == 2);
            browser.Back();
            Assert.True(browser.History.Count == 1);
            Assert.True(browser.ForwardHistory.Count == 1);
            Assert.True(browser.Document() != firstResponseDocument);

            browser.Forward();
            Assert.True(browser.ForwardHistory.Count == 0);
            Assert.True(browser.History.Count == 2);
            Assert.True(secondDocument.Response.ResponseUri == browser.Document().Response.ResponseUri);
        }
        
        [Test]
        public void TestForwardCache()
        {
            Browser browser = new Browser();
            IDocument firstResponseDocument = browser.Navigate("http://google.com");
            Assert.True(firstResponseDocument == browser.Document());
            Assert.True(browser.History.Count == 1);
            IDocument secondDocument = browser.Navigate("https://facebook.com");
            Assert.True(browser.History.Count == 2);
            browser.Back();
            Assert.True(browser.History.Count == 1);
            Assert.True(browser.ForwardHistory.Count == 1);
            Assert.True(browser.Document() != firstResponseDocument);

            browser.Forward(true);
            Assert.True(browser.ForwardHistory.Count == 0);
            Assert.True(browser.History.Count == 2);
            Assert.True(secondDocument == browser.Document());
        }
        
        [Test]
        public async Task TestForwardAsync()
        {
            Browser browser = new Browser();
            IDocument firstResponseDocument = await browser.NavigateAsync("http://google.com");
            Assert.True(firstResponseDocument == browser.Document());
            Assert.True(browser.History.Count == 1);
            IDocument secondDocument = await browser.NavigateAsync("https://facebook.com");
            Assert.True(browser.History.Count == 2);
            await browser.BackAsync();
            Assert.True(browser.History.Count == 1);
            Assert.True(browser.ForwardHistory.Count == 1);
            Assert.True(browser.Document() != firstResponseDocument);

            await browser.ForwardAsync(true);
            Assert.True(browser.ForwardHistory.Count == 0);
            Assert.True(browser.History.Count == 2);
            Assert.True(secondDocument == browser.Document());
        }

        [Test]
        public async Task TestForwardAsyncCache()
        {
            Browser browser = new Browser();
            IDocument firstResponseDocument = await browser.NavigateAsync("http://google.com");
            Assert.True(firstResponseDocument == browser.Document());
            Assert.True(browser.History.Count == 1);
            IDocument secondDocument = await browser.NavigateAsync("https://facebook.com");
            Assert.True(browser.History.Count == 2);
            await browser.BackAsync();
            Assert.True(browser.History.Count == 1);
            Assert.True(browser.ForwardHistory.Count == 1);
            Assert.True(browser.Document() != firstResponseDocument);

            await browser.ForwardAsync();
            Assert.True(browser.ForwardHistory.Count == 0);
            Assert.True(browser.History.Count == 2);
            Assert.True(secondDocument.Response.ResponseUri == browser.Document().Response.ResponseUri);
        }

        [Test]
        public async Task TestMaxHistorySize()
        {
            Browser browser = new Browser();
            browser.MaxHistorySize = 2;
            IDocument document1 = await browser.NavigateAsync("https://github.com");
            Assert.True(document1 == browser.Document());
            IDocument document2 = await browser.NavigateAsync(RequestTesterRouteUri);
            Assert.True(document2 == browser.Document());
            IDocument document3 = await browser.NavigateAsync("https://nuget.org");
            Assert.True(document3 == browser.Document());
            Assert.True(browser.History.Count == 2);
            Assert.True(browser.Documents[0] == document2);
        }
        
        [Test]
        public async Task TestMaxHistorySizeUnlimited()
        {
            Browser browser = new Browser();
        
            IDocument document1 = await browser.NavigateAsync("https://github.com");
            Assert.True(document1 == browser.Document());
            IDocument document2 = await browser.NavigateAsync(RequestTesterRouteUri);
            Assert.True(document2 == browser.Document());
            IDocument document3 = await browser.NavigateAsync("https://nuget.org");
            Assert.True(document3 == browser.Document());
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
            Browser browser = new Browser();
            IDocument document = browser.Navigate("https://github.com");
            IDocument documentRefreshed = browser.Refresh();
            Assert.True(documentRefreshed == browser.Document());
            Assert.True(documentRefreshed != document);
        }

        [Test]
        public async Task TestRefreshAsync()
        {
            Browser browser = new Browser();
            IDocument document = await browser.NavigateAsync("https://github.com");
            IDocument documentRefreshed = await browser.RefreshAsync();
            Assert.True(documentRefreshed == browser.Document());
            Assert.True(documentRefreshed != document);
        }

        [Test]
        public void TestJavascriptScrapingEnabled()
        {
            Browser browser = new Browser();
            browser.JavascriptScrapingEnabled = true;
            IDocument document = browser.Navigate("https://jayx239.github.io/BrowseSharpTest/");
            Assert.True(document.Scripts.Count > 0);
        }
        
        [Test]
        public void TestJavascriptScrapingDisabled()
        {
            Browser browser = new Browser();
            browser.JavascriptScrapingEnabled = false;
            IDocument document = browser.Navigate("https://jayx239.github.io/BrowseSharpTest/");
            Assert.True(document.Scripts.Count == 0);
        }
        
        [Test]
        public async Task TestJavascriptScrapingEnabledAsync()
        {
            Browser browser = new Browser();
            browser.JavascriptScrapingEnabled = true;
            IDocument document = await browser.NavigateAsync("https://jayx239.github.io/BrowseSharpTest/");
            Assert.True(document.Scripts.Count > 0);
        }
        
        [Test]
        public async Task TestJavascriptScrapingDisabledAsync()
        {
            Browser browser = new Browser();
            browser.JavascriptScrapingEnabled = false;
            IDocument document = await browser.NavigateAsync("https://jayx239.github.io/BrowseSharpTest/");
            Assert.True(document.Scripts.Count == 0);
        }
        
        [Test]
        public void TestStyleScrapingEnabled()
        {
            Browser browser = new Browser();
            browser.StyleScrapingEnabled = true;
            IDocument document = browser.Navigate("https://jayx239.github.io/BrowseSharpTest/");
            Assert.True(document.Styles.Count > 0);
        }
        
        [Test]
        public void TestStyleScrapingDisabled()
        {
            Browser browser = new Browser();
            browser.StyleScrapingEnabled = false;
            IDocument document = browser.Navigate("https://jayx239.github.io/BrowseSharpTest/");
            Assert.True(document.Styles.Count == 0);
        }
        
        [Test]
        public async Task TestStyleScrapingEnabledAsync()
        {
            Browser browser = new Browser();
            browser.StyleScrapingEnabled = true;
            IDocument document = await browser.NavigateAsync("https://jayx239.github.io/BrowseSharpTest/");
            Assert.True(document.Styles.Count > 0);
        }
        
        [Test]
        public async Task TestStyleScrapingDisabledAsync()
        {
            Browser browser = new Browser();
            browser.StyleScrapingEnabled = false;
            IDocument document = await browser.NavigateAsync("https://jayx239.github.io/BrowseSharpTest/");
            Assert.True(document.Styles.Count == 0);
        }
        
        [Test]
        public void TestUriHelper()
        {
            Uri testUri = new Uri("https://google.com/something/else/");
            Uri result1 = UriHelper.GetUri(testUri, "/js/sass/");
            Assert.AreEqual(result1.AbsoluteUri, "https://google.com/js/sass/");
            Uri result2 = UriHelper.GetUri(testUri, "something/different");
            Assert.AreEqual(result2.AbsoluteUri, "https://google.com/something/else/something/different");
            Uri result3 = UriHelper.GetUri(testUri, "https://amazon.com/something/different");
            Assert.AreEqual(result3.AbsoluteUri, "https://amazon.com/something/different");
            Uri result4 = UriHelper.GetUri(testUri, "www.amazon.com/something/different");
            Assert.AreEqual(result4.AbsoluteUri, "http://www.amazon.com/something/different");

            Uri testUri2 = new Uri("https://google.com/something/else");
            result1 = UriHelper.GetUri(testUri2, "/js/sass/");
            Assert.AreEqual(result1.AbsoluteUri, "https://google.com/js/sass/");
            result2 = UriHelper.GetUri(testUri2, "something/different");
            Assert.AreEqual(result2.AbsoluteUri, "https://google.com/something/else/something/different");
            result3 = UriHelper.GetUri(testUri2, "https://amazon.com/something/different");
            Assert.AreEqual(result3.AbsoluteUri, "https://amazon.com/something/different");
            result4 = UriHelper.GetUri(testUri2, "www.amazon.com/something/different");
            Assert.AreEqual(result4.AbsoluteUri, "http://www.amazon.com/something/different");
        }
    }
}