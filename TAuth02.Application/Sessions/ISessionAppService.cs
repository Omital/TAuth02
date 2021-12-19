using System.Threading.Tasks;
using Abp.Application.Services;
using TAuth02.Sessions.Dto;

namespace TAuth02.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
