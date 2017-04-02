using Autofac;
using DiplomaManager.Services;

namespace DiplomaManager.Modules
{
    public class IdentityModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<UserInfoService>().As<IUserInfoService>().SingleInstance();
        }
    }
}
