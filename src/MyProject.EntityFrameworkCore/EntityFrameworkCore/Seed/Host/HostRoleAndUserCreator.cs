using Abp.Authorization;
using Abp.Authorization.Roles;
using Abp.Authorization.Users;
using Abp.MultiTenancy;
using MyProject.Authorization;
using MyProject.Authorization.Roles;
using MyProject.Authorization.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Linq;

namespace MyProject.EntityFrameworkCore.Seed.Host;

public class HostRoleAndUserCreator
{
    private readonly MyProjectDbContext _context;

    public HostRoleAndUserCreator(MyProjectDbContext context)
    {
        _context = context;
    }

    public void Create()
    {
        CreateHostRoleAndUsers();
    }

    private void CreateHostRoleAndUsers()
    {
        // Admin role for host

        var adminRoleForHost = _context.Roles.IgnoreQueryFilters().FirstOrDefault(r => r.TenantId == null && r.Name == StaticRoleNames.Host.Admin);
        if (adminRoleForHost == null)
        {
            adminRoleForHost = _context.Roles.Add(new Role(null, StaticRoleNames.Host.Admin, StaticRoleNames.Host.Admin) { IsStatic = true, IsDefault = true }).Entity;
            _context.SaveChanges();
        }

        // Grant all permissions to admin role for host

        var grantedPermissions = _context.Permissions.IgnoreQueryFilters()
            .OfType<RolePermissionSetting>()
            .Where(p => p.TenantId == null && p.RoleId == adminRoleForHost.Id)
            .Select(p => p.Name)
            .ToList();

        var permissions = PermissionFinder
            .GetAllPermissions(new MyProjectAuthorizationProvider())
            .Where(p => p.MultiTenancySides.HasFlag(MultiTenancySides.Host) &&
                        !grantedPermissions.Contains(p.Name))
            .ToList();

        if (permissions.Any())
        {
            _context.Permissions.AddRange(
                permissions.Select(permission => new RolePermissionSetting
                {
                    TenantId = null,
                    Name = permission.Name,
                    IsGranted = true,
                    RoleId = adminRoleForHost.Id
                })
            );
            _context.SaveChanges();
        }

        // Admin user for host

        var adminUserForHost = _context.Users.IgnoreQueryFilters().FirstOrDefault(u => u.TenantId == null && u.UserName == AbpUserBase.AdminUserName);
        if (adminUserForHost == null)
        {
            var user = new User
            {
                TenantId = null,
                UserName = AbpUserBase.AdminUserName,
                Name = "admin",
                Surname = "admin",
                EmailAddress = "admin@aspnetboilerplate.com",
                IsEmailConfirmed = true,
                IsActive = true
            };

            user.Password = new PasswordHasher<User>(new OptionsWrapper<PasswordHasherOptions>(new PasswordHasherOptions())).HashPassword(user, "123qwe");
            user.SetNormalizedNames();

            adminUserForHost = _context.Users.Add(user).Entity;
            _context.SaveChanges();

            // Assign Admin role to admin user
            _context.UserRoles.Add(new UserRole(null, adminUserForHost.Id, adminRoleForHost.Id));
            _context.SaveChanges();
        }
        
        var studentRole = _context.Roles.IgnoreQueryFilters()
            .FirstOrDefault(r => r.TenantId == null &&  r.Name == StaticRoleNames.Host.Student);
        if (studentRole == null)
        {
            studentRole = _context.Roles.Add(new Role(null, StaticRoleNames.Host.Student, "Student")
            {
                IsStatic = true
            }).Entity;

            _context.SaveChanges();
            var studentPermissions = new[]
            {
                PermissionNames.Pages_Courses_View,
                PermissionNames.Pages_Enroll,
                PermissionNames.Pages_My_Enrollments,
            };
                
            foreach (var permissionName in studentPermissions)
            {
                _context.RolePermissions.Add(new RolePermissionSetting
                {
                    TenantId = null,
                    RoleId = studentRole.Id,
                    Name = permissionName
                });
            }
                
            _context.SaveChanges();
        }
    }
}
