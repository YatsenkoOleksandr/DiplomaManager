using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace DiplomaManager.DAL.EF
{
    public class TemporaryDbContextFactory : IDbContextFactory<DiplomaManagerContext>
    {
        public DiplomaManagerContext Create(DbContextFactoryOptions options)
        {
            var builder = new DbContextOptionsBuilder<DiplomaManagerContext>();
            builder.UseSqlServer("Server=(local);Database=diplomamanagerdb;Trusted_Connection=True;");
            return new DiplomaManagerContext(builder.Options);
        }
    }
}
