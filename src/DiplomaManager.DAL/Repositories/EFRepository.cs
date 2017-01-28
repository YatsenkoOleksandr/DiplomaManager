using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using DiplomaManager.DAL.Interfaces;
using DiplomaManager.DAL.Utils;

namespace DiplomaManager.DAL.Repositories
{
    public class EFRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly DbContext _db;
        private readonly DbSet<TEntity> _dbSet;

        public EFRepository(DbContext context)
        {
            _db = context;
            _dbSet = context.Set<TEntity>();
        }

        public IEnumerable<TEntity> Get()
        {
            return Get(null, null, null, null, null);
        }

        public IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter)
        {
            return Get(filter, null, null);
        }

        public IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter,
            string[] includePaths)
        {
            return Get(filter, includePaths, null);
        }

        public IEnumerable<TEntity> Get(
           Expression<Func<TEntity, bool>> filter,
           string[] includePaths,
           int? page,
           int? pageSize = null)
        {
            return Get(includePaths, page, pageSize);
        }

        public IEnumerable<TEntity> Get(
            string[] includePaths = null, 
            int? page = 0, 
            int? pageSize = null,
            FilterExpression<TEntity>[] filters = null,
            params SortExpression<TEntity>[] sortExpressions)
        {
            IQueryable<TEntity> query = _dbSet;

            if (filters != null)
            {
                foreach (var t in filters)
                {
                    if (t != null)
                        query = query.Where(t.Filter);
                }
            }

            if (includePaths != null)
            {
                for (var i = 0; i < includePaths.Count(); i++)
                {
                    query = query.Include(includePaths[i]);
                }
            }

            if (sortExpressions != null)
            {
                IOrderedQueryable<TEntity> orderedQuery = null;
                for (var i = 0; i < sortExpressions.Length; i++)
                {
                    if (i == 0)
                    {
                        orderedQuery = sortExpressions[i].SortDirection == ListSortDirection.Ascending
                            ? query.OrderBy(sortExpressions[i].SortBy)
                            : query.OrderByDescending(sortExpressions[i].SortBy);
                    }
                    else
                    {
                        if (orderedQuery != null)
                        {
                            orderedQuery = sortExpressions[i].SortDirection == ListSortDirection.Ascending
                                ? orderedQuery.ThenBy(sortExpressions[i].SortBy)
                                : orderedQuery.ThenByDescending(sortExpressions[i].SortBy);
                        }
                    }
                }

                if (page != null && orderedQuery != null && pageSize != null)
                {
                    query = orderedQuery.Skip(((int)page - 1) * (int)pageSize);
                }
            }

            if (pageSize != null)
            {
                query = query.Take((int)pageSize);
            }

            return query.ToList();
        }

        public TEntity Get(int id)
        {
            return _dbSet.Find(id);
        }

        public virtual void Add(TEntity entity)
        {
            _dbSet.Add(entity);
        }

        public virtual void Remove(TEntity entityToDelete)
        {
            if (_db.Entry(entityToDelete).State == EntityState.Detached)
            {
                _dbSet.Attach(entityToDelete);
            }
            _dbSet.Remove(entityToDelete);
        }

        public virtual void Update(TEntity entityToUpdate)
        {
            _dbSet.Attach(entityToUpdate);
            _db.Entry(entityToUpdate).State = EntityState.Modified;
        }

        public void Remove(int id)
        {
            var entity = _dbSet.Find(id);
            if (entity != null)
                _dbSet.Remove(entity);
        }

        public bool IsEmpty()
        {
            return !_db.Set<TEntity>().Any();
        }
    }
}
