using System.Net;
using System.Net.Mail;

namespace SendIt.Repo
{
    public class EmailRepo : IEmailRepo
    {
        private readonly IConfiguration config;
        private readonly ILogger<EmailRepo> logger;

        public EmailRepo(IConfiguration config, ILogger<EmailRepo> logger)
        {
            this.config = config;
            this.logger = logger;
        }



        public async Task SendEmail(string provider, string receptor, string subject, string body)
        {
            try
            {
                // Validate if the provider exists in the configuration
                var emailSection = config.GetSection($"EMAIL_CONFIGURATION:{provider.ToUpper()}");

                // Check if the section exists for the provider
                if (!emailSection.Exists())
                {
                    logger.LogError($"Invalid email provider: {provider}");
                    throw new ArgumentException($"Invalid email provider: {provider}");
                }

                var email = config.GetValue<string>("EMAIL_CONFIGURATION:EMAIL");
                var password = config.GetValue<string>("EMAIL_CONFIGURATION:PASSWORD");
                var host = emailSection.GetValue<string>("HOST");
                var port = emailSection.GetValue<int>("PORT");

                // Create the SMTP client with the configuration values
                var smtpClient = new SmtpClient(host, port)
                {
                    EnableSsl = true,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(email, password)
                };

                var message = new MailMessage(email, receptor, subject, body);

                // Send the email asynchronously
                await smtpClient.SendMailAsync(message);

                // Log success (optional)
                logger.LogInformation($"Email sent successfully to {receptor}");

            }
            catch (ArgumentException ex)
            {
                // Log invalid provider error
                logger.LogError(ex, "Failed to send email");
                throw;  // Re-throw the exception if needed or handle it here
            }
            catch (Exception ex)
            {
                // Log other errors
                logger.LogError(ex, "Email sending failed");
                throw;  // Re-throw or handle the exception as necessary
            }
        }
    }
}
