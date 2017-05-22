using Autofac;
using DiplomaManager.BLL.Interfaces;
using DiplomaManager.BLL.Interfaces.ProjectService;
using DiplomaManager.BLL.Services;
using DiplomaManager.BLL.Services.ProjectService;

namespace DiplomaManager.BLL.Modules
{
    public class ServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<StudentDiplomaRequestService>().As<IStudentDiplomaRequestService>();
            builder.RegisterType<UserService>().As<IUserService>();
            builder.RegisterType<TeacherDiplomaRequestService>().As<ITeacherDiplomaRequestService>();
            builder.RegisterType<AdminService>().As<IAdminService>();
            builder.RegisterType<AdminProjectService>().As<IAdminProjectService>();
            builder.RegisterType<ImportService>().As<IImportService>();
            builder.RegisterType<TranslationService>().As<ITranslationService>();
        }
    }
}
