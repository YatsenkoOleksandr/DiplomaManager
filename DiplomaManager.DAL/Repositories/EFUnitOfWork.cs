using System;
using DiplomaManager.DAL.EF;
using DiplomaManager.DAL.Entities.RequestEntities;
using DiplomaManager.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DiplomaManager.DAL.Repositories
{
    public class EFUnitOfWork : IDisposable
    {
        private readonly DiplomaManagerContext _db;
        private DevelopmentAreaRepository _developmentAreaRepository;

        public EFUnitOfWork(string connectionString)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DiplomaManagerContext>();
            var options = optionsBuilder
                .UseSqlServer(connectionString)
                .Options;

            _db = new DiplomaManagerContext(options);
        }

        public IRepository<DevelopmentArea> DevelopmentAreas 
            => _developmentAreaRepository ?? (_developmentAreaRepository = new DevelopmentAreaRepository(_db));

        private bool _disposed;

        protected virtual void Dispose(bool disposing)
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
