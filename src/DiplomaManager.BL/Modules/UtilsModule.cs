using Autofac;
using DiplomaManager.BLL.Interfaces;
using DiplomaManager.BLL.Utils;

namespace DiplomaManager.BLL.Modules
{
    public class UtilsModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<EmailService>().As<IEmailService>();
            builder.RegisterType<TransliterationService>().As<ITransliterationService>();
        }
    }
}
