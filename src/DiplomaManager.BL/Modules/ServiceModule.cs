using Autofac;
using DiplomaManager.BLL.Interfaces;
using DiplomaManager.BLL.Services;

namespace DiplomaManager.BLL.Modules
{
    public class ServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<RequestService>().As<IRequestService>();
            builder.RegisterType<UserService>().As<IUserService>();
            builder.RegisterType<TeacherService>().As<ITeacherService>();
            builder.RegisterType<AdminService>().As<IAdminService>();
        }
    }
}
