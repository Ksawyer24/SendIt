using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SendIt.Dto;
using SendIt.Repo;

namespace SendIt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MailgunController : ControllerBase
    {
        private readonly MailgunRepo mailgunRepo;

        public MailgunController(MailgunRepo mailgunRepo)
        {
            this.mailgunRepo = mailgunRepo;
        }


        [HttpPost("send")]
        public async Task<IActionResult> SendEmail([FromBody] MailgunRequest mailgunRequest)
        {
            if (mailgunRequest == null)
            {
                return BadRequest("Invalid email data.");
            }

            // Map the MailgunRequest to an EmailRequest model
            var emailRequest = new MailgunRequest
            {
                From = mailgunRequest.From,
                To = mailgunRequest.To,
                Subject = mailgunRequest.Subject,
                Text = mailgunRequest.Text,
                Html = mailgunRequest.Html
            };

            // Call the Mailgun service to send the email
            await mailgunRepo.SendEmailAsync(emailRequest);

            return Ok("Email sent successfully.");
        }

    }
}
