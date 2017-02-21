namespace DiplomaManager.BLL.Configuration
{
    public interface ISmtpConfiguration
    {
        string Host { get; set; }
        int Port { get; set; }
        string Email { get; set; }
        string Password { get; set; }
        bool EnableSsl { get; set; }
        string AuthorName { get; set; }
    }
}
