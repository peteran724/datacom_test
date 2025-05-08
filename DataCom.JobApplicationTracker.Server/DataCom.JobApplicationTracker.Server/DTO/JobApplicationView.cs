namespace DataCom.JobApplicationTracker.Server.DTO
{
    public class JobApplicationView
    {
        public Guid Id { get; set; }
        public string CompanyName { get; set; }
        public string Position { get; set; }
        public ApplicationStatusView Status { get; set; }
        public DateTime DateApplied { get; set; }
    }
}
