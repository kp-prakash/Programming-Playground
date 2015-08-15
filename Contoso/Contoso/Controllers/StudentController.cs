using System;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Routing;
using Contoso.DAL;
using Contoso.Models;
using PagedList;
using System.Collections.Generic;

namespace Contoso.Controllers
{
    public class StudentController : Controller
    {
        private readonly IStudentRepository _studentRepository;

        public StudentController()
        {
            _studentRepository = new StudentRepository(new SchoolContext());
        }

        public StudentController(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        private const string ErrorMessage = "Unable to save changes." +
            "Try again, and if the problem persists see your system administrator.";

        //
        // GET: /Student/

        public ViewResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            //The sort order should be maintained while the user performs paging.
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = string.IsNullOrEmpty(sortOrder) ? "Name desc" : string.Empty;
            ViewBag.DateSortParm = sortOrder == "Date" ? "Date desc" : "Date";
            if (Request.HttpMethod == "GET")
            {
                searchString = currentFilter;
            }
            else
            {
                //The page should be reset when the user resets the search strings and
                //posts the form to the server. Hence setting page # to 1.
                page = 1;
            }

            //The search string should be shown to the user while traversing across pages.
            ViewBag.CurrentFilter = searchString;
            var students = from s in _studentRepository.GetStudents()
                           select s;
            students = FilterByName(searchString, students);
            students = UpdateSortingOrder(sortOrder, students);
            const int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(students.ToPagedList(pageNumber, pageSize));
        }

        private static IEnumerable<Student> UpdateSortingOrder(string sortOrder, IEnumerable<Student> students)
        {
            Func<Student, DateTime?> enrollmentDate = student => student.EnrollmentDate;
            Func<Student, string> lastName = student => student.LastName;
            switch (sortOrder)
            {
                case "Name desc":
                    students = students.OrderByDescending(lastName);
                    break;

                case "Date":
                    students = students.OrderBy(enrollmentDate);
                    break;

                case "Date desc":
                    students = students.OrderByDescending(enrollmentDate);
                    break;

                default:
                    students = students.OrderBy(lastName);
                    break;
            }
            return students;
        }

        private static IEnumerable<Student> FilterByName(string searchString, IEnumerable<Student> students)
        {
            if (!string.IsNullOrWhiteSpace(searchString))
            {
                searchString = searchString.ToUpper();
                students = students.Where(s => s.FirstMiddleName.ToUpper().Contains(searchString)
                    || s.LastName.ToUpper().Contains(searchString));
            }
            return students;
        }

        //
        // GET: /Student/Details/5

        public ViewResult Details(int id)
        {
            Student student = _studentRepository.GetStudentByID(id);
            return View(student);
        }

        //
        // GET: /Student/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Student/Create

        [HttpPost]
        public ActionResult Create(Student student)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _studentRepository.InsertStudent(student);
                    _studentRepository.Save();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException)
            {
                //Log the error (add a variable name after DataException)
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }
            return View(student);
        }

        //
        // GET: /Student/Edit/5

        public ActionResult Edit(int id)
        {
            Student student = _studentRepository.GetStudentByID(id);
            return View(student);
        }

        //
        // POST: /Student/Edit/5

        [HttpPost]
        public ActionResult Edit(Student student)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _studentRepository.UpdateStudent(student);
                    _studentRepository.Save();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException)
            {
                //Log the error (add a variable name after DataException)
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }
            return View(student);
        }

        //
        // GET: /Student/Delete/5

        public ActionResult Delete(int id, bool? saveChangesError)
        {
            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = ErrorMessage;
            }
            Student student = _studentRepository.GetStudentByID(id);
            return View(student);
        }

        //
        // POST: /Student/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                //Scaffold Code
                //Student student = _studentRepository.GetStudentByID(id);
                //_studentRepository.DeleteStudent(student);
                //_studentRepository.Save();

                //The code below helps improve performance.
                _studentRepository.DeleteStudent(id);
                _studentRepository.Save();
            }
            catch (DataException)
            {
                //Log the error (add a variable name after DataException)
                return RedirectToAction("Delete",
                    new RouteValueDictionary
                    {
                        {"id", id},
                        {"saveChangesError", true}
                    });
            }
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            _studentRepository.Dispose();
            base.Dispose(disposing);
        }
    }
}