namespace CareerHub.Model
{
    public class JobApplication
    {
        public int ApplicationID { get; set; }
        public int JobID { get; set; }
        public int ApplicantID { get; set; }
        public DateTime ApplicationDate { get; set; }
        public string CoverLetter { get; set; }

        public void SubmitApplication()
        {
            Console.WriteLine($"Application ID {ApplicationID} submitted for JobID {JobID} by ApplicantID {ApplicantID}.");
        }
    }
}
