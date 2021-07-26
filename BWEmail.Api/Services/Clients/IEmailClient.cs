using System.Threading.Tasks;
using BWEmail.Api.Contracts;

namespace BWEmail.Api.Services.Clients {
    public interface IEmailClient {
        Task Send(SendEmailRequest request);
    }
}