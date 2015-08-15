using System;
using Contoso.Models;

namespace Contoso.DAL
{
    public class UnitOfWork : IDisposable
    {
        private readonly SchoolContext _context = new SchoolContext();
        private GenericRepository<Department> _departmentRepository;
        private CourseRepository _courseRepository;
        private bool _disposed = false;

        public GenericRepository<Department> DepartmentRepository
        {
            get
            {
                if (null == _departmentRepository)
                    _departmentRepository = new GenericRepository<Department>(_context);
                return _departmentRepository;
            }
        }

        public CourseRepository CourseRepository
        {
            get
            {
                return _courseRepository ?? (_courseRepository = new CourseRepository(this._context));
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            _disposed = true;
        }
    }
}