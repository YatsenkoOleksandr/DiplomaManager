using System.Threading.Tasks;

namespace DiplomaManager.BL.Interfaces
{
    public interface IEmailService
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
