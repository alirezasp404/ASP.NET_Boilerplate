using System;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using MyProject.Courses;

namespace MyProject.Courses.Dto
{
    [AutoMapFrom(typeof(Course))]
    [AutoMapTo(typeof(Course))]
    public class CourseResponseDto : EntityDto
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
    }
}
