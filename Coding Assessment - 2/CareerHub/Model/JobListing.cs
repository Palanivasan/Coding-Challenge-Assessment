using CareerHub.Dao;
using CareerHub.exception;

namespace CareerHub.Model
{
    public class JobListing
    {
        public int JobID { get; set; }
        public int CompanyID { get; set; }
        public string JobTitle { get; set; }
        public string JobDescription { get; set; }
        public string JobLocation { get; set; }
        public decimal Salary { get; set; }
        public string JobType { get; set; }
        public DateTime PostedDate { get; set; }

        public void Apply(int applicantID, string coverLetter)
        {
            if (Salary < 0)
            {
                throw new SalaryException();
            }

            var dbManager = new DatabaseManager();
            var application = new JobApplication
            {
                JobID = this.JobID,
                ApplicantID = applicantID,
                ApplicationDate = DateTime.Now,
                CoverLetter = coverLetter
            };
            dbManager.InsertJobApplication(application);
        }

       
    }
}
