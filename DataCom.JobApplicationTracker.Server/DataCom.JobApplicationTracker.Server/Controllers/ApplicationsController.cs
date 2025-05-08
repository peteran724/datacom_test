using AutoMapper;
using DataCom.JobApplicationTracker.Contract;
using DataCom.JobApplicationTracker.Model;
using DataCom.JobApplicationTracker.Server.DTO;
using Microsoft.AspNetCore.Mvc;

namespace DataCom.JobApplicationTracker.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationsController : ControllerBase
    {
        private readonly IJobApplicationService _srv;
        private readonly IMapper _mapper;
        public ApplicationsController(IJobApplicationService srv, IMapper mapper)
        {
            _srv = srv;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetPagedList(int pageNumber, int pageSize)
        {
            var data = new List<JobApplicationView>();
            if (pageNumber > 0 && pageSize > 0)
            {
                var pagedList = await _srv.GetPagedListAsync(pageNumber, pageSize);
                pagedList.Applications.ForEach(lx => data.Add(_mapper.Map<JobApplicationView>(lx)));
                var result = new PagedResponse<JobApplicationView>
                {
                    Data = data,
                    PageNumber = pageNumber,
                    PageSize = pageSize,
                    TotalPages = (int)Math.Ceiling(pagedList.TotalRecords / (double)pageSize),
                    TotalRecords = pagedList.TotalRecords
                };
                return Ok(result);
            }
            else
            {
                var list = await _srv.GetAllAsync();
                list.ForEach(lx => { data.Add(_mapper.Map<JobApplicationView>(lx)); });
                return Ok(data);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var application = await _srv.GetAsync(id);
            var result = _mapper.Map<JobApplicationView>(application);
            return Ok(result);
        }


        [HttpPost]
        public async Task<IActionResult> Add([FromBody] JobApplicationRequest request)
        {
            var application = _mapper.Map<JobApplication>(request);
            var result = await _srv.AddAsync(application);
            return Ok(new JobApplicationResponse { Success = result.Item1, ErrorMessage = result.Item2, Id = result.Item1 ? application.Id : null });
        }


        [HttpPatch("{id}/status/{status}")]
        public async Task<IActionResult> Update(Guid id, ApplicationStatusRequest status)
        {
            var _status = _mapper.Map<ApplicationStatus>(status);
            var result = await _srv.EditStatusAsync(id, _status);
            return Ok(new Response { Success = result.Item1, ErrorMessage = result.Item2 });
        }
    }
}
