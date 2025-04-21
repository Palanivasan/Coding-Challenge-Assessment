using CareerHub.Dao;
using CareerHub.exception;
using CareerHub.Model;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerHub.dao
{
    class CareerHubMenu
    {
        public void InsertCompany()
        {
            try
            {
                Console.WriteLine("Enter Company ID:");
                int companyID = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Enter Company Name:");
                string companyName = Console.ReadLine();
                Console.WriteLine("Enter Company Location:");
                string location = Console.ReadLine();

                var company = new Company
                {
                    CompanyID = companyID,
                    CompanyName = companyName,
                    Location = location
                };

                Console.WriteLine("Enter Job Title:");
                string jobTitle = Console.ReadLine();
                Console.WriteLine("Enter Job Description:");
                string jobDescription = Console.ReadLine();
                Console.WriteLine("Enter Job Location:");
                string jobLocation = Console.ReadLine();
                Console.WriteLine("Enter Salary:");
                decimal salary = Convert.ToDecimal(Console.ReadLine());
                Console.WriteLine("Enter Job Type:");
                string jobType = Console.ReadLine();

                company.PostJob(jobTitle, jobDescription, jobLocation, salary, jobType);
            }
            catch (SalaryException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        public void InsertJobListing()
        {
            try
            {
                Console.WriteLine("Enter Job ID:");
                int jobID = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Enter Company ID:");
                int companyID = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Enter Job Title:");
                string jobTitle = Console.ReadLine();
                Console.WriteLine("Enter Job Description:");
                string jobDescription = Console.ReadLine();
                Console.WriteLine("Enter Job Location:");
                string jobLocation = Console.ReadLine();
                Console.WriteLine("Enter Salary:");
                decimal salary = Convert.ToDecimal(Console.ReadLine());

                if (salary < 0)
                {
                    throw new SalaryException();
                }

                Console.WriteLine("Enter Job Type (Full-time, Part-time, etc.):");
                string jobType = Console.ReadLine();
                Console.WriteLine("Enter Posted Date (yyyy-mm-dd):");
                DateTime postedDate = Convert.ToDateTime(Console.ReadLine());

                var job = new JobListing
                {
                    JobID = jobID,
                    CompanyID = companyID,
                    JobTitle = jobTitle,
                    JobDescription = jobDescription,
                    JobLocation = jobLocation,
                    Salary = salary,
                    JobType = jobType,
                    PostedDate = postedDate
                };

                Console.WriteLine("Enter Applicant ID to apply for this job:");
                int applicantID = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Enter Cover Letter:");
                string coverLetter = Console.ReadLine();

                job.Apply(applicantID, coverLetter);
            }
            catch (SalaryException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        public void InsertApplicant()
        {
            try
            {
                Console.WriteLine("Enter Applicant ID:");
                int applicantID = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Enter First Name:");
                string firstName = Console.ReadLine();
                Console.WriteLine("Enter Last Name:");
                string lastName = Console.ReadLine();
                Console.WriteLine("Enter Email:");
                string email = Console.ReadLine();

                if (!IsValidEmail(email))
                {
                    throw new InvalidEmailException();
                }

                Console.WriteLine("Enter Phone:");
                string phone = Console.ReadLine();
                Console.WriteLine("Enter Resume (file name):");
                string resume = Console.ReadLine();

                var applicant = new Applicant
                {
                    ApplicantID = applicantID,
                    FirstName = firstName,
                    LastName = lastName,
                    Email = email,
                    Phone = phone,
                    Resume = resume
                };

                applicant.CreateProfile(email, firstName, lastName, phone);
            }
            catch (InvalidEmailException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        public void ApplyForJob()
        {
            try
            {
                Console.WriteLine("Enter Job ID to apply for:");
                int jobID = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Enter Applicant ID:");
                int applicantID = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Enter Cover Letter:");
                string coverLetter = Console.ReadLine();

                DateTime applicationDeadline = new DateTime(2025, 12, 31);

                if (DateTime.Now > applicationDeadline)
                {
                    throw new DeadlineException();
                }

                DatabaseManager dbManager = new DatabaseManager();
                dbManager.AddJobApplication(jobID, applicantID, coverLetter);
            }
            catch (DeadlineException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error: " + ex.Message);
                throw new DatabaseConnectionException("Failed to connect to the database.", ex);
            }
        }


        public void ViewJobListings()
        {
            var dbManager = new DatabaseManager();
            var jobListings = dbManager.GetJobListings();
            foreach (var job in jobListings)
            {
                Console.WriteLine($"Job ID: {job.JobID}, Title: {job.JobTitle}, Salary: {job.Salary}, Posted on: {job.PostedDate}");
            }
        }

        public void ViewCompanies()
        {
            var dbManager = new DatabaseManager();
            var companies = dbManager.GetCompanies();
            foreach (var company in companies)
            {
                Console.WriteLine($"Company ID: {company.CompanyID}, Name: {company.CompanyName}, Location: {company.Location}");
            }
        }

        public void ViewApplicants()
        {
            var dbManager = new DatabaseManager();
            var applicants = dbManager.GetApplicants();
            foreach (var applicant in applicants)
            {
                Console.WriteLine($"Applicant ID: {applicant.ApplicantID}, Name: {applicant.FirstName} {applicant.LastName}, Email: {applicant.Email}");
            }
        }

        public void ViewApplicationsForJob()
        {
            Console.WriteLine("Enter Job ID to view applications:");
            int jobID = Convert.ToInt32(Console.ReadLine());

            var dbManager = new DatabaseManager();
            var applications = dbManager.GetApplicationsForJob(jobID);
            foreach (var application in applications)
            {
                Console.WriteLine($"Application ID: {application.ApplicationID}, Applicant ID: {application.ApplicantID}, Date: {application.ApplicationDate}");
            }
        }

        public void ViewAllJobsWithCompanyNames()
        {
            var dbManager = new DatabaseManager();
            var jobs = dbManager.GetJobListings();
            foreach (var job in jobs)
            {
                var company = dbManager.GetCompanies().Find(c => c.CompanyID == job.CompanyID);
                Console.WriteLine($"Job: {job.JobTitle}, Company: {company.CompanyName}, Salary: {job.Salary}");
            }
        }

        public void ViewJobsBySalaryRange()
        {
            Console.WriteLine("Enter minimum salary:");
            decimal minSalary = Convert.ToDecimal(Console.ReadLine());
            Console.WriteLine("Enter maximum salary:");
            decimal maxSalary = Convert.ToDecimal(Console.ReadLine());

            var dbManager = new DatabaseManager();
            var jobsInRange = dbManager.GetJobsBySalaryRange(minSalary, maxSalary);
            foreach (var job in jobsInRange)
            {
                var company = dbManager.GetCompanies().Find(c => c.CompanyID == job.CompanyID);
                Console.WriteLine($"Job: {job.JobTitle}, Company: {company.CompanyName}, Salary: {job.Salary}");
            }
        }

        public void ViewJobsForCompany()
        {
            Console.WriteLine("Enter Company ID to view its jobs:");
            int companyID = Convert.ToInt32(Console.ReadLine());

            var dbManager = new DatabaseManager();
            var jobs = dbManager.GetJobsForCompany(companyID);

            if (jobs.Count == 0)
            {
                Console.WriteLine("No jobs found for this company.");
            }
            else
            {
                foreach (var job in jobs)
                {
                    Console.WriteLine($"Job ID: {job.JobID}, Title: {job.JobTitle}, Salary: {job.Salary}, Posted: {job.PostedDate}");
                }
            }
        }

        public bool IsValidEmail(string email)
        {
            return email.Contains("@");
        }
    }
}
