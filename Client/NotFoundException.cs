using System;
using System.Net;

namespace Client
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string message, WebException innerException)
            : base(message, innerException)
        {
        }

        public NotFoundException(WebException innerException)
            : base(innerException.Message, innerException)
        {
        }
    }
}