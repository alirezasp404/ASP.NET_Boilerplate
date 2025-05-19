using Abp.Application.Services;
using MyProject.MultiTenancy.Dto;

namespace MyProject.MultiTenancy;

public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedTenantResultRequestDto, CreateTenantDto, TenantDto>
{
}

