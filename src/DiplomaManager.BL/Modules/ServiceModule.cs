using Autofac;
using DiplomaManager.BLL.Interfaces;
using DiplomaManager.BLL.Services;

namespace DiplomaManager.BLL.Modules
{
    public class ServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<StudentDiplomaRequestService>().As<IStudentDiplomaRequestService>();
            builder.RegisterType<UserService>().As<IUserService>();
            builder.RegisterType<TeacherDiplomaRequestService>().As<ITeacherDiplomaRequestService>();
            builder.RegisterType<ImportService>().As<IImportService>();
            builder.RegisterType<TranslationService>().As<ITranslationService>();
        }
    }
}
