using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SendIt.Dto;
using SendIt.Repo;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using static Org.BouncyCastle.Math.EC.ECCurve;

namespace SimpleEmailApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IEmailRepo emailRepo;
        private readonly ILogger<EmailController> logger;
        private readonly IConfiguration configuration;

        public EmailController(IEmailRepo emailRepo, ILogger<EmailController> logger, IConfiguration configuration)
        {
            this.emailRepo = emailRepo;
            this.logger = logger;
            this.configuration = configuration;
        }


        [HttpPost]
        public async Task<IActionResult> SendEmail(string provider, string receptor, string subject, string body)
        {
            try
            {
                // Validate the provider and check if it exists in the configuration
                if (!configuration.GetSection($"EMAIL_CONFIGURATION:{provider.ToLower()}").Exists())
                    return BadRequest($"Invalid email provider: {provider} is not a valid provider");


                await emailRepo.SendEmail(provider, receptor, subject, body);

                return Ok("Email Sent");
            }

            catch (Exception ex)
            {

                logger.LogError(ex, "Email sending failed");
                return StatusCode(500, "Email sending failed!");
            }

        }

    }
}
