using System;

namespace BWEmail.Api.Exceptions {
    public class EmailStatusTimeoutException : Exception {
        public EmailStatusTimeoutException(string message)
            : base(message)
        {
        }

        public EmailStatusTimeoutException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}