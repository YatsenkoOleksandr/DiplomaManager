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
           FilterExpression<TEntity> filter);

        IEnumerable<TEntity> Get(
           FilterExpression<TEntity> filter,
           IncludeExpression<TEntity>[] includePaths);

        IEnumerable<TEntity> Get(
           FilterExpression<TEntity> filter,
           IncludeExpression<TEntity>[] includePaths,
           int? page,
           int? pageSize = null);

        IEnumerable<TEntity> Get(
            IncludeExpression<TEntity>[] includePaths = null,
            int? page = 0,
            int? pageSize = null,
            FilterExpression<TEntity>[] filters = null,
            params SortExpression<TEntity>[] sortExpressions);

        void Add(TEntity entity);

        void Update(TEntity entity);

        void Remove(TEntity entity);

        void Remove(int id);

        bool IsEmpty();

        bool IsEmpty(FilterExpression<TEntity> expression);

        IEnumerable<TEntity> Local { get; }
    }
}
