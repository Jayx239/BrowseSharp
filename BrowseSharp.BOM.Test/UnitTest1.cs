using Jint;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BrowseSharp.BOM.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            BOM.Window.Window window = new Window.Window();
        }

        [TestMethod]
        public void Test()
        {
            Engine jintEngine = new Engine();
            jintEngine.SetValue("context", new { time = "10:2:01" });
            var val = jintEngine.GetValue("context");
            Assert.AreEqual("10:2:01", val);
        }

        public void Test2()
        {

        }
    }
}
