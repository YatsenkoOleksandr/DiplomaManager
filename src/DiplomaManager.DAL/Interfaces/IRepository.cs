using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using DiplomaManager.DAL.Utils;

namespace DiplomaManager.DAL.Interfaces
{
    /*public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T Get(int id);
        IEnumerable<T> Find(Func<T, bool> predicate);
        bool IsEmpty();
        void Create(T item);
        void Update(T item);
        void Delete(int id);
    }*/

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
           Expression<Func<TEntity, bool>> filter = null,
           string[] includePaths = null,
           int? page = 0,
           int? pageSize = null,
           params SortExpression<TEntity>[] sortExpressions);

        void Add(TEntity entity);

        void Update(TEntity entity);

        void Remove(TEntity entity);

        void Remove(int id);
    }
}
