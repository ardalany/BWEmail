using System.Text.Json.Serialization;

namespace BWEmail.Api.Contracts {
    public class SendEmailRequest {
        public string To { get; set; }
        
        [JsonPropertyName("to_name")]
        public string ToName { get; set; }
        public string From { get; set; }

        [JsonPropertyName("from_name")]
        public string FromName { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }

    }
}