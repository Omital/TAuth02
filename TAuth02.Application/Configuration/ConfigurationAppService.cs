using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Runtime.Session;
using TAuth02.Configuration.Dto;

namespace TAuth02.Configuration
{
    [AbpAuthorize]
    public class ConfigurationAppService : TAuth02AppServiceBase, IConfigurationAppService
    {
        public async Task ChangeUiTheme(ChangeUiThemeInput input)
        {
            await SettingManager.ChangeSettingForUserAsync(AbpSession.ToUserIdentifier(), AppSettingNames.UiTheme, input.Theme);
        }
    }
}
