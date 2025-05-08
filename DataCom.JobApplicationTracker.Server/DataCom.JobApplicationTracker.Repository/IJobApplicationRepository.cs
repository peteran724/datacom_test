using DataCom.JobApplicationTracker.Model;

namespace DataCom.JobApplicationTracker.Repository
{

    public interface IJobApplicationRepository
    {
        Task<List<JobApplication>> QueryAllAsync();
        Task<JobApplication?> QueryAsync(Guid id);
        Task<int> InsertAsync(JobApplication application);
        Task<int> UpdateAsync(JobApplication application);
        Task<JobApplicationPagedList> QueryPagedListAsync(int pageNumber, int pageSizeout);
    }
}
