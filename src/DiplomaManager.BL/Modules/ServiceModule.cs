using Autofac;
using DiplomaManager.BLL.Interfaces;
using DiplomaManager.BLL.Services;

namespace DiplomaManager.BLL.Modules
{
    public class ServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<StudentProjectService>().As<IStudentProjectService>();
            builder.RegisterType<UserService>().As<IUserService>();
            builder.RegisterType<TeacherProjectService>().As<ITeacherProjectService>();
            builder.RegisterType<ImportService>().As<IImportService>();
            builder.RegisterType<TranslationService>().As<ITranslationService>();
            builder.RegisterType<DistributionService>().As<IDistributionService>();
        }
    }
}
