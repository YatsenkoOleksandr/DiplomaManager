using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using DiplomaManager.DAL.EF;
using DiplomaManager.DAL.Entities.RequestEntities;
using DiplomaManager.DAL.Interfaces;
using DiplomaManager.DAL.Utils;

namespace DiplomaManager.DAL.Repositories
{
    public class DevelopmentAreaRepository : IRepository<DevelopmentArea>
    {
        private readonly DiplomaManagerContext _db;

        public DevelopmentAreaRepository(DiplomaManagerContext context)
        {
            _db = context;
        }

        /*public IEnumerable<DevelopmentArea> Get()
        {
            return _db.DevelopmentAreas.Include(da => da.Teachers);
        }

        public DevelopmentArea Get(int id)
        {
            return _db.DevelopmentAreas.Find(id);
        }

        public IEnumerable<DevelopmentArea> Find(Func<DevelopmentArea, bool> predicate)
        {
            return _db.DevelopmentAreas.Include(da => da.Teachers).Where(predicate);
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

        public bool IsEmpty()
        {
            return !_db.DevelopmentAreas.Any();
        }*/

        public IEnumerable<DevelopmentArea> Get()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DevelopmentArea> Get(Expression<Func<DevelopmentArea, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DevelopmentArea> Get(Expression<Func<DevelopmentArea, bool>> filter, string[] includePaths)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DevelopmentArea> Get(Expression<Func<DevelopmentArea, bool>> filter, 
            string[] includePaths, int? page, int? pageSize = null,
            params SortExpression<DevelopmentArea>[] sortExpressions)
        {
            throw new NotImplementedException();
        }

        public void Add(DevelopmentArea entity)
        {
            throw new NotImplementedException();
        }

        public void Remove(DevelopmentArea entity)
        {
            throw new NotImplementedException();
        }

        public void Update(DevelopmentArea entity)
        {
            throw new NotImplementedException();
        }

        public DevelopmentArea Get(int id)
        {
            throw new NotImplementedException();
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }
    }
}
