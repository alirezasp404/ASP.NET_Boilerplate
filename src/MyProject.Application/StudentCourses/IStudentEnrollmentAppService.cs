using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using MyProject.StudentCourses.Dto;

namespace MyProject.StudentCourses;

public interface IStudentEnrollmentAppService : IApplicationService
{
    Task<StudentEnrollmentResponseDto> EnrollStudent(EnrollStudentRequestDto input);
    Task<List<StudentEnrollmentResponseDto>> GetStudentEnrollments(int studentId);
    public  Task<List<StudentEnrollmentResponseDto>> GetMyCourses();
    public Task<StudentEnrollmentResponseDto> SelfEnroll(int courseId);

}