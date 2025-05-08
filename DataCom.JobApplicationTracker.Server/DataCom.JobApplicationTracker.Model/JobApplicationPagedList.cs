namespace DataCom.JobApplicationTracker.Model
{

    public class JobApplicationPagedList
    {
        public List<JobApplication> Applications { get; set; }
        public int TotalRecords { get; set; }
    }
}
