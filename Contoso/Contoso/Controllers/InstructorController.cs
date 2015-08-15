using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using Contoso.DAL;
using Contoso.Models;
using Contoso.ViewModels;

namespace Contoso.Controllers
{
    public class InstructorController : Controller
    {
        private readonly SchoolContext _db = new SchoolContext();

        //
        // GET: /Instructor/

        public ViewResult Index(Int32? id, Int32? courseID)
        {
            #region Eager Loading

            //var viewModel = new InstructorIndexData();
            //viewModel.Instructors = db.Instructors
            //                            .Include(i => i.Courses.Select(c => c.Department))
            //                            .OrderBy(i => i.LastName);
            //if (null != id)
            //{
            //    //Fetch the matching courses for the selected instructor.
            //    ViewBag.PersonID = id;
            //    viewModel.Courses = viewModel.Instructors.Where(i => i.PersonID == id.Value)
            //                        .Single().Courses;
            //}

            //if (null != courseID)
            //{
            //    //Fetchthe matching enrollments for the selected course.
            //    ViewBag.CourseID = courseID.Value;
            //    viewModel.Enrollments = viewModel.Courses.Where(c => c.CourseID == courseID)
            //                            .Single().Enrollments;
            //}
            //return View(viewModel);

            #endregion Eager Loading

            #region Explicit Loading

            var viewModel = new InstructorIndexData();
            viewModel.Instructors = this._db.Instructors
                                        .Include(i => i.Courses.Select(c => c.Department))
                                        .OrderBy(i => i.LastName);
            if (id != null)
            {
                ViewBag.PersonID = id.Value;
                viewModel.Courses = viewModel.Instructors
                                                .Where(i => i.PersonID == id.Value)
                                                .Single()
                                                .Courses;
            }
            if (null != courseID)
            {
                ViewBag.CourseID = courseID.Value;
                var selectedCourse = viewModel.Courses
                                                .Where(c => c.CourseID == courseID.Value)
                                                .Single();

                //Use the Collection method to explicitly load a collection of entities.
                this._db.Entry(selectedCourse).Collection(x => x.Enrollments).Load();
                foreach (var enrollment in selectedCourse.Enrollments)
                {
                    //Use the Reference method to explicitly load an entity.
                    this._db.Entry(enrollment).Reference(x => x.Student).Load();
                }
                viewModel.Enrollments = selectedCourse.Enrollments;
            }
            return View(viewModel);

            #endregion Explicit Loading
        }

        //
        // GET: /Instructor/Details/5

        public ViewResult Details(int id)
        {
            Instructor instructor = this._db.Instructors.Find(id);
            return View(instructor);
        }

        //
        // GET: /Instructor/Create

        public ActionResult Create()
        {
            ViewBag.PersonID = new SelectList(this._db.OfficeAssignments, "PersonID", "Location");
            return View();
        }

        //
        // POST: /Instructor/Create

        [HttpPost]
        public ActionResult Create(Instructor instructor)
        {
            if (ModelState.IsValid)
            {
                this._db.Instructors.Add(instructor);
                this._db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PersonID = new SelectList(this._db.OfficeAssignments, "PersonID", "Location", instructor.PersonID);
            return View(instructor);
        }

        //
        // GET: /Instructor/Edit/5

        public ActionResult Edit(int id)
        {
            Instructor instructor = this._db.Instructors
                .Include(i => i.Courses)
                .Where(i => i.PersonID == id)
                .Single();
            PopulateAssignedCourseData(instructor);
            return View(instructor);
        }

        private void PopulateAssignedCourseData(Instructor instructor)
        {
            var allCourses = this._db.Courses;
            var instructorCourses = new HashSet<int>(instructor.Courses.Select(c => c.CourseID));
            var viewModel = new List<AssignedCourseData>();
            foreach (var course in allCourses)
            {
                viewModel.Add(new AssignedCourseData
                {
                    CourseID = course.CourseID,
                    Title = course.Title,
                    Assigned = instructorCourses.Contains(course.CourseID)
                });
            }
            ViewBag.Courses = viewModel;
        }

        //
        // POST: /Instructor/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection formCollection, string[] selectedCourses)
        {
            var instructorToUpdate = this._db.Instructors
                                        .Include(i => i.Courses)
                                        .Where(i => i.PersonID == id)
                                        .Single();
            if (TryUpdateModel(instructorToUpdate, "", null, new string[] { "Courses" }))
            {
                try
                {
                    if (string.IsNullOrWhiteSpace(instructorToUpdate.OfficeAssignment.Location))
                    {
                        instructorToUpdate.OfficeAssignment = null;
                    }

                    UpdateInstructorCourses(selectedCourses, instructorToUpdate);

                    this._db.Entry(instructorToUpdate).State = EntityState.Modified;
                    this._db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (DataException)
                {
                    //Log the error (add a variable name after DataException)
                    ModelState.AddModelError(string.Empty, Resource.SaveError);
                    return View();
                }
            }
            PopulateAssignedCourseData(instructorToUpdate);
            return View(instructorToUpdate);
        }

        private void UpdateInstructorCourses(IEnumerable<string> selectedCourses, Instructor instructorToUpdate)
        {
            if (selectedCourses == null)
            {
                instructorToUpdate.Courses = new List<Course>();
                return;
            }
            var selectedCoursesHS = new HashSet<string>(selectedCourses);
            var instructorCourses = new HashSet<int>(instructorToUpdate.Courses.Select(c => c.CourseID));
            foreach (var course in this._db.Courses)
            {
                if (selectedCoursesHS.Contains(course.CourseID.ToString()))
                {
                    if (!instructorCourses.Contains(course.CourseID))
                    {
                        instructorToUpdate.Courses.Add(course);
                    }
                }
                else
                {
                    if (instructorCourses.Contains(course.CourseID))
                    {
                        instructorToUpdate.Courses.Remove(course);
                    }
                }
            }
        }

        //
        // GET: /Instructor/Delete/5

        public ActionResult Delete(int id)
        {
            Instructor instructor = this._db.Instructors.Find(id);
            return View(instructor);
        }

        //
        // POST: /Instructor/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Instructor instructor = this._db.Instructors.Find(id);
            this._db.Instructors.Remove(instructor);
            this._db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            this._db.Dispose();
            base.Dispose(disposing);
        }
    }
}