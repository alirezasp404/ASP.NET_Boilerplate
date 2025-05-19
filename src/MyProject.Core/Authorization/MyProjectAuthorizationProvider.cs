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
        
        var studentsPage = context.CreatePermission(PermissionNames.Pages_Students, L("Students"));
        studentsPage.CreateChildPermission(PermissionNames.Pages_Students_View, L("ViewStudents"));
        studentsPage.CreateChildPermission(PermissionNames.Pages_Students_Create, L("CreateStudents"));
        studentsPage.CreateChildPermission(PermissionNames.Pages_Students_Edit, L("EditStudents"));
        studentsPage.CreateChildPermission(PermissionNames.Pages_Students_Delete, L("DeleteStudents"));
        
        // Course enrollment permissions
        var enrollmentsPage = context.CreatePermission(PermissionNames.Pages_Enrollments, L("Enrollments"));
        enrollmentsPage.CreateChildPermission(PermissionNames.Pages_Enrollments_View, L("ViewEnrollments"));
        enrollmentsPage.CreateChildPermission(PermissionNames.Pages_Enrollments_Create, L("CreateEnrollments"));
        enrollmentsPage.CreateChildPermission(PermissionNames.Pages_Enrollments_Edit, L("EditEnrollments"));
        
        enrollmentsPage.CreateChildPermission(PermissionNames.Pages_My_Enrollments, L("MyEnrollments"));
        enrollmentsPage.CreateChildPermission(PermissionNames.Pages_Enroll, L("Enroll"));

    }

    private static ILocalizableString L(string name)
    {
        return new LocalizableString(name, MyProjectConsts.LocalizationSourceName);
    }
}
