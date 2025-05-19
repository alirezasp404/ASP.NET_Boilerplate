using AutoMapper;

namespace MyProject.StudentCourses.Dto;

public class StudentMappingProfile : Profile
{
    public StudentMappingProfile()
    {
        CreateMap<StudentCourse, StudentEnrollmentResponseDto>()
            .ForMember(dest => dest.CourseTitle, opt => opt.MapFrom(src => src.Course.Title))
            .ForMember(dest => dest.StudentName, opt => opt.MapFrom(src => $"{src.Student.FirstName} {src.Student.LastName}"));
    }
}