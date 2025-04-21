using System;

namespace CareerHub.exception
{
    public class DeadlineException : Exception
    {
        public DeadlineException() : base("The application deadline has passed.") { }
    }
}
