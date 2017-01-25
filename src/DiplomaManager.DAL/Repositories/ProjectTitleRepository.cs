using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiplomaManager.DAL.EF;
using DiplomaManager.DAL.Entities.ProjectEntities;
using DiplomaManager.DAL.Interfaces;

namespace DiplomaManager.DAL.Repositories
{
    public class ProjectTitleRepository : IRepository<ProjectTitle>
    {
        private readonly DiplomaManagerContext _db;

        public ProjectTitleRepository(DiplomaManagerContext db)
        {
            _db = db;
        }

        public IEnumerable<ProjectTitle> GetAll()
        {
            return _db.ProjectTitles.Include(t => t.Project);
        }

        public ProjectTitle Get(int id)
        {
            return _db.ProjectTitles.Find(id);
        }

        public IEnumerable<ProjectTitle> Find(Func<ProjectTitle, bool> predicate)
        {
            return _db.ProjectTitles.Include(t => t.Project).Where(predicate);
        }

        public bool IsEmpty()
        {
            return !_db.ProjectTitles.Any();
        }

        public void Create(ProjectTitle item)
        {
            _db.ProjectTitles.Add(item);
        }

        public void Update(ProjectTitle item)
        {
            _db.Entry(item).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            var t = _db.ProjectTitles.Find(id);
            if (t != null)
                _db.ProjectTitles.Remove(t);
        }
    }
}
