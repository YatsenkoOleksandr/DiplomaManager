using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using DiplomaManager.DAL.Interfaces;
using DiplomaManager.DAL.Utils;

namespace DiplomaManager.DAL.Repositories
{
    public class EFRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly DbContext _db;
        private readonly DbSet<TEntity> _dbSet;

        public IEnumerable<TEntity> Local => _dbSet.Local;

        public EFRepository(DbContext context)
        {
            _db = context;
            _dbSet = context.Set<TEntity>();
        }

        public IEnumerable<TEntity> Get()
        {
            return Get<object>(null, null, null, null, null);
        }

        public IEnumerable<TEntity> Get(
            FilterExpression<TEntity> filter)
        {
            return Get<object>(new[] { new FilterExpression<TEntity>(filter.Filter) }, null, null, null, null);
        }

        public IEnumerable<TEntity> Get(
            FilterExpression<TEntity>[] filters)
        {
            return Get<object>(filters, null, null, null, null);
        }

        public IEnumerable<TEntity> Get(
            IncludeExpression<TEntity> includePath)
        {
            return Get<object>(null, new[] { new IncludeExpression<TEntity>(includePath.Property) }, null, null, null);
        }

        public IEnumerable<TEntity> Get(
           FilterExpression<TEntity> filter,
            IncludeExpression<TEntity>[] includePaths)
        {
            return Get<object>(new[] { new FilterExpression<TEntity>(filter.Filter) }, includePaths, null, null, null);
        }

        public IEnumerable<TEntity> Get(
            FilterExpression<TEntity>[] filters, 
            IncludeExpression<TEntity>[] includePaths)
        {
            return Get<object>(filters, includePaths, null, null, null);
        }

        public IEnumerable<TEntity> Get<TKey>(
            FilterExpression<TEntity>[] filters,
            IncludeExpression<TEntity>[] includePaths, 
            int? page, 
            int? pageSize,
            SortExpression<TEntity, TKey>[] sortExpressions)
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
                foreach (var p in includePaths)
                {
                    query = query.Include(p.Property);
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

                if (page != null && pageSize != null)
                {
                    if (orderedQuery == null) throw new ArgumentNullException(nameof(sortExpressions));
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

        public virtual void Attach(TEntity entity)
        {
            _dbSet.Attach(entity);
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

        public bool IsEmpty(FilterExpression<TEntity> expression)
        {
            return !_db.Set<TEntity>().Any(expression.Filter);
        }

        public int Count(FilterExpression<TEntity>[] filters)
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

            return query.Count();
        }
    }
}
