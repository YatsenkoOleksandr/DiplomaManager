using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using DiplomaManager.DAL.Utils;

namespace DiplomaManager.DAL.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> Get();

        TEntity Get(int id);

        IEnumerable<TEntity> Get(
           Expression<Func<TEntity, bool>> filter);

        IEnumerable<TEntity> Get(
           Expression<Func<TEntity, bool>> filter,
           string[] includePaths);

        IEnumerable<TEntity> Get(
           Expression<Func<TEntity, bool>> filter,
           string[] includePaths,
           int? page,
           int? pageSize = null);

        IEnumerable<TEntity> Get(
            string[] includePaths = null,
            int? page = 0,
            int? pageSize = null,
            SortExpression<TEntity>[] sortExpressions = null,
            params Expression<Func<TEntity, bool>>[] filters);

        void Add(TEntity entity);

        void Update(TEntity entity);

        void Remove(TEntity entity);

        void Remove(int id);

        bool IsEmpty();
    }
}
