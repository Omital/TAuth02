using System.Threading.Tasks;
using Abp.Application.Services;
using TAuth02.Authorization.Accounts.Dto;

namespace TAuth02.Authorization.Accounts
{
    public interface IAccountAppService : IApplicationService
    {
        Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input);

        Task<RegisterOutput> Register(RegisterInput input);
    }
}
