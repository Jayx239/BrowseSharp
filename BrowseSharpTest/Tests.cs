using System;
using NUnit.Framework;
using BrowseSharp;
using RestSharp;

namespace BrowseSharpTest
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void Test1()
        {
            BrowseSharp.Browser browser = new BrowseSharp.Browser();
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
            
            Assert.True(true);
        }
    }
}