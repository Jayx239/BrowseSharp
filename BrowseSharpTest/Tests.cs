using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NUnit.Framework;
using BrowseSharp;
using BrowseSharp.Toolbox;
using RestSharp;

namespace BrowseSharpTest
{
    [TestFixture]
    public class Tests
    {
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
            throw new NotImplementedException();
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
            throw new NotImplementedException();
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
            throw new NotImplementedException();
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