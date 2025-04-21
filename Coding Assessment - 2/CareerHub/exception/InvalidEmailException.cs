using System;

namespace CareerHub.exception
{
    public class InvalidEmailException : Exception
    {
        public InvalidEmailException() : base("The email address provided is invalid.") { }
    }
}
