using Abp.Zero.EntityFrameworkCore;
using MyProject.Authorization.Roles;
using MyProject.Authorization.Users;
using MyProject.MultiTenancy;
using Microsoft.EntityFrameworkCore;
using MyProject.Courses;

namespace MyProject.EntityFrameworkCore;

public class MyProjectDbContext : AbpZeroDbContext<Tenant, Role, User, MyProjectDbContext>
{
    public DbSet<Course> Courses { get; set; }

    public MyProjectDbContext(DbContextOptions<MyProjectDbContext> options)
        : base(options)
    {
    }
}