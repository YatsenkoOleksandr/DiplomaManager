using Autofac;
using DiplomaManager.DAL.Interfaces;
using DiplomaManager.DAL.Repositories;

namespace DiplomaManager.DAL.Modules
{
    public class DatabaseModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<EFUnitOfWork>().As<IDiplomaManagerUnitOfWork>();
        }
    }
}
