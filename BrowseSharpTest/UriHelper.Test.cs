using System;
using BrowseSharp.Common.Toolbox;
using NUnit.Framework;

/* Several tests depend on the RequestTester nodejs project
 You can find this project at https://github.com/Jayx239/RequestTester
 Be sure to set the proper port number that your RequestTester is listening on
 */

namespace BrowseSharpTest
{
    [TestFixture]
    public class UriHelperTest
    {

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
            Assert.AreEqual(result2.AbsoluteUri, "https://google.com/something/something/different");
            result3 = UriHelper.GetUri(testUri2, "https://amazon.com/something/different");
            Assert.AreEqual(result3.AbsoluteUri, "https://amazon.com/something/different");
            result4 = UriHelper.GetUri(testUri2, "www.amazon.com/something/different");
            Assert.AreEqual(result4.AbsoluteUri, "http://www.amazon.com/something/different");
        }

        [Test]
        public void TestRelativeUriBug()
        {
            Uri testUri = new Uri("https://www.browsesharp.org/testsitesforms.html");
            Uri testUriResult = UriHelper.GetUri(testUri, "js/jquery-3.3.1.min.js");
            Uri ExpectedResult = new Uri("https://www.browsesharp.org/js/jquery-3.3.1.min.js");
            Assert.AreEqual(testUriResult, ExpectedResult);
        }

        [Test]
        public void TestRelativeUriBug2()
        {
            Uri testUri = new Uri("https://www.browsesharp.org/path/to/view/testsitesforms.html");
            Uri testUriResult = UriHelper.GetUri(testUri, "js/jquery-3.3.1.min.js");
            Uri ExpectedResult = new Uri("https://www.browsesharp.org/path/to/view/js/jquery-3.3.1.min.js");
            Assert.AreEqual(testUriResult, ExpectedResult);

            Uri testUriResult2 = UriHelper.GetUri(testUri, "/js/jquery-3.3.1.min.js");
            Uri ExpectedResult2 = new Uri("https://www.browsesharp.org/js/jquery-3.3.1.min.js");
            Assert.AreEqual(testUriResult2, ExpectedResult2);
        }

        [Test]
        public void TestUri()
        {
            Uri googleUri = UriHelper.Uri("www.google.com");
            Assert.IsTrue(googleUri != null);
            Assert.AreEqual(googleUri, new Uri("http://www.google.com"));

            Uri facebookUri = UriHelper.Uri("www.facebook.com", "https");
            Assert.IsTrue(facebookUri != null);
            Assert.AreEqual(facebookUri, new Uri("https://www.facebook.com"));

            Uri ebayUri = UriHelper.Uri("https://www.ebay.com");
            Assert.IsTrue(ebayUri != null);
            Assert.AreEqual(ebayUri, new Uri("https://www.ebay.com"));

            UriHelper.DefaultUriProtocol = "https";

            Uri browsesharpUri = UriHelper.Uri("www.browsesharp.com");
            Assert.IsTrue(browsesharpUri != null);
            Assert.AreEqual(browsesharpUri, new Uri("https://www.browsesharp.com"));
        }

        /* Bug fix release 0.0.8-beta */
        [Test]
        public void TestRelativeUri()
        {
            Uri uri = UriHelper.GetUri(new Uri("https://jayx239.github.io/BrowseSharpTest/"), "js/vendor/modernizr-3.6.0.min.js");
            Assert.AreEqual(uri.AbsoluteUri, "https://jayx239.github.io/BrowseSharpTest/js/vendor/modernizr-3.6.0.min.js");
        }

        [Test]
        public void TestParentDirectoyRelativeUri()
        {
            Uri uri = UriHelper.GetUri(new Uri("https://jayx239.github.io/BrowseSharpTest/"), "../js/vendor/modernizr-3.6.0.min.js");
            Assert.AreEqual(uri.AbsoluteUri, "https://jayx239.github.io/js/vendor/modernizr-3.6.0.min.js");
        }
    }
}