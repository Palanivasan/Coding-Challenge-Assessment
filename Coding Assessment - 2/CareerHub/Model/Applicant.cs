using CareerHub.Dao;
using CareerHub.exception;  

namespace CareerHub.Model
{
    public class Applicant
    {
        public int ApplicantID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Resume { get; set; }

        public void CreateProfile(string email, string firstName, string lastName, string phone)
        {
            if (!IsValidEmail(email))
            {
                throw new InvalidEmailException();
            }

            var dbManager = new DatabaseManager();
            this.Email = email;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Phone = phone;
            dbManager.InsertApplicant(this);
        }

        public void ApplyForJob(int jobID, string coverLetter)
        {
            var dbManager = new DatabaseManager();
            var application = new JobApplication
            {
                JobID = jobID,
                ApplicantID = this.ApplicantID,
                ApplicationDate = DateTime.Now,
                CoverLetter = coverLetter
            };
            dbManager.InsertJobApplication(application);
        }

        public bool IsValidEmail(string email)
        {
            return email.Contains("@");
        }
    }
}
