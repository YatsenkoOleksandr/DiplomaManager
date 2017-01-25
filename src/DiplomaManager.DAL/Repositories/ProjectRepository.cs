using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DiplomaManager.DAL.EF;
using DiplomaManager.DAL.Entities.ProjectEntities;
using DiplomaManager.DAL.Interfaces;

namespace DiplomaManager.DAL.Repositories
{
    public class ProjectRepository : IRepository<Project>
    {
        private readonly DiplomaManagerContext _db;

        public ProjectRepository(DiplomaManagerContext context)
        {
            _db = context;
        }

        public IEnumerable<Project> GetAll()
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
        }
    }
}
