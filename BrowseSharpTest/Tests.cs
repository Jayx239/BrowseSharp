using System;
using System.Net;
using System.Threading;
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
            Browser browser = new BrowseSharp.Browser();
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
        public async Task TestExecuteAsGet()
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
        public async Task TestExecuteAsPost()
        {
            Assert.True(false);
        }
        
        [Test]
        public async Task TestExecuteTaskAsyncToken(){}
        
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
        public async Task TestExecuteGetTaskAsyncToken(){}

        [Test]
        public async Task TestExecutePostTaskAsync()
        {
            Assert.True(false);
        }

        [Test]
        public async Task TestExecutePostTaskAsyncToken()
        {
            Assert.True(false);
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

            Uri testuri2 = new Uri("https://google.com/something/else");
            result1 = UriHelper.GetUri(testUri, "/js/sass/");
            Assert.AreEqual(result1.AbsoluteUri, "https://google.com/js/sass/");
            result2 = UriHelper.GetUri(testUri, "something/different");
            Assert.AreEqual(result2.AbsoluteUri, "https://google.com/something/else/something/different");
            result3 = UriHelper.GetUri(testUri, "https://amazon.com/something/different");
            Assert.AreEqual(result3.AbsoluteUri, "https://amazon.com/something/different");
            result4 = UriHelper.GetUri(testUri, "www.amazon.com/something/different");
            Assert.AreEqual(result4.AbsoluteUri, "http://www.amazon.com/something/different");
        }
    }
}