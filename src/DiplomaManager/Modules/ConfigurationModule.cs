using Autofac;
using DiplomaManager.BLL.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace DiplomaManager.Modules
{
    public class ConfigurationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(CreateSmtpConfiguration).SingleInstance();
        }

        private static SmtpConfiguration CreateSmtpConfiguration(IComponentContext context)
        {
            var configuration = context.Resolve<IConfiguration>();

            var result = new SmtpConfiguration();

            new ConfigureFromConfigurationOptions<SmtpConfiguration>(configuration.GetSection("Smtp"))
                .Configure(result);

            return result;
        }
    }
}
