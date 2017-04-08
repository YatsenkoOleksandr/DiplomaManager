using System.Collections.Generic;
using Autofac;
using DiplomaManager.BLL.Configuration;
using DiplomaManager.DAL.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace DiplomaManager.Modules
{
    public class ConfigurationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(CreateSmtpConfiguration).SingleInstance();
            builder.Register(CreateAppConfiguration).SingleInstance();
            builder.Register(CreateDatabaseConnectionConfiguration).SingleInstance();
            builder.Register(CreateTranslationConfiguration).SingleInstance();
        }

        private static ISmtpConfiguration CreateSmtpConfiguration(IComponentContext context)
        {
            var configuration = context.Resolve<IConfiguration>();
            
            var result = new SmtpConfiguration();

            new ConfigureFromConfigurationOptions<ISmtpConfiguration>(configuration.GetSection("Smtp"))
                .Configure(result);

            return result;
        }

        private static ILocaleConfiguration CreateAppConfiguration(IComponentContext context)
        {
            var configuration = context.Resolve<IConfiguration>();

            var result = new CultureConfiguration();

            new ConfigureFromConfigurationOptions<ILocaleConfiguration>(configuration.GetSection("Locale"))
                .Configure(result);

            return result;
        }

        private static IDatabaseConnectionConfiguration CreateDatabaseConnectionConfiguration(IComponentContext context)
        {
            var configuration = context.Resolve<IConfiguration>();

            var result = new DatabaseConnectionConfiguration();

            new ConfigureFromConfigurationOptions<DatabaseConnectionConfiguration>(configuration.GetSection("Data"))
                .Configure(result);

            return result;
        }

        private static ITranslationConfiguration CreateTranslationConfiguration(IComponentContext context)
        {
            var configuration = context.Resolve<IConfiguration>();

            var result = new TranslationConfiguration();

            new ConfigureFromConfigurationOptions<TranslationConfiguration>(configuration.GetSection("Translation"))
                .Configure(result);

            return result;
        }

        #region Nested Class

        public class DatabaseConnectionConfiguration : IDatabaseConnectionConfiguration
        {
            public string ConnectionString { get; set; }
        }

        public class SmtpConfiguration : ISmtpConfiguration
        {
            public string Host { get; set; }
            public int Port { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
            public bool EnableSsl { get; set; }
            public string AuthorName { get; set; }
        }

        public class CultureConfiguration : ILocaleConfiguration
        {
            public IEnumerable<string> LocaleNames { get; set; }
            public string DefaultLocaleName { get; set; }
        }

        public class TranslationConfiguration : ITranslationConfiguration
        {
            public string BaseUrl { get; set; }
            public string TranslateBaseApiPath { get; set; }
            public string ApiKey { get; set; }
        }

        #endregion
    }
}
