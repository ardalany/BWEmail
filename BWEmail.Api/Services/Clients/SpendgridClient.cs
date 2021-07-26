using System.Net.Http;
using BWEmail.Api.Contracts;
using System.Text.Json;
using System.Text;
using System.Threading.Tasks;

namespace BWEmail.Api.Services.Clients {
    public class SpendgridClient : IEmailClient {
        private readonly HttpClient _httpClient;

        public SpendgridClient(HttpClient httpClient) {
            _httpClient = httpClient;
        }

        public async Task Send(SendEmailRequest request) {
            SpendgridRequest email = new SpendgridRequest() {
                Sender = $"{request.FromName} <{request.From}>",
                Recipient = $"{request.ToName} <{request.To}>",
                Subject = request.Subject,
                Body = request.Body
            };

            JsonSerializerOptions serializeOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            string bodyJson = JsonSerializer.Serialize(email, serializeOptions);

            StringContent postBody = new StringContent(bodyJson, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PostAsync("send_email", postBody);
            // Throw exception if status code is not in the 200-299 range
            response.EnsureSuccessStatusCode();
        }
    }
}