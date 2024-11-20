using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SendIt.Dto;
using SendIt.Repo;

namespace SimpleEmailApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IEmailRepo emailRepo;

        public EmailController(IEmailRepo emailRepo)
        {
            this.emailRepo = emailRepo;
        }

        [HttpPost]
        public async Task<IActionResult> SendEmail(EmailDto request)
        {
            await emailRepo.SendEmailAsync(request);
            return Ok(new { Message = "Email sent successfully" });
        }
    }
}
