using System;
using DiplomaManager.DAL.Entities.RequestEntities;

namespace DiplomaManager.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<DevelopmentArea> DevelopmentAreas { get; }

        void Save();
    }
}
