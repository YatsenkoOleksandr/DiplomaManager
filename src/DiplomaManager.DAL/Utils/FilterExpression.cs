using System;
using System.Linq.Expressions;

namespace DiplomaManager.DAL.Utils
{
    public class FilterExpression<TEntity> where TEntity : class
    {
        public FilterExpression(Expression<Func<TEntity, bool>> filter)
        {
            Filter = filter;
        }

        public Expression<Func<TEntity, bool>> Filter { get; set; }
    }
}
