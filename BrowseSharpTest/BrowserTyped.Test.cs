using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BrowseSharp;
using BrowseSharp.Browsers.Core;
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
    public class TypedCoreTest
    {
        /* RequestTester Configuration */
        public static int RequestTesterPort = 3000; // This is the port your RequestTester application is listening to
        public static string RequestTesterRouteUri = "https://requesttester.com/tester/view";//"http://localhost:" + RequestTesterPort + "/tester/view";
        public static string RequestTesterRouteJsonUri = "https://requesttester.com/tester";

        

        #region Typed Tests
        [Test]
        public void TestExecute()
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
        public void TestExecuteAsGet()
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
        public void TestExecuteAsPost()
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

        [Test]
        public async Task TestExecuteGetTaskAsync()
        {

            TypedCore browser = new TypedCore();
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
            IDocument<RandomObject> random = new Document<RandomObject>(document);
        }
        
        [Test]
        public async Task TestExecuteGetTaskAsyncToken()
        {

            TypedCore browser = new TypedCore();
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
            IDocument<RandomObject> random = new Document<RandomObject>(document);
        }
        
        [Test]
        public async Task TestExecutePostTaskAsync()
        {

            TypedCore browser = new TypedCore();
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

            TypedCore browser = new TypedCore();
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

            TypedCore browser = new TypedCore();
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

            TypedCore browser = new TypedCore();
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
            TypedCore browser = new TypedCore();
            var response = browser.Navigate<Request>(RequestTesterRouteJsonUri);
            Assert.IsTrue(response.Data != null);
            Assert.IsTrue(response.Data is Request);
        }
        
        [Test]
        public void TestNavigateHeaders()
        {
            TypedCore browser = new TypedCore();

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
            TypedCore browser = new TypedCore();
            
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
            TypedCore browser = new TypedCore();
            var response = await browser.NavigateAsync<Request>(RequestTesterRouteJsonUri);
            Assert.IsTrue(response.Data != null);
            Assert.IsTrue(response.Data is Request);
        }
        
        [Test]
        public async Task TestNavigateHeadersAsync()
        {
            TypedCore browser = new TypedCore();

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
            TypedCore browser = new TypedCore();
            
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
            TypedCore browser = new TypedCore();
            
            IDocument<Request> response = browser.Submit<Request>(RequestTesterRouteJsonUri);
            Request request = response.Data;
            
            Assert.IsTrue(request.Headers.Count > 0);
            
        }
        
        [Test]
        public void TestSubmit()
        {
            TypedCore browser = new TypedCore();

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
            TypedCore browser = new TypedCore();

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
            TypedCore browser = new TypedCore();
            
            IDocument<Request> response = await browser.SubmitAsync<Request>(RequestTesterRouteJsonUri);
            Request request = response.Data;
            
            Assert.IsTrue(request.Headers.Count > 0);
            
        }
        
        [Test]
        public async Task TestSubmitAsync()
        {
            TypedCore browser = new TypedCore();

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
            TypedCore browser = new TypedCore();

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
    }
}