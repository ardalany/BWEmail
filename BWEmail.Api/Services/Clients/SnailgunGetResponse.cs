using System.Text.Json.Serialization;

namespace BWEmail.Api.Services.Clients {
    public class SnailgunGetResponse {
        [JsonPropertyName("status")]
        public string Status { get; set; }
    }
}