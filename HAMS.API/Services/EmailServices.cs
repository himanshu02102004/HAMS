    using Hospital_Management.Model;
    using Hospital_Management.Services.IServices;
    using Microsoft.Extensions.Options;
    using MimeKit;
    using MailKit.Net.Smtp;

    namespace Hospital_Management.Services
    {
        public class EmailServices : IEmailServices
        {

            private readonly EmailSetting _emailsetting;
            public EmailServices( IOptions <EmailSetting> emailsetting)
            {
                _emailsetting = emailsetting.Value;
            }



            public async Task SendEmailAsync(string toEmail, string subject, string body)
            {
                if (string.IsNullOrWhiteSpace(_emailsetting.FromEmail))
                    throw new ArgumentException("Sender email (FromEmail) iss not configured.");

                if (string.IsNullOrWhiteSpace(toEmail))
                    throw new ArgumentException("Recipient email (toEmail) cannot be null or empty.");

                if (!MailboxAddress.TryParse(_emailsetting.FromEmail, out var fromAddress))
                    throw new ArgumentException($"Invalid sender email format: {_emailsetting.FromEmail}");

                if (!MailboxAddress.TryParse(toEmail, out var toAddress))
                    throw new ArgumentException($"Invalid recipient email format: {toEmail}");

                var email = new MimeMessage();
                email.From.Add(fromAddress);
                email.To.Add(toAddress);
                email.Subject = subject;

                var builder = new BodyBuilder { HtmlBody = body };
                email.Body = builder.ToMessageBody();

                using var smtp = new SmtpClient();
                await smtp.ConnectAsync(_emailsetting.SmtpHost, _emailsetting.SmtpPort, MailKit.Security.SecureSocketOptions.StartTls);
                await smtp.AuthenticateAsync(_emailsetting.Username, _emailsetting.Password);
                await smtp.SendAsync(email);
                await smtp.DisconnectAsync(true);

           

            }

        }
    }
