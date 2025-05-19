using MyProject.Configuration.Dto;
using System.Threading.Tasks;

namespace MyProject.Configuration;

public interface IConfigurationAppService
{
    Task ChangeUiTheme(ChangeUiThemeInput input);
}
