using System;
using System.ComponentModel.DataAnnotations;
using Abp.AutoMapper;

namespace MyProject.StudentCourses.Dto;

[AutoMapTo(typeof(StudentCourse))]
public class EnrollStudentRequestDto
{
    [Required]
    public int StudentId { get; set; }
        
    [Required]
    public int CourseId { get; set; }
        
    public DateTime? EnrollmentDate { get; set; } = DateTime.Now;
        
    public bool IsActive { get; set; } = true;
    
    public CourseCompletionStatus CompletionStatus { get; set; } = CourseCompletionStatus.NotStarted;

}
