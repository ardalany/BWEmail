using System;

namespace BWEmail.Api.Exceptions {
    public class EmailFailedStatusException : Exception {
        public EmailFailedStatusException(string message)
            : base(message)
        {
        }

        public EmailFailedStatusException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}