using System;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;
using BWEmail.Api;
using BWEmail.Api.Contracts;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text;

namespace BWEmail.IntegrationTests
{
    public class EmailControllerTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;

        public EmailControllerTests(WebApplicationFactory<Startup> factory) {
            _factory = factory;
        }

        [Fact]
        public async Task EmailController_sends_email()
        {
            // Arrange
            SendEmailRequest request = new SendEmailRequest() {
                To = "hpotter@hogwartz.com",
                ToName = "Harry Potter",
                From = "dumbledore@hogwartz.com",
                FromName = "Albus Dumbledore",
                Subject = "Next class",
                Body = "Wednesday 8:30 PM my office"
            };
            HttpClient client = _factory.CreateClient();

            JsonSerializerOptions serializeOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            string bodyJson = JsonSerializer.Serialize(request, serializeOptions);
            StringContent postBody = new StringContent(bodyJson, Encoding.UTF8, "application/json");

            // Act
            var response = await client.PostAsync("/v1/emails", postBody);

            // Assert
            response.EnsureSuccessStatusCode();
        }
    }
}
