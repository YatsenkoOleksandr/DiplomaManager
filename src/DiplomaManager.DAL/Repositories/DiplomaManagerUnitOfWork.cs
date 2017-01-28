using System;
using DiplomaManager.DAL.EF;
using DiplomaManager.DAL.Entities.ProjectEntities;
using DiplomaManager.DAL.Entities.RequestEntities;
using DiplomaManager.DAL.Entities.StudentEntities;
using DiplomaManager.DAL.Entities.TeacherEntities;
using DiplomaManager.DAL.Interfaces;

namespace DiplomaManager.DAL.Repositories
{
    public sealed class EFUnitOfWork : IDiplomaManagerUnitOfWork
    {
        private readonly DiplomaManagerContext _db;

        private EFRepository<DevelopmentArea> _developmentAreaRepository;

        private EFRepository<Admin> _adminRepository;
        private EFRepository<Teacher> _teacherRepository;
        private EFRepository<Student> _studentRepository;

        private EFRepository<Project> _projectRepository;

        public EFUnitOfWork(string connectionString)
        {
            _db = new DiplomaManagerContext(connectionString);
        }

        public IRepository<DevelopmentArea> DevelopmentAreas 
            => _developmentAreaRepository ?? (_developmentAreaRepository = new EFRepository<DevelopmentArea>(_db));

        public IRepository<Project> Projects
            => _projectRepository ?? (_projectRepository = new EFRepository<Project>(_db));

        public IRepository<Admin> Admins
            => _adminRepository ?? (_adminRepository = new EFRepository<Admin>(_db));

        public IRepository<Teacher> Teachers
            => _teacherRepository ?? (_teacherRepository = new EFRepository<Teacher>(_db));

        public IRepository<Student> Students
            => _studentRepository ?? (_studentRepository = new EFRepository<Student>(_db));

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
