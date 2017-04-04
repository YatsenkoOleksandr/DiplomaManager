using System.Collections.Generic;
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
            IncludeExpression<TEntity> includePath);

        IEnumerable<TEntity> Get(
           FilterExpression<TEntity> filter,
           IncludeExpression<TEntity>[] includePaths);

        IEnumerable<TEntity> Get(
           FilterExpression<TEntity>[] filters,
           IncludeExpression<TEntity>[] includePaths);

        IEnumerable<TEntity> Get<TKey>(
            FilterExpression<TEntity>[] filters,
            IncludeExpression<TEntity>[] includePaths,
            int? page = null,
            int? pageSize = null,
            params SortExpression<TEntity, TKey>[] sortExpressions);

        void Add(TEntity entity);

        void Attach(TEntity entity);

        void Update(TEntity entity);

        void Remove(TEntity entity);

        void Remove(int id);

        bool IsEmpty();

        bool IsEmpty(FilterExpression<TEntity> expression);

        int Count(FilterExpression<TEntity>[] filters);

        IEnumerable<TEntity> Local { get; }
    }
}
