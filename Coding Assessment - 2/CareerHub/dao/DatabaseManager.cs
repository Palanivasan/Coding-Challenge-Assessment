using System;
using System.Collections.Generic;
using CareerHub.Model;
using CareerHub.exception;
using CareerHub.dao;
using CareerHub.Util;
using Microsoft.Data.SqlClient;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CareerHub.Dao
{
    public class DatabaseManager : IDatabaseManager
    {
        public void InitializeDatabase()
        {
            try
            {
                string createCompanyTable = @"
            IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Companies')
            BEGIN
                CREATE TABLE Companies (
                    CompanyID INT PRIMARY KEY,
                    CompanyName VARCHAR(100),
                    Location VARCHAR(100)
                );
            END";

                string createJobTable = @"
            IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Jobs')
            BEGIN
                CREATE TABLE Jobs (
                    JobID INT PRIMARY KEY,
                    CompanyID INT,
                    JobTitle VARCHAR(100),
                    JobDescription TEXT,
                    JobLocation VARCHAR(100),
                    Salary DECIMAL(15, 2),
                    JobType VARCHAR(50),
                    PostedDate DATETIME,
                    FOREIGN KEY (CompanyID) REFERENCES Companies(CompanyID)
                );
            END";

                using (var connection = DBConnUtil.GetConnectionString())
                {
                    using (var command = new SqlCommand(createCompanyTable, connection))
                    {
                        command.ExecuteNonQuery();
                    }

                    using (var command = new SqlCommand(createJobTable, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }
                Console.WriteLine("Database initialized successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error initializing database: {ex.Message}");
                throw new DatabaseConnectionException();
            }
        }

        public void InsertCompany(Company company)
        {
            try
            {
                using (var connection = DBConnUtil.GetConnectionString())
                {
                    string query = "INSERT INTO Companies (CompanyID, CompanyName, Location) VALUES (@CompanyID, @CompanyName, @Location)";

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@CompanyID", company.CompanyID);
                        command.Parameters.AddWithValue("@CompanyName", company.CompanyName);
                        command.Parameters.AddWithValue("@Location", company.Location);

                        command.ExecuteNonQuery();
                    }
                }

                Console.WriteLine($"Company {company.CompanyName} inserted successfully.");
            }
            catch (Exception ex)
            {
                throw new DatabaseConnectionException("Failed to insert company. " + ex.Message);
            }
        }


        public List<Company> GetCompanies()
        {
            var companyList = new List<Company>();

            try
            {
                string query = "SELECT * FROM Companies;";

                using (var connection = DBConnUtil.GetConnectionString())
                {
                    using (var command = new SqlCommand(query, connection))
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            companyList.Add(new Company
                            {
                                CompanyID = reader.GetInt32(0),
                                CompanyName = reader.GetString(1),
                                Location = reader.GetString(2)
                            });
                        }
                    }
                }
                return companyList;
            }
            catch (Exception ex)
            {
                throw new DatabaseConnectionException();
            }
        }

        public void InsertJobListing(JobListing job)
        {
            //try
            //{
                string query = @"
            INSERT INTO Jobs (JobID, CompanyID, JobTitle, JobDescription, JobLocation, Salary, JobType, PostedDate)
            VALUES (@JobID, @CompanyID, @JobTitle, @JobDescription, @JobLocation, @Salary, @JobType, @PostedDate);";

                using (var connection = DBConnUtil.GetConnectionString())
                {
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@JobID", job.JobID);
                        command.Parameters.AddWithValue("@CompanyID", job.CompanyID);
                        command.Parameters.AddWithValue("@JobTitle", job.JobTitle);
                        command.Parameters.AddWithValue("@JobDescription", job.JobDescription);
                        command.Parameters.AddWithValue("@JobLocation", job.JobLocation);
                        command.Parameters.AddWithValue("@Salary", job.Salary);
                        command.Parameters.AddWithValue("@JobType", job.JobType);
                        command.Parameters.AddWithValue("@PostedDate", job.PostedDate);

                        command.ExecuteNonQuery();
                    }
                }
                Console.WriteLine("Job listing inserted successfully.");
            //}
            //catch (Exception ex)
            //{
            //    throw new DatabaseConnectionException();
            //}
        }

        public List<JobListing> GetJobListings()
        {
            var jobList = new List<JobListing>();

            try
            {
                string query = "SELECT * FROM Jobs;";

                using (var connection = DBConnUtil.GetConnectionString())
                {
                    using (var command = new SqlCommand(query, connection))
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            jobList.Add(new JobListing
                            {
                                JobID = reader.GetInt32(0),
                                CompanyID = reader.GetInt32(1),
                                JobTitle = reader.GetString(2),
                                JobDescription = reader.GetString(3),
                                JobLocation = reader.GetString(4),
                                Salary = reader.GetDecimal(5),
                                JobType = reader.GetString(6),
                                PostedDate = reader.GetDateTime(7)
                            });
                        }
                    }
                }
                return jobList;
            }
            catch (Exception ex)
            {
                throw new DatabaseConnectionException();
            }
        }

        public void InsertApplicant(Applicant applicant)
        {
            try
            {
                string query = @"
            INSERT INTO Applicants (ApplicantID, FirstName, LastName, Email, Phone, Resume)
            VALUES (@ApplicantID, @FirstName, @LastName, @Email, @Phone, @Resume);";

                using (var connection = DBConnUtil.GetConnectionString())
                {
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ApplicantID", applicant.ApplicantID);
                        command.Parameters.AddWithValue("@FirstName", applicant.FirstName);
                        command.Parameters.AddWithValue("@LastName", applicant.LastName);
                        command.Parameters.AddWithValue("@Email", applicant.Email);
                        command.Parameters.AddWithValue("@Phone", applicant.Phone);
                        command.Parameters.AddWithValue("@Resume", applicant.Resume);

                        command.ExecuteNonQuery();
                    }
                }
                Console.WriteLine($"Applicant {applicant.FirstName} {applicant.LastName} inserted successfully.");
            }
            catch (Exception ex)
            {
                throw new DatabaseConnectionException();
            }
        }

        public List<Applicant> GetApplicants()
        {
            var applicantList = new List<Applicant>();

            try
            {
                string query = "SELECT * FROM Applicants;";

                using (var connection = DBConnUtil.GetConnectionString())
                {
                    using (var command = new SqlCommand(query, connection))
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            applicantList.Add(new Applicant
                            {
                                ApplicantID = reader.GetInt32(0),
                                FirstName = reader.GetString(1),
                                LastName = reader.GetString(2),
                                Email = reader.GetString(3),
                                Phone = reader.GetString(4),
                                Resume = reader.GetString(5)
                            });
                        }
                    }
                }
                return applicantList;
            }
            catch (Exception ex)
            {
                throw new DatabaseConnectionException();
            }
        }

        public void InsertJobApplication(JobApplication application)
        {
            try
            {
                string query = @"
            INSERT INTO Applications (ApplicationID, JobID, ApplicantID, ApplicationDate, CoverLetter)
            VALUES (@ApplicationID, @JobID, @ApplicantID, @ApplicationDate, @CoverLetter);";

                using (var connection = DBConnUtil.GetConnectionString())
                {
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ApplicationID", application.ApplicationID);
                        command.Parameters.AddWithValue("@JobID", application.JobID);
                        command.Parameters.AddWithValue("@ApplicantID", application.ApplicantID);
                        command.Parameters.AddWithValue("@ApplicationDate", application.ApplicationDate);
                        command.Parameters.AddWithValue("@CoverLetter", application.CoverLetter);

                        command.ExecuteNonQuery();
                    }
                }
                application.SubmitApplication();
            }
            catch (Exception ex)
            {
                throw new DatabaseConnectionException();
            }
        }

        public List<JobApplication> GetApplicationsForJob(int jobID)
        {
            var applicationList = new List<JobApplication>();

            try
            {
                string query = "SELECT * FROM Applications WHERE JobID = @JobID;";

                using (var connection = DBConnUtil.GetConnectionString())
                {
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@JobID", jobID);

                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                applicationList.Add(new JobApplication
                                {
                                    ApplicationID = reader.GetInt32(0),
                                    JobID = reader.GetInt32(1),
                                    ApplicantID = reader.GetInt32(2),
                                    ApplicationDate = reader.GetDateTime(3),
                                    CoverLetter = reader.GetString(4)
                                });
                            }
                        }
                    }
                }
                return applicationList;
            }
            catch (Exception ex)
            {
                throw new DatabaseConnectionException();
            }
        }

        public List<JobListing> GetJobsBySalaryRange(decimal minSalary, decimal maxSalary)
        {
            var jobList = new List<JobListing>();

            try
            {
                string query = "SELECT * FROM Jobs WHERE Salary BETWEEN @MinSalary AND @MaxSalary;";

                using (var connection = DBConnUtil.GetConnectionString())
                {
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@MinSalary", minSalary);
                        command.Parameters.AddWithValue("@MaxSalary", maxSalary);

                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                jobList.Add(new JobListing
                                {
                                    JobID = reader.GetInt32(0),
                                    CompanyID = reader.GetInt32(1),
                                    JobTitle = reader.GetString(2),
                                    JobDescription = reader.GetString(3),
                                    JobLocation = reader.GetString(4),
                                    Salary = reader.GetDecimal(5),
                                    JobType = reader.GetString(6),
                                    PostedDate = reader.GetDateTime(7)
                                });
                            }
                        }
                    }
                }
                return jobList;
            }
            catch (Exception ex)
            {
                Console.WriteLine("ACTUAL ERROR: " + ex.Message);
                throw new DatabaseConnectionException();
            }
        }

        public List<JobListing> GetJobsForCompany(int companyID)
        {
            var jobList = new List<JobListing>();

            string query = "SELECT * FROM Jobs WHERE CompanyID = @CompanyID";

            using (var connection = DBConnUtil.GetConnectionString())
            {
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@CompanyID", companyID);

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            jobList.Add(new JobListing
                            {
                                JobID = reader.GetInt32(0),
                                CompanyID = reader.GetInt32(1),
                                JobTitle = reader.GetString(2),
                                JobDescription = reader.GetString(3),
                                JobLocation = reader.GetString(4),
                                Salary = reader.GetDecimal(5),
                                JobType = reader.GetString(6),
                                PostedDate = reader.GetDateTime(7)
                            });
                        }
                    }
                }
            }
            return jobList;
        }

        public void AddJobApplication(int jobID, int applicantID, string coverLetter)
        {
            try
            {
                using (var conn = DBConnUtil.GetConnectionString())
                {
                    string query = "INSERT INTO Applications (JobID, ApplicantID, CoverLetter, ApplicationDate) " +
                                   "VALUES (@JobID, @ApplicantID, @CoverLetter, @ApplicationDate)";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@JobID", jobID);
                        cmd.Parameters.AddWithValue("@ApplicantID", applicantID);
                        cmd.Parameters.AddWithValue("@CoverLetter", coverLetter);
                        cmd.Parameters.AddWithValue("@ApplicationDate", DateTime.Now);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            Console.WriteLine("Application successfully submitted.");
                        }
                        else
                        {
                            Console.WriteLine("Failed to submit application.");
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error: " + ex.Message);
                throw new DatabaseConnectionException("Failed to connect to the database.", ex);
            }
        }

    }
}
