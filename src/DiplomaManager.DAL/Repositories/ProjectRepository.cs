using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using DiplomaManager.DAL.EF;
using DiplomaManager.DAL.Entities.ProjectEntities;
using DiplomaManager.DAL.Interfaces;
using DiplomaManager.DAL.Utils;

namespace DiplomaManager.DAL.Repositories
{
    public class ProjectRepository : IRepository<Project>
    {
        private readonly DiplomaManagerContext _db;

        public ProjectRepository(DiplomaManagerContext context)
        {
            _db = context;
        }

        /*public IEnumerable<Project> GetAll()
        {
            return _db.Projects.Include(p => p.ProjectsTitles);
        }

        public Project Get(int id)
        {
            return _db.Projects.Find(id);
        }

        public IEnumerable<Project> Find(Func<Project, bool> predicate)
        {
            return _db.Projects.Include(p => p.ProjectsTitles).Where(predicate);
        }

        public bool IsEmpty()
        {
            return !_db.Projects.Any();
        }

        public void Create(Project item)
        {
            _db.Projects.Add(item);
        }

        public void Update(Project item)
        {
            _db.Entry(item).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            var p = _db.Projects.Find(id);
            if (p != null)
                _db.Projects.Remove(p);
        }*/

        public IEnumerable<Project> Get()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Project> Get(Expression<Func<Project, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Project> Get(Expression<Func<Project, bool>> filter, string[] includePaths)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Project> Get(Expression<Func<Project, bool>> filter, 
            string[] includePaths, 
            int? page, 
            int? pageSize = null,
            params SortExpression<Project>[] sortExpressions)
        {
            throw new NotImplementedException();
        }

        public void Add(Project entity)
        {
            throw new NotImplementedException();
        }

        public void Remove(Project entity)
        {
            throw new NotImplementedException();
        }

        public void Update(Project entity)
        {
            throw new NotImplementedException();
        }

        public Project Get(int id)
        {
            throw new NotImplementedException();
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }
    }
}
