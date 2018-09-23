using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BrowseSharp;
using BrowseSharp.Browsers;
using BrowseSharp.Browsers.Core;
using BrowseSharp.Html;
using BrowseSharpTest.Models;
using Microsoft.DocAsCode.Common;
using NUnit.Framework;
using RestSharp;

/* Several tests depend on the RequestTester nodejs project
 You can find this project at https://github.com/Jayx239/RequestTester
 Be sure to set the proper port number that your RequestTester is listening on
 */

namespace BrowseSharpTest
{
    [TestFixture]
    public class TypedCoreTest
    {
        /* RequestTester Configuration */
        public static int RequestTesterPort = 3000; // This is the port your RequestTester application is listening to
        public static string RequestTesterRouteUri = "https://requesttester.com/tester/view";//"http://localhost:" + RequestTesterPort + "/tester/view";
        public static string RequestTesterRouteJsonUri = "https://requesttester.com/tester";

        

        #region Typed Tests
        [Test]
        public void TestExecuteTyped()
        {
            TypedCore browser = new TypedCore();
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
            IDocument<RandomObject> random = new Document<RandomObject>(document);
        }

        [Test]
        public void TestExecuteAsGetTyped()
        {

            TypedCore browser = new TypedCore();
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
            IDocument<RandomObject> random = new Document<RandomObject>(document);
            
        }

        [Test]
        public void TestExecuteAsPostTyped()
        {
            TypedCore browser = new TypedCore();
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

        #endregion
    }
}