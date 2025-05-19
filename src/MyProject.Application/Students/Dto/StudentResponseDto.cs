using System;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace MyProject.Students.Dto;

[AutoMapFrom(typeof(Student))]
public class StudentResponseDto : EntityDto
{
    public string FirstName { get; set; }
        
    public string LastName { get; set; }
        
    public string Email { get; set; }
        
    public string StudentNumber { get; set; }
        
    public DateTime? DateOfBirth { get; set; }
        
    public bool IsActive { get; set; }
        
    public string FullName => $"{FirstName} {LastName}";
}
