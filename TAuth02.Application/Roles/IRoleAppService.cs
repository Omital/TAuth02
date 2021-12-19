using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using TAuth02.Roles.Dto;

namespace TAuth02.Roles
{
    public interface IRoleAppService : IAsyncCrudAppService<RoleDto, int, PagedResultRequestDto, CreateRoleDto, RoleDto>
    {
        Task<ListResultDto<PermissionDto>> GetAllPermissions();
    }
}
