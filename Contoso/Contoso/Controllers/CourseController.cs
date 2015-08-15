using System.Linq;
using System.Web.Mvc;
using Contoso.DAL;
using Contoso.Models;

namespace Contoso.Controllers
{
    public class CourseController : Controller
    {
        private readonly UnitOfWork _unitOfWork = new UnitOfWork();

        //
        // GET: /Course/

        public ViewResult Index(int? selectedDepartment)
        {
            var departments = _unitOfWork.DepartmentRepository.Get(orderBy: q => q.OrderBy(d => d.Name));
            ViewBag.selectedDepartment = new SelectList(departments, "DepartmentID", "Name", selectedDepartment); 
            int departmentID = selectedDepartment.GetValueOrDefault();
            var data =
                _unitOfWork.CourseRepository.Get(
                    d => !selectedDepartment.HasValue || d.DepartmentID == departmentID,
                    q => q.OrderBy(d => d.CourseID),
                    includeProperties: "Department");
            return View(data);
        }

        //
        // GET: /Course/Details/5

        public ViewResult Details(int id)
        {
            //Course course = _unitOfWork.CourseRepository.GetByID(id);
            //return View(course);

            //Using Raw SQL
            const string query = "SELECT * FROM Course WHERE CourseID = @p0";
            return View(_unitOfWork.CourseRepository.GetWithRawSql(query, id).Single());
        }

        //
        // GET: /Course/Create

        public ActionResult Create()
        {
            PopulateDepartmentsDropDownList();
            return View();
        }

        private void PopulateDepartmentsDropDownList(object selectedDepartment = null)
        {
            var departmentsQuery = _unitOfWork.DepartmentRepository.Get(orderBy: q => q.OrderBy(d => d.Name));
            ViewBag.DepartmentID = new SelectList(departmentsQuery, "DepartmentID", "Name", selectedDepartment);
        }

        //
        // POST: /Course/Create

        [HttpPost]
        public ActionResult Create(Course course)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _unitOfWork.CourseRepository.Insert(course);
                    _unitOfWork.Save();
                    return RedirectToAction("Index");
                }
            }
            catch
            {
                //Log the error (add a variable name after DataException)
                ModelState.AddModelError(string.Empty, Resource.SaveError);
            }
            PopulateDepartmentsDropDownList(course.DepartmentID);
            return View(course);
        }

        //
        // GET: /Course/Edit/5

        public ActionResult Edit(int id)
        {
            Course course = _unitOfWork.CourseRepository.GetByID(id);
            PopulateDepartmentsDropDownList(course.DepartmentID);
            return View(course);
        }

        //
        // POST: /Course/Edit/5

        [HttpPost]
        public ActionResult Edit(Course course)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _unitOfWork.CourseRepository.Update(course);
                    _unitOfWork.Save();
                    return RedirectToAction("Index");
                }
            }
            catch
            {
                //Log the error (add a variable name after DataException)
                ModelState.AddModelError(string.Empty, Resource.SaveError);
            }
            PopulateDepartmentsDropDownList(course.DepartmentID);
            return View(course);
        }

        //
        // GET: /Course/Delete/5

        public ActionResult Delete(int id)
        {
            Course course = _unitOfWork.CourseRepository.GetByID(id);
            return View(course);
        }

        //
        // POST: /Course/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Course course = _unitOfWork.CourseRepository.GetByID(id);
            _unitOfWork.CourseRepository.Delete(course);
            _unitOfWork.Save();
            return RedirectToAction("Index");
        }

        public ActionResult UpdateCourseCredits(int? multiplier)
        {
            if (multiplier != null)
            {
                ViewBag.RowsAffected = _unitOfWork.CourseRepository.UpdateCourseCredits(multiplier.Value);
            }
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            _unitOfWork.Dispose();
            base.Dispose(disposing);
        }
    }
}