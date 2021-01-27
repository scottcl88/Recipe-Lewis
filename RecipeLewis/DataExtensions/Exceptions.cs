using System;

namespace RecipeLewis.DataExtensions
{
    public class EmailSendException : Exception
    {
        public EmailSendException(string message) : base(message)
        {
        }
    }
}