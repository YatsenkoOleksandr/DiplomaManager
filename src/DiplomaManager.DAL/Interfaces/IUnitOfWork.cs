using System;
using DiplomaManager.DAL.Entities.RequestEntities;
using DiplomaManager.DAL.Entities.StudentEntities;
using DiplomaManager.DAL.Entities.TeacherEntities;

namespace DiplomaManager.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<DevelopmentArea> DevelopmentAreas { get; }

        IRepository<Admin> Admins { get; }

        IRepository<Teacher> Teachers { get; }

        IRepository<Student> Students { get; }

        void Save();
    }
}
