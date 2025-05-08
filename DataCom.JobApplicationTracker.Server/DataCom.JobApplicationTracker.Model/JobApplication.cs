namespace DataCom.JobApplicationTracker.Model
{

    public class JobApplication
    {
        public Guid Id { get; set; }= Guid.NewGuid();
        public string CompanyName { get; set; }
        public string Position { get; set; }
        public ApplicationStatus Status { get; set; }=ApplicationStatus.Applied;
        public DateTime DateApplied { get; set; } = DateTime.UtcNow;
    }
}
