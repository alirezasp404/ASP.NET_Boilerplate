using Abp.Application.Services.Dto;

namespace MyProject.Students.Dto;

public class GetStudentsRequest : PagedAndSortedResultRequestDto
{
    public string Filter { get; set; }
}
