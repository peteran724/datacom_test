using DataCom.JobApplicationTracker.Model;
using Microsoft.EntityFrameworkCore;

namespace DataCom.JobApplicationTracker.Repository
{
    public class JobApplicationRepository : IJobApplicationRepository
    {
        private readonly JobApplicationDbContext _context;
        public JobApplicationRepository(JobApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<JobApplication>> QueryAllAsync()
        {
            return await _context.JobApplications.ToListAsync();
        }

        public async Task<JobApplication?> QueryAsync(Guid id)
        {
            return await _context.JobApplications.SingleOrDefaultAsync(ja => ja.Id == id);
        }


        public async Task<int> InsertAsync(JobApplication application)
        {
            await _context.JobApplications.AddAsync(application);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> UpdateAsync(JobApplication application)
        {
            _context.JobApplications.Update(application);
            return await _context.SaveChangesAsync();
        }

        public async Task<JobApplicationPagedList> QueryPagedListAsync(int pageNumber, int pageSize)
        {
            var query = _context.JobApplications.OrderByDescending(a => a.DateApplied);

            var totalRecords = await query.CountAsync();

            var applications = await query.Skip((pageNumber - 1) * pageSize)
                                   .Take(pageSize)
                                   .ToListAsync();

            return new JobApplicationPagedList
            {
                Applications = applications,
                TotalRecords = totalRecords
            };
        }

    }
}
