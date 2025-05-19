using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Authorization;
using Abp.Authorization.Users;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Abp.UI;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MyProject.Authorization;
using MyProject.Authorization.Roles;
using MyProject.Authorization.Users;
using MyProject.Students;
using MyProject.Students.Dto;
using MyProject.Users.Dto;

namespace MyProject.StudentCourses;
[AbpAuthorize(PermissionNames.Pages_Students)]
public class StudentAppService : AsyncCrudAppService<Student, StudentResponseDto, int, GetStudentsRequest, CreateStudentRequestDto, UpdateStudentRequestDto>
{
    private readonly UserManager _userManager;
    private readonly RoleManager _roleManager;

    public StudentAppService(IRepository<Student> repository,UserManager userManager,RoleManager roleManager)
        : base(repository)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public override async Task<StudentResponseDto> CreateAsync(CreateStudentRequestDto input)
    {
        var foundUser = await _userManager.FindByNameOrEmailAsync(input.Email);
        if (foundUser is not null)
        {
            throw new UserFriendlyException("UserWithThisUserNameAlreadyExists"); 
        }
        
        var newUser=new User
        {
            TenantId = null,
            UserName = input.Email,
            Name = input.FirstName,
            Surname =input.LastName,
            EmailAddress = input.Email,
            IsEmailConfirmed = true,
            IsActive = true
        };
        var password = new PasswordHasher<User>(new OptionsWrapper<PasswordHasherOptions>(new PasswordHasherOptions())).HashPassword(newUser, input.Password);
        var user = await _userManager.CreateAsync(newUser,input.Password);
        if (!user.Succeeded)
        {
            throw new UserFriendlyException(L("AnErrorOccurredWhileProcessingYourRequest"));
        }
        var userDto =await _userManager.FindByEmailAsync(newUser.EmailAddress);
        var student = ObjectMapper.Map<Student>(input);
        student.UserId = userDto.Id;
        var result = await Repository.InsertAsync(student);
        await CurrentUnitOfWork.SaveChangesAsync();
    
        await _userManager.AddToRoleAsync(userDto, StaticRoleNames.Host.Student);
    
        return ObjectMapper.Map<StudentResponseDto>(result);

    }

    protected override IQueryable<Student> CreateFilteredQuery(GetStudentsRequest input)
    {
        return Repository.GetAllIncluding(s => s.StudentCourses)
            .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), s => 
                s.FirstName.Contains(input.Filter) || 
                s.LastName.Contains(input.Filter) || 
                s.Email.Contains(input.Filter) ||
                s.StudentNumber.Contains(input.Filter));
    }
        
    public async Task<List<StudentResponseDto>> GetActiveStudents()
    {
        var students = await Repository.GetAll()
            .Where(s => s.IsActive)
            .ToListAsync();
                
        return ObjectMapper.Map<List<StudentResponseDto>>(students);
    }
}
