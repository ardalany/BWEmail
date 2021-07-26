using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using BWEmail.Api.Contracts;
using BWEmail.Api.Exceptions;

namespace BWEmail.Api.Services.Clients {
    public class SnailgunClient : IEmailClient
    {
        private readonly HttpClient _httpClient;

        public SnailgunClient(HttpClient httpClient) {
            _httpClient = httpClient;
        }
        
        public async Task Send(SendEmailRequest request)
        {
            StringContent postBody = CreatePostBody(request);

            HttpResponseMessage response = await _httpClient.PostAsync("emails", postBody);
            // Throw exception if status code is not in the 200-299 range
            response.EnsureSuccessStatusCode();
            
            // Get the email ID from response to check its status
            string jsonResponseContent = await response.Content.ReadAsStringAsync();
            SnailgunPostResponse postResponse = JsonSerializer.Deserialize<SnailgunPostResponse>(jsonResponseContent);

            // Check the status 5 times with 4 second delay in between until it is sent or failed
            int maxRetry = 5;
            for(int i = 0; i  < maxRetry; i++) {
                SnailgunGetResponse getResponse = await GetStatus(postResponse.Id);

                if(getResponse.Status == "sent") {
                    return;
                } else if(getResponse.Status == "failed") {
                    throw new EmailFailedStatusException($"Email {postResponse.Id} failed");
                } else {
                    await Task.Delay(4000);
                }
            }

            // Time out
            throw new EmailStatusTimeoutException($"Getting the status of email {postResponse.Id} timed out.");
        }

        public async Task<SnailgunGetResponse> GetStatus(string emailId) {
            HttpResponseMessage response = await _httpClient.GetAsync($"emails/{emailId}");
            // Throw exception if status code is not in the 200-299 range
            response.EnsureSuccessStatusCode();
            
            // Parse json response
            string jsonResponseContent = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<SnailgunGetResponse>(jsonResponseContent);
        }

        private StringContent CreatePostBody(SendEmailRequest request) {
            SnailgunPostRequest email = new SnailgunPostRequest() {
                FromEmail = request.From,
                FromName = request.FromName,
                ToEmail = request.To,
                ToName = request.ToName,
                Subject = request.Subject,
                Body = request.Body
            };
            string bodyJson = JsonSerializer.Serialize(email);

            return new StringContent(bodyJson, Encoding.UTF8, "application/json");
        }
    }
}