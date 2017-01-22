using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DiplomaManager.DAL.EF;
using DiplomaManager.DAL.Entities.RequestEntities;
using DiplomaManager.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DiplomaManager.DAL.Repositories
{
    public class DevelopmentAreaRepository : IRepository<DevelopmentArea>
    {
        private readonly DiplomaManagerContext _db;

        public DevelopmentAreaRepository(DiplomaManagerContext context)
        {
            _db = context;
        }

        public IEnumerable<DevelopmentArea> GetAll()
        {
            return _db.DevelopmentAreas.Include(da => da.Interests);
        }

        public DevelopmentArea Get(int id)
        {
            return _db.DevelopmentAreas.Find(id);
        }

        public IEnumerable<DevelopmentArea> Find(Func<DevelopmentArea, bool> predicate)
        {
            return _db.DevelopmentAreas.Include(da => da.Interests).Where(predicate);
        }

        public void Create(DevelopmentArea item)
        {
            _db.DevelopmentAreas.Add(item);
        }

        public void Update(DevelopmentArea item)
        {
            _db.Entry(item).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            var da = _db.DevelopmentAreas.Find(id);
            if (da != null)
                _db.DevelopmentAreas.Remove(da);
        }
    }
}
