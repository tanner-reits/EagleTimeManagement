using NUnit.Framework;
using EagleTimeManagement.Controllers;
using EagleTimeManagement.Models;
using Microsoft.AspNetCore.Mvc;

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
        public void IndexViewTest()
        {
            // Instantiate login controller
            LoginController controller = new LoginController();
            var res = controller.Index();

            Assert.IsInstanceOf<ViewResult>(res);
        }

        [Test]
        public void InvalidCredentialsTest() {
            // Create invalid user credentials object
            UserCredentials creds = new UserCredentials();
            creds.Username = "invalidUsername";
            creds.Password = "invalidPassword";

            // Instantiate login controller
            LoginController controller = new LoginController();
            var res = controller.Authenticate(creds);

            Assert.IsInstanceOf<RedirectResult>(res);
        }
    }
}