using DiplomaManager.BLL.Interfaces;
using DiplomaManager.BLL.Utils;
using DiplomaManager.Modules;
using NUnit.Framework;

namespace DiplomaManager.Tests
{
    [TestFixture]
    public class EmailServiceTests
    {
        private IEmailService _emailService;

        [SetUp]
        public void Setup()
        {
            var cfg = new ConfigurationModule.SmtpConfiguration
            {
                AuthorName = "TestAuthor",
                Host = "smtp.meta.ua",
                Port = 465,
                Email = "teland94@meta.ua",
                Password = "123432Tt",
                EnableSsl = true
            };
            _emailService = new EmailService(cfg);
        }

        [Test]
        public void SendEmailAsyncTest()
        {
            Assert.DoesNotThrowAsync(() => 
                _emailService.SendEmailAsync("teland94@mail.ru", "Test", "TEST!"));
        }
    }
}
