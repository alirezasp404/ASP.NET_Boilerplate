using System;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using MyProject.Courses;
using MyProject.Students;

namespace MyProject.StudentCourses;

[Table("StudentCourses")]
public class StudentCourse : CreationAuditedEntity
{
    public int StudentId { get; set; }
    public int CourseId { get; set; }

    [ForeignKey("StudentId")]
    public virtual Student Student { get; set; }

    [ForeignKey("CourseId")]
    public virtual Course Course { get; set; }

    public decimal? Grade { get; set; }
    public DateTime? EnrollmentDate { get; set; }
    public bool IsActive { get; set; }
    public CourseCompletionStatus CompletionStatus { get; set; }

}