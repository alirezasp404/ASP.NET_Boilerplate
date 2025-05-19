using Abp.Application.Services;
using MyProject.Sessions.Dto;
using System.Threading.Tasks;

namespace MyProject.Sessions;

public interface ISessionAppService : IApplicationService
{
    Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
}
