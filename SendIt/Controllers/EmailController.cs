using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SendIt.Dto;
using SendIt.Repo;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

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
        public async Task<IActionResult> SendEmail(string receptor, string subject, string body)
        {
            await emailRepo.SendEmail(receptor, subject, body);
            return Ok();
        }


    }
}
