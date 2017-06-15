using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Text.RegularExpressions;
using System.Transactions;

namespace DiplomaManager.DAL.Utils
{
    public static class DbContextExtensions
    {
        public static void AddWithId<TEntity>(this DbContext context, TEntity entity)
            where TEntity : class
        {
            using (var scope = new TransactionScope(TransactionScopeOption.RequiresNew))
            {
                var tableName = SetIdentityInsert<TEntity>(context);

                context.Set<TEntity>().Add(entity);

                Save(context, scope, tableName);
            }
        }

        public static void AddRangeWithId<TEntity>(this DbContext context, IEnumerable<TEntity> entities)
            where TEntity : class
        {
            using (var scope = new TransactionScope(TransactionScopeOption.RequiresNew))
            {
                var tableName = SetIdentityInsert<TEntity>(context);

                context.Set<TEntity>().AddRange(entities);

                Save(context, scope, tableName);
            }
        }

        private static string SetIdentityInsert<TEntity>(DbContext context)
            where TEntity : class
        {
            var tableName = context.GetTableName<TEntity>();

            context.Database.ExecuteSqlCommand($"SET IDENTITY_INSERT {tableName} ON");

            return tableName;
        }

        private static void Save(DbContext context, TransactionScope scope, string tableName)
        {
            context.SaveChanges();

            context.Database.ExecuteSqlCommand($"SET IDENTITY_INSERT {tableName} OFF");
            scope.Complete();
        }

        private static string GetTableName<T>(this DbContext context) where T : class
        {
            var objectContext = ((IObjectContextAdapter)context).ObjectContext;

            return objectContext.GetTableName<T>();
        }

        private static string GetTableName<T>(this ObjectContext context) where T : class
        {
            var sql = context.CreateObjectSet<T>().ToTraceString();
            var regex = new Regex("FROM (?<table>.*) AS");
            var match = regex.Match(sql);

            var table = match.Groups["table"].Value;
            return table;
        }
    }
}