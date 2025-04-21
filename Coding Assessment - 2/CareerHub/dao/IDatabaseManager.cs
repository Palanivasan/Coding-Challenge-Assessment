using CareerHub.Model;

namespace CareerHub.dao
{
    public interface IDatabaseManager
    {
        void InitializeDatabase();
        void InsertCompany(Company company);
        List<Company> GetCompanies();
        void InsertJobListing(JobListing job);
        List<JobListing> GetJobListings();
        void InsertApplicant(Applicant applicant);
        List<Applicant> GetApplicants();
        void InsertJobApplication(JobApplication application);
        List<JobApplication> GetApplicationsForJob(int jobID);
    }
}
