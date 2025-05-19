using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Application.Services.Dto;
using MyProject.Courses.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyProject.Courses
{
    public interface ICourseAppService : IApplicationService
    {
        /// <summary>
        /// Gets a paged list of courses based on input
        /// </summary>
        Task<PagedResultDto<CourseDto>> GetAllAsync(GetCoursesInput input);
        
        /// <summary>
        /// Gets a specific course by id
        /// </summary>
        Task<CourseDto> GetAsync(EntityDto<int> input);
        
        /// <summary>
        /// Creates a new course
        /// </summary>
        Task<CourseDto> CreateAsync(CreateCourseDto input);
        
        /// <summary>
        /// Updates an existing course
        /// </summary>
        Task<CourseDto> UpdateAsync(UpdateCourseDto input);
        
        /// <summary>
        /// Deletes a course
        /// </summary>
        Task DeleteAsync(EntityDto<int> input);
    }
}
