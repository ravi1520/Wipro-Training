using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using Microsoft.AspNetCore.Http;

namespace coreidentity_demo.Models
{
    public class EmailService
    {
        public async Task SendEmailAsync(MailRequest mailRequest)
        {
            var email = new MimeMessage();

            // Add sender with display name
            email.Sender = new MailboxAddress(EmailConfiguration.DisplayName, EmailConfiguration.SenderEmail);

            // Add recipient(s)
            email.To.Add(MailboxAddress.Parse(mailRequest.ToEmail));
            email.Subject = mailRequest.Subject ?? "(No Subject)";

            var builder = new BodyBuilder
            {
                HtmlBody = mailRequest.Body ?? string.Empty
            };

            // Handle attachments if any
            if (mailRequest.Attachments != null && mailRequest.Attachments.Count > 0)
            {
                foreach (var file in mailRequest.Attachments)
                {
                    if (file?.Length > 0)
                    {
                        using var ms = new MemoryStream();
                        await file.CopyToAsync(ms);
                        builder.Attachments.Add(file.FileName, ms.ToArray(), 
                            ContentType.Parse(file.ContentType));
                    }
                }
            }

            email.Body = builder.ToMessageBody();

            using var smtp = new SmtpClient();

            // SecureSocketOptions is better than just bool UseSsl
            await smtp.ConnectAsync(
                EmailConfiguration.Host,
                EmailConfiguration.Port,
                EmailConfiguration.UseSsl ? SecureSocketOptions.StartTls : SecureSocketOptions.Auto);

            await smtp.AuthenticateAsync(EmailConfiguration.SenderEmail, EmailConfiguration.Password);
            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);
        }
    }

    public class MailRequest
    {
        public string ToEmail { get; set; } = string.Empty;
        public string Subject { get; set; } = string.Empty;
        public string Body { get; set; } = string.Empty;
        public List<IFormFile>? Attachments { get; set; }
    }
}
