using System;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;

namespace MyProject.Students.Dto;

public class UpdateStudentRequestDto : EntityDto
{
    public int Id { get; set; }
    [Required]
    [StringLength(50)]
    public string FirstName { get; set; }
        
    [Required]
    [StringLength(50)]
    public string LastName { get; set; }
        
    [StringLength(100)]
    public string Email { get; set; }
        
    [StringLength(20)]
    public string StudentNumber { get; set; }
        
    public DateTime? DateOfBirth { get; set; }
        
    public bool IsActive { get; set; }
}