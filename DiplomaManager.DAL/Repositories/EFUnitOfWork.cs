using System;
using DiplomaManager.DAL.EF;
using DiplomaManager.DAL.Entities.RequestEntities;
using DiplomaManager.DAL.Interfaces;

namespace DiplomaManager.DAL.Repositories
{
    public sealed class EFUnitOfWork : IUnitOfWork
    {
        private readonly DiplomaManagerContext _db;
        private DevelopmentAreaRepository _developmentAreaRepository;

        public EFUnitOfWork(string connectionString)
        {
            _db = new DiplomaManagerContext(connectionString);
        }

        public IRepository<DevelopmentArea> DevelopmentAreas 
            => _developmentAreaRepository ?? (_developmentAreaRepository = new DevelopmentAreaRepository(_db));

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
