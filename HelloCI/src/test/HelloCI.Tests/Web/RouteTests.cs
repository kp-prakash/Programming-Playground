using System.Web.Routing;
using HelloCI.Web;
using HelloCI.Web.Controllers;
using NUnit.Framework;
using MvcContrib.TestHelper;

namespace HelloCI.Tests.Web
{
    [TestFixture]
    public class RouteTests
    {
        [SetUp]
        public void SetUp()
        {
            RouteTable.Routes.Clear();
            MvcApplication.RegisterRoutes(RouteTable.Routes);
        }

        [Test]
        public void HomeShouldMapToHomeController()
        {
            "~/home".ShouldMapTo<HomeController>(x => x.Index());
        }

        [Test]
        public void HomeIndexShouldMapToHomeController()
        {
            "~/home/index".ShouldMapTo<HomeController>(x => x.Index());
        }

        [Test]
        public void HomeAboutShouldMapToHomeController()
        {
            "~/home/about".ShouldMapTo<HomeController>(x => x.About());
        }

        [Test]
        public void HomeHelpShouldMapToHomeController()
        {
            "~/home/help".ShouldMapTo<HomeController>(x => x.Help());
        }

        [Test]
        public void EmptyShouldMapToHomeController()
        {
            "~/".ShouldMapTo<HomeController>(x => x.Index());
        }
    }
}