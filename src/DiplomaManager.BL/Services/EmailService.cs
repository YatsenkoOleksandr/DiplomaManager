using System.Threading.Tasks;
using DiplomaManager.BLL.Interfaces;
using MailKit.Net.Smtp;
using MimeKit;

namespace DiplomaManager.BLL.Services
{
    public class EmailService : IEmailService
    {
        public async Task SendEmailAsync(string email, string subject, string message)
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("Администрация сайта", "teland94@meta.ua"));
            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = message
            };

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync("smtp.meta.ua", 465, true);
                await client.AuthenticateAsync("teland94@meta.ua", "123432Tt");
                await client.SendAsync(emailMessage);

                await client.DisconnectAsync(true);
            }
        }
    }
}