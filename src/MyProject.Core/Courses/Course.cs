using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using MyProject.StudentCourses;

namespace MyProject.Courses
{
    [Table("Courses")]
    public class Course : FullAuditedEntity
    {
        [Required]
        [StringLength(256)]
        public string Title { get; set; }

        [StringLength(2000)]
        public string Description { get; set; }

        public int Credits { get; set; }

        [StringLength(50)]
        public string Code { get; set; }
        
        public DateTime? StartDate { get; set; }
        
        public DateTime? EndDate { get; set; }
        
        public decimal? Price { get; set; }
        
        public bool IsActive { get; set; }
        public virtual ICollection<StudentCourse> StudentCourses { get; set; }

        
    }
}
