using CareerHub.Dao;
using CareerHub.exception;
namespace CareerHub.Model
{
    public class Company
    {
        public int CompanyID { get; set; }
        public string CompanyName { get; set; }
        public string Location { get; set; }

        public void PostJob(string jobTitle, string jobDescription, string jobLocation, decimal salary, string jobType)
        {
            if (salary < 0)
            {
                throw new SalaryException();
            }

            var dbManager = new DatabaseManager();
            var job = new JobListing
            {
                CompanyID = this.CompanyID,
                JobTitle = jobTitle,
                JobDescription = jobDescription,
                JobLocation = jobLocation,
                Salary = salary,
                JobType = jobType,
                PostedDate = DateTime.Now
            };
            dbManager.InsertJobListing(job);
        }
    }
}
