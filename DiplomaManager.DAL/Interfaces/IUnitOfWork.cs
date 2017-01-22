using System;
using DiplomaManager.DAL.Repositories;

namespace DiplomaManager.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<DevelopmentAreaRepository> Phones { get; }

        void Save();
    }
}
