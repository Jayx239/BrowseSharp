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

namespace BrowseSharpTest
{
    [TestFixture]
    public class RaceConditionProof
    {
        [TestCase]
        public async Task BrowserFail()
        {

            try
            {
                BrowserThreadFail browser = new BrowserThreadFail();
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
            catch (Exception ex)
            {
                return;
            }
            Assert.IsTrue(false);
            
        }
        
        [TestCase]
        public async Task BrowserThreadSafePass()
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
        
    }
}