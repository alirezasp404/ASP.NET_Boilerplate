using System;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace MyProject.StudentCourses.Dto;

[AutoMapFrom(typeof(StudentCourse))]
public class StudentEnrollmentResponseDto : EntityDto
{
    public int StudentId { get; set; }
        
    public string StudentName { get; set; }
        
    public int CourseId { get; set; }
        
    public string CourseTitle { get; set; }
        
    public DateTime EnrollmentDate { get; set; }
        
    public bool IsActive { get; set; }
        
    public decimal? Grade { get; set; }
        
    public CourseCompletionStatus CompletionStatus { get; set; }
}
