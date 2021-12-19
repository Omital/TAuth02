using System.Threading.Tasks;
using Abp.Application.Services;
using TAuth02.Configuration.Dto;

namespace TAuth02.Configuration
{
    public interface IConfigurationAppService: IApplicationService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
    }
}