using System.Net;
using System.Net.Mail;

namespace SendIt.Repo
{
    public class EmailRepo:IEmailRepo
    {
        private readonly IConfiguration config;


        public EmailRepo(IConfiguration config)
        {
            this.config = config;

        }

        public async Task SendEmail(string receptor, string subject, string body)
        {
            var email = config.GetValue<string>("EMAIL_CONFIGURATION:EMAIL");
            var password = config.GetValue<string>("EMAIL_CONFIGURATION:PASSWORD");
            var host = config.GetValue<string>("EMAIL_CONFIGURATION:HOST");
            var port = config.GetValue<int>("EMAIL_CONFIGURATION:PORT");

            
            var smtpClient = new SmtpClient(host,port);
            smtpClient.EnableSsl = true;
            smtpClient.UseDefaultCredentials = false;

            smtpClient.Credentials = new NetworkCredential(email, password);

            var message = new MailMessage(email!,receptor, subject, body);   
           


            try
            {
                await smtpClient.SendMailAsync(message);
            }

            catch (Exception ex)
            {
                Console.WriteLine($"Email sending failed:{ ex.Message}");
            }


        }
    }
}
