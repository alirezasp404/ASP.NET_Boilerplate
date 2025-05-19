using Abp.AspNetCore;
using Abp.AspNetCore.TestBase;
using Abp.Modules;
using Abp.Reflection.Extensions;
using MyProject.EntityFrameworkCore;
using MyProject.Web.Startup;
using Microsoft.AspNetCore.Mvc.ApplicationParts;

namespace MyProject.Web.Tests;

[DependsOn(
    typeof(MyProjectWebMvcModule),
    typeof(AbpAspNetCoreTestBaseModule)
)]
public class MyProjectWebTestModule : AbpModule
{
    public MyProjectWebTestModule(MyProjectEntityFrameworkModule abpProjectNameEntityFrameworkModule)
    {
        abpProjectNameEntityFrameworkModule.SkipDbContextRegistration = true;
    }

    public override void PreInitialize()
    {
        Configuration.UnitOfWork.IsTransactional = false; //EF Core InMemory DB does not support transactions.
    }

    public override void Initialize()
    {
        IocManager.RegisterAssemblyByConvention(typeof(MyProjectWebTestModule).GetAssembly());
    }

    public override void PostInitialize()
    {
        IocManager.Resolve<ApplicationPartManager>()
            .AddApplicationPartsIfNotAddedBefore(typeof(MyProjectWebMvcModule).Assembly);
    }
}