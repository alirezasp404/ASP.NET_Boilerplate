using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using MyProject.Authorization.Users;
using MyProject.StudentCourses;

namespace MyProject.Students;

[Table("Students")]
public class Student : FullAuditedEntity
{
    [Required] [StringLength(50)] public string FirstName { get; set; }

    [Required] [StringLength(50)] public string LastName { get; set; }

    [StringLength(100)] public string Email { get; set; }

    [StringLength(20)] public string StudentNumber { get; set; }

    public DateTime? DateOfBirth { get; set; }

    public bool IsActive { get; set; }

    public virtual ICollection<StudentCourse> StudentCourses { get; set; }

    [ForeignKey("UserId")] public User User { get; set; }
    public long UserId { get; set; }
}