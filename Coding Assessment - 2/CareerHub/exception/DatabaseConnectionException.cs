using System;

namespace CareerHub.exception
{
    public class DatabaseConnectionException : Exception
    {
        private const string DefaultMessage = "There was an error connecting to the database.";

        public DatabaseConnectionException()
            : base(DefaultMessage) { }

        public DatabaseConnectionException(Exception innerException)
            : base(DefaultMessage, innerException) { }

        public DatabaseConnectionException(string customMessage)
            : base(customMessage) { }

        public DatabaseConnectionException(string customMessage, Exception innerException)
            : base(customMessage, innerException) { }
    }
}
