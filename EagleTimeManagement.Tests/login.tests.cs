using NUnit.Framework;
using EagleTimeManagement.Controllers;

namespace Tests
{
    [TestFixture]
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            LoginController controller = new LoginController();
            var num = controller.test();
            Assert.AreEqual(num, 1);
        }
    }
}