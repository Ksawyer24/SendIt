using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;
using MimeKit.Text;
using SendIt.Dto;
using System;
using System.Threading.Tasks;

namespace SendIt.Repo
{
    public class EmailRepo : IEmailRepo
    {
        private readonly IConfiguration config;

        public EmailRepo(IConfiguration config)
        {
            this.config = config;
        }

        public async Task SendEmailAsync(EmailDto request)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(config.GetValue<string>("EmailUsername")));
            email.To.Add(MailboxAddress.Parse(request.To));
            email.Subject = request.Subject;
            email.Body = new TextPart(TextFormat.Html) { Text = request.Body };

            using var smtp = new SmtpClient();
            try
            {
                await smtp.ConnectAsync(config.GetValue<string>("EmailHost"), 587, SecureSocketOptions.StartTls);
                await smtp.AuthenticateAsync(config.GetValue<string>("EmailUsername"), config.GetValue<string>("EmailPassword"));
                await smtp.SendAsync(email);
            }
            catch (Exception ex)
            {
                // Log the exception (replace with your logging solution)
                Console.WriteLine($"Error sending email: {ex.Message}");
                throw; // Rethrow to handle it further up the chain if needed
            }
            finally
            {
                await smtp.DisconnectAsync(true);
            }
        }
    }
}
