using System;

namespace CareerHub.exception
{
    public class FileUploadException : Exception
    {
        public FileUploadException() : base("There was an error during the file upload.") { }
    }
}
