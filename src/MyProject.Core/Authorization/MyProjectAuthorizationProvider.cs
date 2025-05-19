using Abp.Authorization;
using Abp.Localization;
using Abp.MultiTenancy;

namespace MyProject.Authorization;

public class MyProjectAuthorizationProvider : AuthorizationProvider
{
    public override void SetPermissions(IPermissionDefinitionContext context)
    {
        context.CreatePermission(PermissionNames.Pages_Users, L("Users"));
        context.CreatePermission(PermissionNames.Pages_Users_Activation, L("UsersActivation"));
        context.CreatePermission(PermissionNames.Pages_Roles, L("Roles"));
        context.CreatePermission(PermissionNames.Pages_Tenants, L("Tenants"), multiTenancySides: MultiTenancySides.Host);
        
        var courses = context.CreatePermission(PermissionNames.Pages_Courses, L("Courses"));
        courses.CreateChildPermission(PermissionNames.Pages_Courses_View, L("Courses"));
        courses.CreateChildPermission(PermissionNames.Pages_Courses_Create, L("CreateCourse"));
        courses.CreateChildPermission(PermissionNames.Pages_Courses_Edit, L("EditCourse"));
        courses.CreateChildPermission(PermissionNames.Pages_Courses_Delete, L("DeleteCourse"));
    }

    private static ILocalizableString L(string name)
    {
        return new LocalizableString(name, MyProjectConsts.LocalizationSourceName);
    }
}
