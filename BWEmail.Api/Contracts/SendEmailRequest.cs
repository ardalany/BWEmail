using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BWEmail.Api.Contracts {
    public class SendEmailRequest {
        [Required]
        [EmailAddress]
        public string To { get; set; }
        
        [Required]
        [JsonPropertyName("to_name")]
        public string ToName { get; set; }

        [Required]
        [EmailAddress]
        public string From { get; set; }

        [Required]
        [JsonPropertyName("from_name")]
        public string FromName { get; set; }

        [Required]
        public string Subject { get; set; }

        [Required]
        public string Body { get; set; }

    }
}