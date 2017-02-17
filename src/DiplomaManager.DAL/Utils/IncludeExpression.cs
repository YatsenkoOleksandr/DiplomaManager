using System;
using System.Linq.Expressions;

namespace DiplomaManager.DAL.Utils
{
    public class IncludeExpression<TEntity> where TEntity : class
    {
        public IncludeExpression(Expression<Func<TEntity, object>> property)
        {
            Property = property;
        }

        public Expression<Func<TEntity, object>> Property { get; set; }
    }
}
