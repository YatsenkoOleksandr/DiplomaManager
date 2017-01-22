using Autofac;
using DiplomaManager.DAL.Interfaces;
using DiplomaManager.DAL.Repositories;

namespace DiplomaManager.BLL.Infrastructure
{
    public class ServiceModule : Module
    {
        private readonly string _connectionString;

        public ServiceModule(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(c => new EFUnitOfWork(_connectionString))
                .As<IUnitOfWork>()
                .InstancePerLifetimeScope();
        }
    }
}
