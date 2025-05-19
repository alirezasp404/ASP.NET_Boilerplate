using Abp.Application.Services;
using MyProject.Authorization.Accounts.Dto;
using System.Threading.Tasks;

namespace MyProject.Authorization.Accounts;

public interface IAccountAppService : IApplicationService
{
    Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input);

    Task<RegisterOutput> Register(RegisterInput input);
}
