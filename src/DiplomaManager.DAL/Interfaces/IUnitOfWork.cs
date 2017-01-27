using System;

namespace DiplomaManager.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        void Save();
    }
}
