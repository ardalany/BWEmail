using System;
using System.Threading.Tasks;
using BWEmail.Api.Contracts;
using BWEmail.Api.Services.Clients;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BWEmail.Api.Controllers.V1 {
    [ApiController]
    [Route("v1/emails")]
    public class EmailController : ControllerBase {
        private readonly IEmailClient _emailClient;

        public EmailController(IEmailClient emailClient) {
            _emailClient = emailClient;
        }

        [HttpPost]
        public async Task<ActionResult> SendEmail(SendEmailRequest request) {
            try {
                await _emailClient.Send(request);
                return Ok();
            } catch(Exception ex) {
                // TODO: log exception details

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}