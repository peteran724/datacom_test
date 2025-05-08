using DataCom.JobApplicationTracker.Contract;
using DataCom.JobApplicationTracker.Model;
using DataCom.JobApplicationTracker.Repository;
using Microsoft.Extensions.Logging;

namespace DataCom.JobApplicationTracker.Service
{
    public class JobApplicationService : IJobApplicationService
    {
        private readonly IJobApplicationRepository _repo;
        private readonly ILogger<JobApplicationService> _logger;
        public JobApplicationService(IJobApplicationRepository reop, ILogger<JobApplicationService> logger)
        {
            _repo = reop;
            _logger = logger;
        }
        public async Task<List<JobApplication>> GetAllAsync()
        {
            List<JobApplication> list = Enumerable.Empty<JobApplication>().ToList();
            try
            {
                list = await _repo.QueryAllAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Get All JobApplications occurs error");
            }
            return list;
        }

        public async Task<JobApplication?> GetAsync(Guid id)
        {
            JobApplication? application = null;
            try
            {
                application = await _repo.QueryAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Get JobApplication with id {id} occurs error");
            }
            return application;
        }
        /// <summary>
        /// Add a job application
        /// </summary>
        /// <param name="application"></param>
        /// <returns>
        /// Item1=>is success or not
        /// Item2=>error message, if Item1 is true , Item2 is empty
        /// </returns>
        public async Task<(bool, string)> AddAsync(JobApplication application)
        {
            string errMsg = "Add JobApplication failed";
            int result = 0;
            try
            {
                result = await _repo.InsertAsync(application);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Add JobApplication occurs error");
                return (false, errMsg);
            }

            if (result != 1)
            {
                _logger.LogError("The row count of Add JobApplication result is not 1");
                return (false, errMsg);
            }

            return (true, string.Empty);
        }
        /// <summary>
        /// Edit job application status
        /// </summary>
        /// <param name="id"></param>
        /// <param name="status"></param>
        /// <returns>
        /// Item1=>is success or not
        /// Item2=>error message, if Item1 is true , Item2 is empty
        /// </returns>
        public async Task<(bool, string)> EditStatusAsync(Guid id, ApplicationStatus status)
        {
            string errMsg = "Edit JobApplicationStatus failed";
            JobApplication? application = null;

            try
            {
                application = await _repo.QueryAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Get JobApplication with id {id} occurs error");
                return (false, errMsg);
            }

            if (application == null)
                return (false, $"JobApplication not found");

            int result = 0;
            application.Status = status;
            try
            {
                result = await _repo.UpdateAsync(application);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Edit JobApplication Status occurs error");
                return (false, errMsg);
            }
            if (result != 1)
            {
                _logger.LogError("The row count of Edit JobApplication Status result is not 1");
                return (false, errMsg);
            }

            return (true, string.Empty);
        }

        public async Task<JobApplicationPagedList> GetPagedListAsync(int pageNumber, int pageSize)
        {
            var result = new JobApplicationPagedList
            {
                Applications = Enumerable.Empty<JobApplication>().ToList(),
                TotalRecords = 0
            };
            
            try
            {
                result = await _repo.QueryPagedListAsync(pageNumber, pageSize);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Get Paged JobApplications List occurs error");
            }

            return result;
        }
    }
}
