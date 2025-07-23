namespace Hospital_Management.Services.IServices
{
    public interface IEmailServices
    {

        Task SendEmailAsync(String toEmail, string subject, string body);
    }
}
