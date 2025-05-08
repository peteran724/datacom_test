using AutoMapper;
using DataCom.JobApplicationTracker.Model;
using DataCom.JobApplicationTracker.Server.DTO;

namespace DataCom.JobApplicationTracker.Server.Mapper
{
    public class AutomapperConfig : Profile
    {
        public AutomapperConfig()
        {
            CreateMap<ApplicationStatus, ApplicationStatusView>();
            CreateMap<ApplicationStatusRequest, ApplicationStatus>();
            CreateMap<JobApplicationRequest, JobApplication>();
            CreateMap<JobApplication, JobApplicationView>()
                .ForMember(d => d.DateApplied, s => s.MapFrom(a=> a.DateApplied.Date));
        }
    }
}
