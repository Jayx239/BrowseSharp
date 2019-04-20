using BrowseSharp.BOM.Navigator;
using Jint;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BrowseSharp.BOM.Test
{
    [TestClass]
    public class NavigatorTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            Engine _jintEngine = new Engine();
            Navigator.Navigator navigator = new Navigator.Navigator(_jintEngine);
            navigator.InitializeEngine();
            navigator.appCodeName = "hellp";
            System.Console.WriteLine(navigator.appCodeName);
            var val = (INavigator) _jintEngine.GetValue("Navigator").ToObject();
            Assert.AreEqual("hellp",val.appCodeName);
            Assert.AreEqual("hellp", navigator.appCodeName);

        }

        [TestMethod]
        public void Test()
        {
            Engine jintEngine = new Engine();
            jintEngine.SetValue("context", new { time = "10:2:01" });
            var val = jintEngine.GetValue("context");
            Assert.AreEqual("10:2:01", val.AsObject().GetOwnProperty("time").Value);
        }
    }
}
