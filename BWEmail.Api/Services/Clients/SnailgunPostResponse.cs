using System.Text.Json.Serialization;

namespace BWEmail.Api.Services.Clients {
    public class SnailgunPostResponse {
        [JsonPropertyName("id")]
        public string Id { get; set; }
    }
}