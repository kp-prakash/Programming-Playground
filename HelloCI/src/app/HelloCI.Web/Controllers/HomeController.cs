﻿using System.Web.Mvc;
using MvcContrib;

namespace HelloCI.Web.Controllers
{
    [HandleError]
    public class HomeController : Controller {
        public ActionResult Index()
        {
            ViewData["Message"] = "Welcome to ASP.NET MVC!";

            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Help()
        {
            return this.RedirectToAction(x => x.About());
        }
    }
}