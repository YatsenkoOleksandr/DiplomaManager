using System;
using System.ComponentModel;
using System.Linq.Expressions;

namespace DiplomaManager.DAL.Utils
{
    public class SortExpression<TEntity, TKey> where TEntity : class
    {
        public SortExpression(Expression<Func<TEntity, TKey>> sortBy, ListSortDirection sortDirection)
        {
            SortBy = sortBy;
            SortDirection = sortDirection;
        }

        public Expression<Func<TEntity, TKey>> SortBy { get; set; }
        public ListSortDirection SortDirection { get; set; }
    }
}