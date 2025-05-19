using Abp.Application.Services.Dto;

namespace MyProject.Courses.Dto
{
    public class GetCoursesRequest : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
        
        public bool? IsActive { get; set; }
    }
}
