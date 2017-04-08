using System;
using DiplomaManager.DAL.Configuration;
using DiplomaManager.DAL.EF;
using DiplomaManager.DAL.Entities.ProjectEntities;
using DiplomaManager.DAL.Entities.RequestEntities;
using DiplomaManager.DAL.Entities.SharedEntities;
using DiplomaManager.DAL.Entities.StudentEntities;
using DiplomaManager.DAL.Entities.TeacherEntities;
using DiplomaManager.DAL.Entities.UserEnitites;
using DiplomaManager.DAL.Interfaces;

namespace DiplomaManager.DAL.Repositories
{
    public sealed class EFUnitOfWork : IDiplomaManagerUnitOfWork
    {
        private readonly DiplomaManagerContext _db;

        private EFRepository<Locale> _localeRepository;
        private EFRepository<DevelopmentArea> _developmentAreaRepository;

        private EFRepository<User> _userRepository;
        private EFRepository<Admin> _adminRepository;
        private EFRepository<Teacher> _teacherRepository;
        private EFRepository<Student> _studentRepository;

        private EFRepository<Project> _projectRepository;
        private EFRepository<ProjectTitle> _projectTitleRepository;

        private EFRepository<Position> _positionRepository;
        private EFRepository<PositionName> _positionNameRepository;

        private EFRepository<PeopleName> _peopleNameRepository;

        private EFRepository<Degree> _degreeRepository;
        private EFRepository<DegreeName> _degreeNameRepository;
        private EFRepository<Capacity> _capacityRepository;

        private EFRepository<Group> _groupRepository;

        public EFUnitOfWork(IDatabaseConnectionConfiguration databaseConnectionConfiguration)
        {
            _db = new DiplomaManagerContext(databaseConnectionConfiguration.ConnectionString);
        }

        public IRepository<Locale> Locales
            => _localeRepository ?? (_localeRepository = new EFRepository<Locale>(_db));
        public IRepository<DevelopmentArea> DevelopmentAreas 
            => _developmentAreaRepository ?? (_developmentAreaRepository = new EFRepository<DevelopmentArea>(_db));

        public IRepository<User> Users
            => _userRepository ?? (_userRepository = new EFRepository<User>(_db));
        public IRepository<Admin> Admins
            => _adminRepository ?? (_adminRepository = new EFRepository<Admin>(_db));
        public IRepository<Teacher> Teachers
            => _teacherRepository ?? (_teacherRepository = new EFRepository<Teacher>(_db));
        public IRepository<Student> Students
            => _studentRepository ?? (_studentRepository = new EFRepository<Student>(_db));

        public IRepository<Project> Projects
            => _projectRepository ?? (_projectRepository = new EFRepository<Project>(_db));
        public IRepository<ProjectTitle> ProjectTitles
            => _projectTitleRepository ?? (_projectTitleRepository = new EFRepository<ProjectTitle>(_db));

        public IRepository<Position> Positions
            => _positionRepository ?? (_positionRepository = new EFRepository<Position>(_db));
        public IRepository<PositionName> PositionNames
            => _positionNameRepository ?? (_positionNameRepository = new EFRepository<PositionName>(_db));

        public IRepository<PeopleName> PeopleNames
            => _peopleNameRepository ?? (_peopleNameRepository = new EFRepository<PeopleName>(_db));

        public IRepository<Degree> Degrees
            => _degreeRepository ?? (_degreeRepository = new EFRepository<Degree>(_db));
        public IRepository<DegreeName> DegreeNames
            => _degreeNameRepository ?? (_degreeNameRepository = new EFRepository<DegreeName>(_db));
        public IRepository<Capacity> Capacities
            => _capacityRepository ?? (_capacityRepository = new EFRepository<Capacity>(_db));

        public IRepository<Group> Groups
            => _groupRepository ?? (_groupRepository = new EFRepository<Group>(_db));

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
