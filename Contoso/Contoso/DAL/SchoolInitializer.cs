﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using Contoso.Models;

namespace Contoso.DAL
{
    public class SchoolInitializer : DropCreateDatabaseIfModelChanges<SchoolContext>
    {
        protected override void Seed(SchoolContext context)
        {
            InitializeStudents(context);
            var instructors = InitializeInstructors(context);
            InitializeDepartments(context);
            var courses = InitializeCourses(context);
            AssignInstructors(context, instructors, courses);
            EnrollStudents(context);
            AssignOffices(context);
        }

        private static void AssignOffices(SchoolContext context)
        {
            var officeAssignments = new List<OfficeAssignment>
            {
                new OfficeAssignment { PersonID = 9, Location = "Smith 17" },
                new OfficeAssignment { PersonID = 10, Location = "Gowan 27" },
                new OfficeAssignment { PersonID = 11, Location = "Thompson 304" },
            };
            officeAssignments.ForEach(s => context.OfficeAssignments.Add(s));
            context.SaveChanges();
        }

        private static void EnrollStudents(SchoolContext context)
        {
            var enrollments = new List<Enrollment>
            {
                new Enrollment { PersonID = 1, CourseID = 1050, Grade = 1 },
                new Enrollment { PersonID = 1, CourseID = 4022, Grade = 3 },
                new Enrollment { PersonID = 1, CourseID = 4041, Grade = 1 },
                new Enrollment { PersonID = 2, CourseID = 1045, Grade = 2 },
                new Enrollment { PersonID = 2, CourseID = 3141, Grade = 4 },
                new Enrollment { PersonID = 2, CourseID = 2021, Grade = 4 },
                new Enrollment { PersonID = 3, CourseID = 1050            },
                new Enrollment { PersonID = 4, CourseID = 1050,           },
                new Enrollment { PersonID = 4, CourseID = 4022, Grade = 4 },
                new Enrollment { PersonID = 5, CourseID = 4041, Grade = 3 },
                new Enrollment { PersonID = 6, CourseID = 1045            },
                new Enrollment { PersonID = 7, CourseID = 3141, Grade = 2 },
            };
            enrollments.ForEach(s => context.Enrollments.Add(s));
            context.SaveChanges();
        }

        private static void AssignInstructors(SchoolContext context, List<Instructor> instructors, List<Course> courses)
        {
            courses[0].Instructors.Add(instructors[0]);
            courses[0].Instructors.Add(instructors[1]);
            courses[1].Instructors.Add(instructors[2]);
            courses[2].Instructors.Add(instructors[2]);
            courses[3].Instructors.Add(instructors[3]);
            courses[4].Instructors.Add(instructors[3]);
            courses[5].Instructors.Add(instructors[3]);
            courses[6].Instructors.Add(instructors[3]);
            context.SaveChanges();
        }

        private static List<Course> InitializeCourses(SchoolContext context)
        {
            var courses = new List<Course>
            {
                new Course { CourseID = 1050, Title = "Chemistry",      Credits = 3, DepartmentID = 3, Instructors = new List<Instructor>() },
                new Course { CourseID = 4022, Title = "Microeconomics", Credits = 3, DepartmentID = 4, Instructors = new List<Instructor>() },
                new Course { CourseID = 4041, Title = "Macroeconomics", Credits = 3, DepartmentID = 4, Instructors = new List<Instructor>() },
                new Course { CourseID = 1045, Title = "Calculus",       Credits = 4, DepartmentID = 2, Instructors = new List<Instructor>() },
                new Course { CourseID = 3141, Title = "Trigonometry",   Credits = 4, DepartmentID = 2, Instructors = new List<Instructor>() },
                new Course { CourseID = 2021, Title = "Composition",    Credits = 3, DepartmentID = 1, Instructors = new List<Instructor>() },
                new Course { CourseID = 2042, Title = "Literature",     Credits = 4, DepartmentID = 1, Instructors = new List<Instructor>() }
            };
            courses.ForEach(s => context.Courses.Add(s));
            context.SaveChanges();
            return courses;
        }

        private static void InitializeDepartments(SchoolContext context)
        {
            var departments = new List<Department>
            {
                new Department { Name = "English",     Budget = 350000, StartDate = DateTime.Parse("2007-09-01"), PersonID = 9 },
                new Department { Name = "Mathematics", Budget = 100000, StartDate = DateTime.Parse("2007-09-01"), PersonID = 10 },
                new Department { Name = "Engineering", Budget = 350000, StartDate = DateTime.Parse("2007-09-01"), PersonID = 11 },
                new Department { Name = "Economics",   Budget = 100000, StartDate = DateTime.Parse("2007-09-01"), PersonID = 12 }
            };
            departments.ForEach(s => context.Departments.Add(s));
            context.SaveChanges();
        }

        private static List<Instructor> InitializeInstructors(SchoolContext context)
        {
            var instructors = new List<Instructor>
            {
                new Instructor { FirstMiddleName = "Kim",     LastName = "Abercrombie", HireDate = DateTime.Parse("1995-03-11") },
                new Instructor { FirstMiddleName = "Fadi",    LastName = "Fakhouri",    HireDate = DateTime.Parse("2002-07-06") },
                new Instructor { FirstMiddleName = "Roger",   LastName = "Harui",       HireDate = DateTime.Parse("1998-07-01") },
                new Instructor { FirstMiddleName = "Candace", LastName = "Kapoor",      HireDate = DateTime.Parse("2001-01-15") },
                new Instructor { FirstMiddleName = "Roger",   LastName = "Zheng",       HireDate = DateTime.Parse("2004-02-12") }
            };
            instructors.ForEach(s => context.Instructors.Add(s));
            context.SaveChanges();
            return instructors;
        }

        private static void InitializeStudents(SchoolContext context)
        {
            var students = new List<Student>
            {
                new Student { FirstMiddleName = "Carson",   LastName = "Alexander", EnrollmentDate = DateTime.Parse("2005-09-01") },
                new Student { FirstMiddleName = "Meredith", LastName = "Alonso",    EnrollmentDate = DateTime.Parse("2002-09-01") },
                new Student { FirstMiddleName = "Arturo",   LastName = "Anand",     EnrollmentDate = DateTime.Parse("2003-09-01") },
                new Student { FirstMiddleName = "Gytis",    LastName = "Barzdukas", EnrollmentDate = DateTime.Parse("2002-09-01") },
                new Student { FirstMiddleName = "Yan",      LastName = "Li",        EnrollmentDate = DateTime.Parse("2002-09-01") },
                new Student { FirstMiddleName = "Peggy",    LastName = "Justice",   EnrollmentDate = DateTime.Parse("2001-09-01") },
                new Student { FirstMiddleName = "Laura",    LastName = "Norman",    EnrollmentDate = DateTime.Parse("2003-09-01") },
                new Student { FirstMiddleName = "Nino",     LastName = "Olivetto",  EnrollmentDate = DateTime.Parse("2005-09-01") }
            };
            students.ForEach(s => context.Students.Add(s));
            context.SaveChanges();
        }
    }
}