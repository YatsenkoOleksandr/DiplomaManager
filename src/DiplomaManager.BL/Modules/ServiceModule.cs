using Autofac;
using DiplomaManager.BLL.Interfaces;
using DiplomaManager.BLL.Interfaces.PredefenseService;
using DiplomaManager.BLL.Interfaces.ProjectService;
using DiplomaManager.BLL.Services;
using DiplomaManager.BLL.Services.PredefenseService;
using DiplomaManager.BLL.Services.ProjectService;

namespace DiplomaManager.BLL.Modules
{
    public class ServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<StudentProjectService>().As<IStudentProjectService>();
            builder.RegisterType<UserService>().As<IUserService>();
            builder.RegisterType<TeacherProjectService>().As<ITeacherProjectService>();
            builder.RegisterType<AdminService>().As<IAdminService>();
            builder.RegisterType<AdminProjectService>().As<IAdminProjectService>();
            builder.RegisterType<ImportService>().As<IImportService>();
            builder.RegisterType<TranslationService>().As<ITranslationService>();
            builder.RegisterType<DistributionService>().As<IDistributionService>();
            builder.RegisterType<StudentPredefenseService>().As<IStudentPredefenseService>();
            builder.RegisterType<TeacherPredefenseService>().As<ITeacherPredefenseService>();
            builder.RegisterType<AdminPredefenseService>().As<IAdminPredefenseService>();
        }
    }
}
