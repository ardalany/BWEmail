using System.Text.Json.Serialization;

namespace BWEmail.Api.Services.Clients {
    public class SnailgunPostRequest {
        [JsonPropertyName("from_email")]
        public string FromEmail { get; set; }

        [JsonPropertyName("from_name")]
        public string FromName { get; set; }

        [JsonPropertyName("to_email")]
        public string ToEmail { get; set; }

        [JsonPropertyName("to_name")]
        public string ToName { get; set; }

        [JsonPropertyName("subject")]
        public string Subject { get; set; }

        [JsonPropertyName("body")]
        public string Body { get; set; }
    }
}