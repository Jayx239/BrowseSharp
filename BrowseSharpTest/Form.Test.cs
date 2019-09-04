using System.Collections.Generic;
using BrowseSharp;
using BrowseSharp.Common.Html;
using NUnit.Framework;

namespace BrowseSharpTest
{
    [TestFixture]
    public class FormTest
    {
        /* RequestTester Configuration */
        public static int RequestTesterPort = 3000; // This is the port your RequestTester application is listening to
        public static string RequestTesterRouteUri = Globals.RequestTesterRouteUri;//"http://localhost:" + RequestTesterPort + "/tester/view";
        public static string RequestTesterRouteJsonUri = Globals.RequestTesterRouteJsonUri;

        [Test]
        public void TestExecute()
        {
            Browser browser = new Browser();
            browser.Navigate("https://www.browsesharp.org/testsitesforms.html");
            List<Form> forms = browser.Document.Forms;
            var form = forms[0];
            
            form.SetValue("UserName", "TestUserName");
            form.SetValue("Password", "TestPassword");
            Assert.True(form.FormValues.ContainsKey("UserName"));
            Assert.True(form.FormValues["UserName"] == "TestUserName");
            Assert.True(form.FormValues.ContainsKey("Password"));
            Assert.True(form.FormValues["Password"] == "TestPassword");
        }

    }
}