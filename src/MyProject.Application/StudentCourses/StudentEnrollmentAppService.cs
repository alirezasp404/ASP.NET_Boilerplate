using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Runtime.Session;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using MyProject.Authorization;
using MyProject.Authorization.Roles;
using MyProject.Courses;
using MyProject.StudentCourses.Dto;
using MyProject.Students;

namespace MyProject.StudentCourses;

public class StudentEnrollmentAppService : ApplicationService, IStudentEnrollmentAppService
{
    private readonly IRepository<StudentCourse> _enrollmentRepository;
    private readonly IRepository<Student> _studentRepository;
    private readonly IRepository<Course> _courseRepository;
    private readonly IAbpSession _abpSession;

    public StudentEnrollmentAppService(
        IRepository<StudentCourse> enrollmentRepository,
        IRepository<Student> studentRepository,
        IRepository<Course> courseRepository,
        IAbpSession abpSession)
    {
        _enrollmentRepository = enrollmentRepository;
        _studentRepository = studentRepository;
        _courseRepository = courseRepository;
        _abpSession = abpSession;
    }

    [AbpAuthorize(PermissionNames.Pages_Enrollments_Create)]
    public async Task<StudentEnrollmentResponseDto> EnrollStudent(EnrollStudentRequestDto input)
    {
        var student = await _studentRepository.GetAsync(input.StudentId);
        if (student == null)
        {
            throw new UserFriendlyException(L("NotFound"));
        }

        var course = await _courseRepository.GetAsync(input.CourseId);
        if (course == null)
        {
            throw new UserFriendlyException(L("NotFound"));
        }

        var existingEnrollment = await _enrollmentRepository.FirstOrDefaultAsync(e =>
            e.StudentId == input.StudentId && e.CourseId == input.CourseId);

        if (existingEnrollment != null)
        {
            throw new UserFriendlyException(L("StudentIsAlreadyEnrolledInThisCourse"));
        }

        var enrollment = ObjectMapper.Map<StudentCourse>(input);

        var result = await _enrollmentRepository.InsertAsync(enrollment);
        await CurrentUnitOfWork.SaveChangesAsync();

        var enrollmentDto = ObjectMapper.Map<StudentEnrollmentResponseDto>(result);
        enrollmentDto.StudentName = $"{student.FirstName} {student.LastName}";
        enrollmentDto.CourseTitle = course.Title;

        return enrollmentDto;
    }

    [AbpAuthorize(PermissionNames.Pages_Enrollments_View)]
    public async Task<List<StudentEnrollmentResponseDto>> GetStudentEnrollments(int studentId)
    {
        var enrollments = await _enrollmentRepository.GetAll()
            .Include(e => e.Course)
            .Include(e=>e.Student)
            .Where(e => e.StudentId == studentId)
            .ToListAsync();

        var result = ObjectMapper.Map<List<StudentEnrollmentResponseDto>>(enrollments);

        return result;
    }
    
    [AbpAuthorize(PermissionNames.Pages_Enroll)]
    public async Task<StudentEnrollmentResponseDto> SelfEnroll(int courseId)
    {
        var student = await _studentRepository.FirstOrDefaultAsync(s => s.UserId == _abpSession.UserId);
        if (student == null)
        {
            throw new UserFriendlyException(L("NotFound"));
        }
    
        var enrollmentDto = new EnrollStudentRequestDto()
        {
            StudentId = student.Id,
            CourseId = courseId,
            EnrollmentDate = DateTime.Now,
            IsActive = true
        };
    
        return await EnrollStudent(enrollmentDto);
    }
    
    [AbpAuthorize(PermissionNames.Pages_My_Enrollments)]
    public async Task<List<StudentEnrollmentResponseDto>> GetMyCourses()
    {
        var userId = _abpSession.UserId;
    
        var student = await _studentRepository.FirstOrDefaultAsync(s => s.UserId == userId);
        if (student == null)
        {
            throw new UserFriendlyException(L("NotFound"));
        }
    
        return await GetStudentEnrollments(student.Id);
    }

}