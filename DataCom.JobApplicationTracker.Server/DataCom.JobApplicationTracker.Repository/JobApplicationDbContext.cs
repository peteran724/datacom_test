using DataCom.JobApplicationTracker.Model;
using Microsoft.EntityFrameworkCore;

namespace DataCom.JobApplicationTracker.Repository
{
    public class JobApplicationDbContext : DbContext
    {
        public JobApplicationDbContext(DbContextOptions<JobApplicationDbContext> options) : base(options) { }

        public DbSet<JobApplication> JobApplications { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // init data
            modelBuilder.Entity<JobApplication>().HasData(
                new JobApplication
                {
                    CompanyName = "Microsoft",
                    Position = "Senior .NET Developer"
                },
                new JobApplication
                {
                    CompanyName = "Google",
                    Position = "Senior Ruby Developer"
                },
                new JobApplication
                {
                    CompanyName = "Amazon",
                    Position = "Senior C# Developer"
                },
                new JobApplication
                {
                    CompanyName = "ASB Bank",
                    Position = "Senior JAVA Developer"
                },
                new JobApplication
                {
                    CompanyName = "Kiwi Bank",
                    Position = "Senior C++ Developer"
                }
            );
        }
    }



}
