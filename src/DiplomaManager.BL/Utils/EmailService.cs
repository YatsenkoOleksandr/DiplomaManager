using System.Threading.Tasks;
using DiplomaManager.BLL.Configuration;
using DiplomaManager.BLL.Interfaces;
using MailKit.Net.Smtp;
using MimeKit;

namespace DiplomaManager.BLL.Utils
{
    public class EmailService : IEmailService
    {
        private readonly ISmtpConfiguration _configuration;

        public EmailService(ISmtpConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmailAsync(string email, string subject, string message)
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress(_configuration.AuthorName, _configuration.Email));
            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = message
            };

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync(_configuration.Host, _configuration.Port, _configuration.EnableSsl);
                await client.AuthenticateAsync(_configuration.Email, _configuration.Password);
                await client.SendAsync(emailMessage);

                await client.DisconnectAsync(true);
            }
        }
    }
}