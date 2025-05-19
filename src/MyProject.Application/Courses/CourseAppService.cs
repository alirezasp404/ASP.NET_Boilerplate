using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Entities;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Linq.Extensions;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using MyProject.Authorization;
using MyProject.Courses.Dto;

namespace MyProject.Courses
{
    [AbpAuthorize]
    public class CourseAppService : ApplicationService, ICourseAppService
    {
        private readonly IRepository<Course, int> _courseRepository;

        public CourseAppService(IRepository<Course, int> courseRepository)
        {
            _courseRepository = courseRepository;
            LocalizationSourceName = "MyProject";
        }

        [AbpAuthorize(PermissionNames.Pages_Courses_View)]
        public async Task<PagedResultDto<CourseResponseDto>> GetAllAsync(GetCoursesRequest request)
        {
            Logger.Info("Getting all courses for input: " + request);

            try
            {
                var query = _courseRepository.GetAll()
                    .WhereIf(!request.Filter.IsNullOrWhiteSpace(), x => x.Title.Contains(request.Filter) || 
                                                                      x.Description.Contains(request.Filter) || 
                                                                      x.Code.Contains(request.Filter))
                    .WhereIf(request.IsActive.HasValue, x => x.IsActive == request.IsActive.Value);

                var totalCount = await query.CountAsync();
                
                if (request is IPagedResultRequest pagedResultRequest)
                {
                    query = query.PageBy(pagedResultRequest);
                }
                

                var courses = await query.ToListAsync();
                var dtos = ObjectMapper.Map<List<CourseResponseDto>>(courses);

                return new PagedResultDto<CourseResponseDto>(totalCount, dtos);
            }
            catch (Exception ex)
            {
                Logger.Error("Error getting all courses", ex);
                throw new UserFriendlyException(L("ErrorOccurredWhileProcessingRequest"));
            }
        }

        [AbpAuthorize(PermissionNames.Pages_Courses_View)]
        public async Task<CourseResponseDto> GetAsync(EntityDto<int> input)
        {
            Logger.Info("Getting course with id: " + input.Id);

            try
            {
                var course = await _courseRepository.GetAsync(input.Id);
                return ObjectMapper.Map<CourseResponseDto>(course);
            }
            catch (EntityNotFoundException)
            {
                Logger.Warn("Course not found with id: " + input.Id);
                throw new UserFriendlyException(L("NotFound"));
            }
            catch (Exception ex)
            {
                Logger.Error("Error getting course with id: " + input.Id, ex);
                throw new UserFriendlyException(L("ErrorOccurredWhileProcessingRequest"));
            }
        }

        [AbpAuthorize(PermissionNames.Pages_Courses_Create)]
        public async Task<CourseResponseDto> CreateAsync(CreateCourseRequestDto input)
        {
            Logger.Info("Creating a new course: " + input);

            try
            {
                var course = ObjectMapper.Map<Course>(input);
                await _courseRepository.InsertAsync(course);
                await CurrentUnitOfWork.SaveChangesAsync();

                Logger.Info("Created new course with id: " + course.Id);
                return ObjectMapper.Map<CourseResponseDto>(course);
            }
            catch (Exception ex)
            {
                Logger.Error("Error creating a new course", ex);
                throw new UserFriendlyException(L("ErrorOccurredWhileProcessingRequest"));
            }
        }

        [AbpAuthorize(PermissionNames.Pages_Courses_Edit)]
        public async Task<CourseResponseDto> UpdateAsync(UpdateCourseRequestDto input)
        {
            Logger.Info("Updating course with id: " + input.Id);

            try
            {
                var course = await _courseRepository.GetAsync(input.Id);
                if (course == null)
                {
                    throw new UserFriendlyException(L("NotFound"));
                }

                ObjectMapper.Map(input, course);
                await _courseRepository.UpdateAsync(course);
                await CurrentUnitOfWork.SaveChangesAsync();

                Logger.Info("Successfully updated course with id: " + course.Id);
                return ObjectMapper.Map<CourseResponseDto>(course);
            }
            catch (EntityNotFoundException)
            {
                Logger.Warn("Course not found with id: " + input.Id);
                throw new UserFriendlyException(L("NotFound"));
            }
            catch (Exception ex)
            {
                Logger.Error("Error updating course with id: " + input.Id, ex);
                throw new UserFriendlyException(L("ErrorOccurredWhileProcessingRequest"));
            }
        }

        [AbpAuthorize(PermissionNames.Pages_Courses_Delete)]
        public async Task DeleteAsync(EntityDto<int> input)
        {
            Logger.Info("Deleting course with id: " + input.Id);

            try
            {
                await _courseRepository.DeleteAsync(input.Id);
                await CurrentUnitOfWork.SaveChangesAsync();
                Logger.Info("Successfully deleted course with id: " + input.Id);
            }
            catch (EntityNotFoundException)
            {
                Logger.Warn("Course not found with id: " + input.Id);
                throw new UserFriendlyException(L("NotFound"));
            }
            catch (Exception ex)
            {
                Logger.Error("Error deleting course with id: " + input.Id, ex);
                throw new UserFriendlyException(L("ErrorOccurredWhileProcessingRequest"));
            }
        }
        
    }
}
