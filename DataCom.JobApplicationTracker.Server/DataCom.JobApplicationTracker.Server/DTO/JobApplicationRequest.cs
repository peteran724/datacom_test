using System.ComponentModel.DataAnnotations;

namespace DataCom.JobApplicationTracker.Server.DTO
{
    public class JobApplicationRequest
    {
        [Required]
        public string CompanyName { get; set; }
        [Required]
        public string Position { get; set; }
    }
}
