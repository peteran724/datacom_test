using DataCom.JobApplicationTracker.Model;

namespace DataCom.JobApplicationTracker.Contract
{
    public interface IJobApplicationService
    {
        Task<List<JobApplication>> GetAllAsync();
        Task<JobApplication?> GetAsync(Guid id);
        Task<(bool, string)> AddAsync(JobApplication application);
        Task<(bool, string)> EditStatusAsync(Guid id, ApplicationStatus status);

        Task<JobApplicationPagedList> GetPagedListAsync(int pageNumber, int pageSize);
    }
}
