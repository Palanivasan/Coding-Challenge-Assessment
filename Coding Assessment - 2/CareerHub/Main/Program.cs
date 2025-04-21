using System;
using CareerHub.Dao;
using CareerHub.Model;
using CareerHub.exception;
using CareerHub.dao;

namespace CareerHub.Main
{
    public class Program
    {
        static void Main(string[] args)
        {
            var dbManager = new DatabaseManager();
            var CbMenu = new CareerHubMenu();
            bool running = true;

            while (running)
            {
                Console.Clear();
                Console.WriteLine("- - - - CareerHub - - - -");
                Console.WriteLine("1. Insert Company");
                Console.WriteLine("2. Insert Job Listing");
                Console.WriteLine("3. Insert Applicant");
                Console.WriteLine("4. Apply for Job");
                Console.WriteLine("5. View Job Listings");
                Console.WriteLine("6. View Companies");
                Console.WriteLine("7. View Applicants");
                Console.WriteLine("8. View Applications for Job");
                Console.WriteLine("9. Initialize Database");
                Console.WriteLine("10. View All Jobs with Company Names");
                Console.WriteLine("11. View Jobs by Salary Range");
                Console.WriteLine("12. View Jobs for a Company");
                Console.WriteLine("0. Exit");
                Console.Write("Choose an option: ");

                int choice = Convert.ToInt32(Console.ReadLine());

                try
                {
                    switch (choice)
                    {
                        case 1:
                            CbMenu.InsertCompany();
                            break;
                        case 2:
                            CbMenu.InsertJobListing();
                            break;
                        case 3:
                            CbMenu.InsertApplicant();
                            break;
                        case 4:
                            CbMenu.ApplyForJob();
                            break;
                        case 5:
                            CbMenu.ViewJobListings();
                            break;
                        case 6:
                            CbMenu.ViewCompanies();
                            break;
                        case 7:
                            CbMenu.ViewApplicants();
                            break;
                        case 8:
                            CbMenu.ViewApplicationsForJob();
                            break;
                        case 9:
                            dbManager.InitializeDatabase();
                            break;
                        case 10:
                            CbMenu.ViewAllJobsWithCompanyNames();
                            break;
                        case 11:
                            CbMenu.ViewJobsBySalaryRange();
                            break;
                        case 12:
                            CbMenu.ViewJobsForCompany();
                            break;
                        case 0:
                            running = false;
                            break;
                        default:
                            Console.WriteLine("Invalid option. Please try again.");
                            break;
                    }
                }
                catch (InvalidEmailException ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
                catch (SalaryException ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
                catch (FileUploadException ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
                catch (DeadlineException ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
                catch (DatabaseConnectionException ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }

                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
            }
        }
    }
}

    
