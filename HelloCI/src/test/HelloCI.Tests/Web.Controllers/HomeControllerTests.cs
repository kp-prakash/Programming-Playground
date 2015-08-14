using System.Web.Mvc;
using HelloCI.Web.Controllers;
using MvcContrib.ActionResults;
using NUnit.Framework;
using MvcContrib.TestHelper;

namespace HelloCI.Tests.Web.Controllers
{
    [TestFixture]
    public class HomeControllerTests
    {
        [Test]
        public void IndexShouldReturnIndexPage()
        {
            var controller = new HomeController();
            var result = controller.Index();
            result.AssertViewRendered()
                  .ForView("");
        }

        [Test]
        public void IndexPageShouldDisplayTheCorrectMessage()
        {
            var controller = new HomeController();
            var result = (ViewResult)controller.Index();
            Assert.That(result.ViewData["Message"], Is.EqualTo("Welcome to ASP.NET MVC!"));
        }

        [Test]
        public void AboutShouldReturnAboutPage()
        {
            var controller = new HomeController();
            var result = controller.About();
            result.AssertViewRendered()
                  .ForView("");
        }

        [Test]
        public void HelpShouldRedirectToAbout()
        {
            var controller = new HomeController();
            var result = (RedirectToRouteResult<HomeController>)controller.Help();
            result.AssertActionRedirect()
                  .ToAction<HomeController>(x => x.About());
        }
    }
}