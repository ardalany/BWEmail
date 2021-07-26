namespace BWEmail.Api.Services.Clients {
    public class SpendgridRequest {
        public string Sender { get; set; }
        public string Recipient { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}