namespace RealEstate.Controllers
{
    using System.Web.Mvc;
    using RealEstate.App_Start;

    public class HomeController : Controller
    {
        private readonly RealEstateContext realEstateContext = new RealEstateContext();

        public ActionResult About()
        {
            ViewBag.Message = "Rentals Application.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Srihari Sridharan.";

            return View();
        }

        public ActionResult Index()
        {
            return Json(realEstateContext.Database.Settings, JsonRequestBehavior.AllowGet);
        }
    }
}