using System.Linq;
using System.Web.Mvc;
using Contoso.DAL;
using Contoso.ViewModels;

namespace Contoso.Controllers
{
    public class HomeController : Controller
    {
        private readonly SchoolContext _db = new SchoolContext();

        public ActionResult Index()
        {
            ViewBag.Message = string.Format("Welcome to {0}", Resource.UniversityName);
            return View();
        }

        public ActionResult About()
        {
            //Group students by enrollment date.
            //var data = from student in db.Students
            //           group student by student.EnrollmentDate into dateGroup
            //           select new EnrollmentDateGroup
            //           {
            //               EnrollmentDate = dateGroup.Key,
            //               StudentCount = dateGroup.Count()
            //           };

            //Calling a Query that Returns Other Types of Objects
            const string query = "SELECT EnrollmentDate, COUNT(*) AS StudentCount "
                                 + "FROM Person "
                                 + "WHERE EnrollmentDate IS NOT NULL "
                                 + "GROUP BY EnrollmentDate";
            var data = this._db.Database.SqlQuery<EnrollmentDateGroup>(query);
            return View(data);
        }

        protected override void Dispose(bool disposing)
        {
            this._db.Dispose();
            base.Dispose(disposing);
        }
    }
}