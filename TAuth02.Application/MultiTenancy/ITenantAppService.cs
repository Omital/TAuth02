using Abp.Application.Services;
using Abp.Application.Services.Dto;
using TAuth02.MultiTenancy.Dto;

namespace TAuth02.MultiTenancy
{
    public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedResultRequestDto, CreateTenantDto, TenantDto>
    {
    }
}
