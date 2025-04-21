using System;

namespace CareerHub.exception
{
    public class SalaryException : Exception
    {
        public SalaryException() : base("Salary cannot be negative.") { }
    }
}
