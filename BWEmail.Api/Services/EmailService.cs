using BWEmail.Api.Contracts;
using BWEmail.Api.Services.Clients;

namespace BWEmail.Api.Services {
    public class EmailService {
        private readonly IEmailClient _emailClient;

        public EmailService(IEmailClient emailClient) {
            _emailClient = emailClient;
        }

        public void SendEmail(SendEmailRequest request) {
            _emailClient.Send(request);
        }
    }
}