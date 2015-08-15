using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web.Mvc;
using Contoso.DAL;
using Contoso.Models;

namespace Contoso.Controllers
{
    using System;

    public class DepartmentController : Controller
    {
        private readonly SchoolContext _db = new SchoolContext();

        //
        // GET: /Department/

        public ViewResult Index()
        {
            var departments = this._db.Departments.Include(d => d.Administrator);
            return View(departments.ToList());
        }

        //
        // GET: /Department/Details/5

        public ViewResult Details(int id)
        {
            Department department = this._db.Departments.Find(id);
            return View(department);
        }

        //
        // GET: /Department/Create

        public ActionResult Create()
        {
            ViewBag.PersonID = new SelectList(this._db.Instructors, "PersonID", "FullName");
            return View();
        }

        //
        // POST: /Department/Create

        [HttpPost]
        public ActionResult Create(Department department)
        {
            if (ModelState.IsValid)
            {
                this._db.Departments.Add(department);
                this._db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PersonID = new SelectList(this._db.Instructors, "PersonID", "FullName", department.PersonID);
            return View(department);
        }

        //
        // GET: /Department/Edit/5

        public ActionResult Edit(int id)
        {
            Department department = this._db.Departments.Find(id);
            ViewBag.PersonID = new SelectList(this._db.Instructors, "PersonID", "FullName", department.PersonID);
            return View(department);
        }

        //
        // POST: /Department/Edit/5

        [HttpPost]
        public ActionResult Edit(Department department)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ValidateOneAdministratorAssignmentPerInstructor(department);
                }
                if (ModelState.IsValid)
                {
                    _db.Entry(department).State = EntityState.Modified;
                    _db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (DbUpdateConcurrencyException ex)
            {
                var entry = ex.Entries.Single();
                var databaseValues = (Department)entry.GetDatabaseValues().ToObject();
                var clientValues = (Department)entry.Entity;
                if (databaseValues.Name != clientValues.Name)
                {
                    ModelState.AddModelError("Name", Resource.CurrentValue + databaseValues.Name);
                }
                if (databaseValues.Budget != clientValues.Budget)
                {
                    ModelState.AddModelError("Budget", Resource.CurrentValue + databaseValues.Budget);
                }
                if (databaseValues.StartDate != clientValues.StartDate)
                {
                    ModelState.AddModelError("StartDate", Resource.CurrentValue + databaseValues.StartDate);
                }
                if (databaseValues.PersonID != clientValues.PersonID)
                {
                    ModelState.AddModelError("PersonID", Resource.CurrentValue
                        + this._db.Instructors.Find(databaseValues.PersonID).FullName);
                }
                ModelState.AddModelError(string.Empty, Resource.EditConcurrencyError);
                department.TimeStamp = databaseValues.TimeStamp;
            }
            catch (DataException)
            {
                //Log the error (add a variable name after Exception)
                ModelState.AddModelError(string.Empty, Resource.SaveError);
            }
            ViewBag.PersonID = new SelectList(this._db.Instructors, "PersonID", "FullName", department.PersonID);
            return View(department);
        }

        //
        // GET: /Department/Delete/5

        public ActionResult Delete(int id, bool? concurrencyError)
        {
            if (concurrencyError.GetValueOrDefault())
            {
                ViewBag.ConcurrencyErrorMessage = Resource.DeleteConcurrencyError;
            }
            Department department = this._db.Departments.Find(id);
            return View(department);
        }

        //
        // POST: /Department/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(Department department)
        {
            try
            {
                this._db.Entry(department).State = EntityState.Deleted;
                this._db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (DbUpdateConcurrencyException)
            {
                return RedirectToAction("Delete",
                    new System.Web.Routing.RouteValueDictionary { { "concurrencyError", true } });
            }
            catch (DataException)
            {
                //Log the error (add a variable name after Exception)
                ModelState.AddModelError(string.Empty, Resource.SaveError);
                return View(department);
            }
        }

        private void ValidateOneAdministratorAssignmentPerInstructor(Department department)
        {
            if (department.PersonID == null) return;
            var duplicateDepartment =
                _db.Departments.Include("Administrator").Where(d => d.PersonID == department.PersonID).AsNoTracking().
                    FirstOrDefault();
            if (duplicateDepartment == null || duplicateDepartment.DepartmentID == department.DepartmentID) return;
            var errorMessage = string.Format(
                "Instructor {0} {1} is already administrator of the {2} department.",
                duplicateDepartment.Administrator.FirstMiddleName,
                duplicateDepartment.Administrator.LastName,
                duplicateDepartment.Name);
            ModelState.AddModelError(string.Empty, errorMessage);
        }

        protected override void Dispose(bool disposing)
        {
            this._db.Dispose();
            base.Dispose(disposing);
        }
    }
}