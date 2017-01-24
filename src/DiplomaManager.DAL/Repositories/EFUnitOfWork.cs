using System;
using DiplomaManager.DAL.EF;
using DiplomaManager.DAL.Entities.RequestEntities;
using DiplomaManager.DAL.Entities.StudentEntities;
using DiplomaManager.DAL.Entities.TeacherEntities;
using DiplomaManager.DAL.Interfaces;

namespace DiplomaManager.DAL.Repositories
{
    public sealed class EFUnitOfWork : IUnitOfWork
    {
        private readonly DiplomaManagerContext _db;
        private DevelopmentAreaRepository _developmentAreaRepository;
        private UserRepository<Admin> _adminRepository;
        private UserRepository<Teacher> _teacherRepository;
        private UserRepository<Student> _studentRepository;

        public EFUnitOfWork(string connectionString)
        {
            _db = new DiplomaManagerContext(connectionString);
        }

        public IRepository<DevelopmentArea> DevelopmentAreas 
            => _developmentAreaRepository ?? (_developmentAreaRepository = new DevelopmentAreaRepository(_db));

        public IRepository<Admin> Admins
            => _adminRepository ?? (_adminRepository = new UserRepository<Admin>(_db));

        public IRepository<Teacher> Teachers
            => _teacherRepository ?? (_teacherRepository = new UserRepository<Teacher>(_db));

        public IRepository<Student> Students
            => _studentRepository ?? (_studentRepository = new UserRepository<Student>(_db));

        public void Save()
        {
            _db.SaveChanges();
        }

        private bool _disposed;

        private void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _db.Dispose();
                }
                _disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
