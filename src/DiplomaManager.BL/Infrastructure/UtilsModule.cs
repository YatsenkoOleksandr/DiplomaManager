using Autofac;
using DiplomaManager.BLL.Interfaces;
using DiplomaManager.BLL.Utils;

namespace DiplomaManager.BLL.Infrastructure
{
    public class UtilsModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<EmailService>()
                .As<IEmailService>()
                .InstancePerLifetimeScope();

            builder.RegisterType<TransliterationService>()
                .As<ITransliterationService>()
                .InstancePerLifetimeScope();
        }
    }
}
