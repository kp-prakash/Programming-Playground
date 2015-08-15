using System;
using System.Collections.Generic;
using Contoso.Models;

namespace Contoso.DAL
{
    public interface IStudentRepository : IDisposable
    {
        IEnumerable<Student> GetStudents();

        Student GetStudentByID(int studentID);

        void InsertStudent(Student student);

        void DeleteStudent(int studentID);

        void UpdateStudent(Student student);

        void Save();
    }
}